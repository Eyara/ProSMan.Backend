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
    public class TaskService: ITaskService
    {
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public TaskService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public ITask GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}

		public List<ITask> GetListBySprint(Guid id)
		{
			return GetOrderedList(x => x.SprintId == id &&
				!x.Sprint.IsDeleted && !x.Category.IsDeleted);
		}

		public List<ITask> GetListToday(string username)
		{
			return GetOrderedList(x => (x.Date ?? DateTime.MinValue).Date == DateTime.Today.Date &&
					x.Project.User.UserName == username &&
					!x.Sprint.IsDeleted && !x.Category.IsDeleted);
		}

		public bool Add(ITask model)
		{
			try
			{
				var task = _mapper.Map<Model.Task>(model as TaskViewModel);
				task.Id = Guid.NewGuid();
				
				_dbContext.Tasks.Add(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Update(ITask model)
		{
			try
			{
				var task = _mapper.Map<Model.Task>(model as TaskViewModel);
				_dbContext.Tasks.Update(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool Delete(ITask model)
		{
			try
			{
				var task = _mapper.Map<Model.Task>(model as TaskViewModel);
				_dbContext.Tasks.Remove(task);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public void DeleteByProjectId(Guid id)
		{
			var tasksToDelete = _dbContext.Tasks
				.Where(task => task.ProjectId == id);

			_dbContext.Tasks.RemoveRange(tasksToDelete);
			_dbContext.SaveChanges();
		}

		private List<ITask> GetOrderedList(Expression<Func<Model.Task, Boolean>> predicate)
		{
			return _dbContext.Tasks
				.Where(predicate)
				.OrderBy(x => x.IsFinished)
				.ThenByDescending(x => x.Priority)
				.ThenBy(x => x.Name)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.OfType<ITask>()
				.ToList();
		}

		private ITask GetItem(Expression<Func<Model.Task, bool>> predicate)
		{
			return _dbContext.Tasks
				.Where(predicate)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
