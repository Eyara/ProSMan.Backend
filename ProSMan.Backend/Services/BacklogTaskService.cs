using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class BacklogTaskService: IBacklogTaskService
	{
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public BacklogTaskService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public IBacklogTask GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}

		public List<IBacklogTask> GetListByProject(Guid id)
		{
			return GetOrderedList(x => x.ProjectId == id);
		}

		public bool Add(IBacklogTask model)
		{
			try
			{
				var task = _mapper.Map<BacklogTask>(model as BacklogTaskViewModel);
				task.Id = Guid.NewGuid();
				
				_dbContext.BacklogTasks.Add(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Update(IBacklogTask model)
		{
			try
			{
				var task = _mapper.Map<BacklogTask>(model as BacklogTaskViewModel);
				_dbContext.BacklogTasks.Update(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Delete(IBacklogTask model)
		{
			try
			{
				var backlogTask = _mapper.Map<BacklogTask>(model as BacklogTaskViewModel);
				_dbContext.BacklogTasks.Remove(backlogTask);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private List<IBacklogTask> GetOrderedList(Expression<Func<BacklogTask, Boolean>> predicate)
		{
			return _dbContext.BacklogTasks
				.Where(predicate)
				.OrderBy(x => x.Name)
				.ProjectTo<BacklogTaskViewModel>(_mapper.ConfigurationProvider)
				.OfType<IBacklogTask>()
				.ToList();
		}

		private IBacklogTask GetItem(Expression<Func<BacklogTask, bool>> predicate)
		{
			return _dbContext.BacklogTasks
				.Where(predicate)
				.ProjectTo<BacklogTaskViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
