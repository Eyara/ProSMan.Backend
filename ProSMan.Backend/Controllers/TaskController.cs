using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class TaskController : ApiController
	{
		public TaskController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper
			) : base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
		}

		public ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var entities = await _dbContext.Tasks
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetBySprintId")]
		public async Task<IActionResult> GetBySprintId(Guid id)
		{
			var entities = await _dbContext.Tasks
				.Where(x => x.SprintId == id && !x.Sprint.IsDeleted && !x.Category.IsDeleted)
				.OrderBy(x => x.IsFinished)
				.ThenByDescending(x => x.Priority)
				.ThenBy(x => x.Name)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("getTodayTasks")]
		public async Task<IActionResult> GetTodayTasks()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;


			if (userName == null)
			{
				return Unauthorized();
			}

			var entities = await _dbContext.Tasks
				.Where(x => (x.Date ?? DateTime.MinValue).Date == DateTime.Today.Date &&
					x.Project.User.UserName == userName &&
					!x.Sprint.IsDeleted && !x.Category.IsDeleted)
				.OrderBy(x => x.IsFinished)
				.ThenByDescending(x => (int)x.Priority)
				.ThenBy(x => x.Name)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TaskViewModel model)
		{
			Model.Task task = _mapper.Map<Model.Task>(model);
			task.Id = Guid.NewGuid();

			_dbContext.Tasks.Add(task);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTask([FromBody] TaskViewModel model)
		{
			var entity = await _dbContext.Tasks
				.Where(x => x.Id == model.Id)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				var task = _mapper.Map<Model.Task>(model);
				_dbContext.Tasks.Update(task);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpPost("ToggleFinishTask")]
		public async Task<IActionResult> ToggleFinishTask(Guid id)
		{
			var entity = await _dbContext.Tasks
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.IsFinished = !entity.IsFinished;
				_dbContext.Update(entity);
				await _dbContext.SaveChangesAsync();
			}

			return Ok();
		}

		[HttpPost("ToggleTodayTask")]
		public async Task<IActionResult> ToggleTodayTask(Guid id)
		{
			var entity = await _dbContext.Tasks
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.Date = !entity.Date.HasValue ? DateTime.Today : (DateTime?)null;
				_dbContext.Update(entity);
				await _dbContext.SaveChangesAsync();
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await _dbContext.Tasks
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				_dbContext.Tasks.Remove(entity);
				_dbContext.SaveChanges();
			}

			return Ok();
		}
	}
}
