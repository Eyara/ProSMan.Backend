using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProSMan.Backend.API.Services
{
    public class NonSprintTaskService: INonSprintTaskService
	{
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public NonSprintTaskService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public INonSprintTask GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}

		public List<INonSprintTask> GetListByProject(Guid id)
		{
			return GetOrderedList(x => !x.IsBacklog && x.ProjectId == id);
		}

		public List<INonSprintTask> GetBacklogListByProject(Guid id)
		{
			return GetOrderedList(x => x.IsBacklog && x.ProjectId == id);
		}

		public bool Add(INonSprintTask model)
		{
			try
			{
				var task = _mapper.Map<Model.NonSprintTask>(model as NonSprintTaskViewModel);
				task.Id = Guid.NewGuid();
				
				_dbContext.NonSprintTasks.Add(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Update(INonSprintTask model)
		{
			try
			{
				var nonSprintTask = _mapper.Map<Model.NonSprintTask>(model as NonSprintTaskViewModel);
				_dbContext.NonSprintTasks.Update(nonSprintTask);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Delete(INonSprintTask model)
		{
			try
			{
				var nonSprintTask = _mapper.Map<Model.NonSprintTask>(model as NonSprintTaskViewModel);
				_dbContext.NonSprintTasks.Remove(nonSprintTask);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private List<INonSprintTask> GetOrderedList(Expression<Func<Model.NonSprintTask, Boolean>> predicate)
		{
			return _dbContext.NonSprintTasks
				.Where(predicate)
				.OrderBy(x => x.IsFinished)
				.ThenByDescending(x => x.Priority)
				.ThenBy(x => x.Name)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.OfType<INonSprintTask>()
				.ToList();
		}

		private INonSprintTask GetItem(Expression<Func<Model.NonSprintTask, bool>> predicate)
		{
			return _dbContext.NonSprintTasks
				.Where(predicate)
				.ProjectTo<NonSprintTaskViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
