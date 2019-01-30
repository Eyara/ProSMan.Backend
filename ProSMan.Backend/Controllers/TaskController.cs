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

		[HttpGet("GetById")]
		public async Task<IActionResult> Get(Guid id)
		{
			var entities = await _dbContext.Tasks
				.Where(x => x.Id == id)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id)
		{
			var entities = await _dbContext.Tasks
				.Where(x => x.ProjectId == id)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetBySprintId")]
		public async Task<IActionResult> GetBySprintId(Guid id)
		{
			var entities = await _dbContext.Tasks
				.Where(x => x.SprintId == id)
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TaskViewModel model)
		{
			Model.Task task = _mapper.Map<Model.Task>(model);
			task.Id = Guid.NewGuid();

			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();

			Category category = await _dbContext.Categories
				.Where(x => x.Id == model.CategoryId)
				.SingleOrDefaultAsync();

			Sprint sprint = await _dbContext.Sprints
				.Where(x => x.Id == model.SprintId)
				.SingleOrDefaultAsync();


			if (project != null && category != null && sprint != null)
			{
				task.Project = project;
				task.Category = category;
				task.Sprint = sprint;
				_dbContext.Tasks.Add(task);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTask([FromBody] TaskViewModel model)
		{
			var entity = await _dbContext.Tasks
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.Name = model.Name;
				entity.Priority = (Model.Priority)model.Priority;
				entity.ActualSpentTime = model.ActualSpentTime;
				entity.Description = model.Description;
				entity.IsFinished = model.IsFinished;
				entity.TimeEstimate = model.TimeEstimate;
				_dbContext.SaveChanges();
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
