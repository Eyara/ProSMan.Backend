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
	public class NonSprintTaskController : ApiController
	{
		public NonSprintTaskController(ILoggerFactory loggerFactory,
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
		public async Task<IActionResult> GetNonSprintTasks()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			if (userName == null)
			{
				return Unauthorized();
			}

			var entities = await _dbContext.NonSprintTasks
				.Where(x => !x.IsBacklog && x.Project.User.UserName == userName)
				.ProjectTo<NonSprintTaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("getBacklog")]
		public async Task<IActionResult> GetBacklog()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			if (userName == null)
			{
				return Unauthorized();
			}

			var entities = await _dbContext.NonSprintTasks
				.Where(x => x.IsBacklog && x.Project.User.UserName == userName)
				.ProjectTo<NonSprintTaskViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}


		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NonSprintTaskViewModel model)
		{
			Model.NonSprintTask task = _mapper.Map<Model.NonSprintTask>(model);
			task.Id = Guid.NewGuid();

			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();


			if (project != null)
			{
				task.Project = project;
				_dbContext.NonSprintTasks.Add(task);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTask([FromBody] NonSprintTaskViewModel model)
		{
			var entity = await _dbContext.NonSprintTasks
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.Name = model.Name;
				entity.Priority = (Model.Priority)model.Priority;
				entity.Description = model.Description;
				entity.IsFinished = model.IsFinished;
				entity.TimeEstimate = model.TimeEstimate;
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpPost("ToggleFinishTask")]
		public async Task<IActionResult> ToggleFinishTask(Guid id)
		{
			var entity = await _dbContext.NonSprintTasks
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.IsFinished = !entity.IsFinished;
				_dbContext.NonSprintTasks.Update(entity);
				await _dbContext.SaveChangesAsync();
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await _dbContext.NonSprintTasks
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				_dbContext.NonSprintTasks.Remove(entity);
				_dbContext.SaveChanges();
			}

			return Ok();
		}
	}
}
