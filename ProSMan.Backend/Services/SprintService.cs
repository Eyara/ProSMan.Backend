﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProSMan.Backend.Core.Base;
using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProSMan.Backend.API.Services
{
	public class SprintService : ISprintService
	{
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public SprintService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public ISprint GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}

		public List<ISprint> GetListById(Guid id)
		{
			return GetOrderedList(x => !x.IsDeleted && x.Id == id);
		}

		public List<ISprint> GetUnfinishedListByProjectId(Guid id)
		{
			return GetOrderedList(x => !x.IsDeleted && !x.IsFinished && x.ProjectId == id);
		}

		public List<ISprint> GetListByProjectId(Guid id)
		{
			return GetOrderedList(x => !x.IsDeleted && x.ProjectId == id);
		}

		public PaginationResponse<ISprint> GetListByProjectId(ISprintListPagination model)
		{
			return GetPaginateOrderedList(x => !x.IsDeleted && x.ProjectId == model.ProjectId, model.CurrentPage, model.PageCount);
		}

		public bool Add(ISprint model)
		{
			try
			{
				var sprint = _mapper.Map<Sprint>(model as SprintViewModel);

				sprint.Id = Guid.NewGuid();

				_dbContext.Sprints.Add(sprint);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Update(ISprint model)
		{
			try
			{
				var sprint = _mapper.Map<Sprint>(model as SprintViewModel);
				_dbContext.Sprints.Update(sprint);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Finish(ISprint model)
		{
			try
			{
				var sprint = _mapper.Map<Sprint>(model as SprintViewModel);

				sprint.IsFinished = true;
				sprint.FinishedOn = DateTime.UtcNow;

				var nonFinishedTasks = _dbContext.Tasks
					.Where(x => x.SprintId == sprint.Id && !x.IsFinished)
					.ToList();


				var backlogTasks = _mapper
					.Map<List<BacklogTask>>(nonFinishedTasks);

				_dbContext.Sprints.Update(sprint);
				_dbContext.BacklogTasks.AddRange(backlogTasks);
				_dbContext.Tasks.RemoveRange(nonFinishedTasks);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Delete(ISprint model)
		{
			try
			{
				var sprint = _mapper.Map<Sprint>(model as SprintViewModel);
				sprint.IsDeleted = true;

				_dbContext.Sprints.Update(sprint);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Delete(List<ISprint> sprints)
		{
			try
			{
				var sprintsList = _mapper.Map<List<Sprint>>(sprints.ConvertAll(sprint => sprint as SprintViewModel));

				_dbContext.Sprints.RemoveRange(sprintsList);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private List<ISprint> GetOrderedList(Expression<Func<Sprint, Boolean>> predicate)
		{
			return _dbContext.Sprints
				.Where(predicate)
				.OrderBy(x => x.FromDate)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.OfType<ISprint>()
				.ToList();
		}

		private PaginationResponse<ISprint> GetPaginateOrderedList(Expression<Func<Sprint, Boolean>> predicate,
			int currentPage = 1, int pageCount = 0)
		{
			var orderedList = _dbContext.Sprints
				.Where(predicate)
				.OrderBy(x => x.FromDate);

			var totalCount = orderedList.Count();
			var lastPage = pageCount > 0 ? (int)Math.Ceiling((double)totalCount / pageCount) : 0;

			var paginatedList = orderedList
				.Skip((currentPage - 1) * pageCount)
				.Take(pageCount > 0 ? pageCount : totalCount)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.OfType<ISprint>()
				.ToList();

			return new PaginationResponse<ISprint>
			{
				CurrentPage = currentPage,
				PageCount = pageCount,
				LastPage = lastPage,
				TotalCount = totalCount,
				Items = paginatedList
			};
		}

		private ISprint GetItem(Expression<Func<Sprint, Boolean>> predicate)
		{
			return _dbContext.Sprints
				.Where(predicate)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
