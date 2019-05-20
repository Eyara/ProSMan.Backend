using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Commands.NonSprintTasks;
using ProSMan.Backend.API.Application.Queries.NonSprintTasks;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class NonSprintTaskController : ApiController
	{
		public NonSprintTaskController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper,
			IMediator mediator
			) : base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
			_mediator = mediator;
		}

		public ProSManContext _dbContext { get; set; }
		public IMediator _mediator { get; set; }
		private readonly IMapper _mapper;

		[HttpGet]
		public async Task<IActionResult> GetNonSprintTasks(Guid projectId)
		{
			return Ok(await _mediator.Send(new GetNonSprintsTasksByProjectQuery(projectId)));
		}

		[HttpGet("getBacklog")]
		public async Task<IActionResult> GetBacklog(Guid projectId)
		{
			return Ok(await _mediator.Send(new GetBacklogByProjectQuery(projectId)));
		}


		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NonSprintTaskViewModel model)
		{
			return Ok(await _mediator.Send(new AddNonSprintTaskCommand(model)));
		}

		[HttpPost("Backlog")]
		public async Task<IActionResult> Backlog([FromBody] NonSprintTaskViewModel model)
		{
			return Ok(await _mediator.Send(new AddBacklogCommand(model)));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTask([FromBody] NonSprintTaskViewModel model)
		{
			var entity = await _dbContext.NonSprintTasks
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				var nonSprintTask = _mapper.Map<NonSprintTask>(model);
				_dbContext.NonSprintTasks.Update(nonSprintTask);
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

		[HttpPut("MoveToSprint")]
		public async Task<IActionResult> MoveToSprint([FromBody] TaskMoveViewModel model)
		{
			var nonSprintTask = await _dbContext.NonSprintTasks.FirstOrDefaultAsync(x => x.Id == model.Id);

			if (nonSprintTask == null)
			{
				return BadRequest();
			}

			Project project = await _dbContext.Projects
				.Where(x => x.Id == nonSprintTask.ProjectId)
				.SingleOrDefaultAsync();

			Category category = await _dbContext.Categories
				.Where(x => x.Id == model.CategoryId)
				.SingleOrDefaultAsync();

			Sprint sprint = await _dbContext.Sprints
				.Where(x => x.Id == model.SprintId)
				.SingleOrDefaultAsync();

			var task = new Model.Task
			{
				Id = model.Id,
				SprintId = model.SprintId,
				CategoryId = model.CategoryId,
				Name = nonSprintTask.Name,
				Description = nonSprintTask.Description,
				TimeEstimate = nonSprintTask.TimeEstimate,
				Priority = nonSprintTask.Priority,
				IsFinished = nonSprintTask.IsFinished,
				Date = nonSprintTask.Date,
				Sprint = sprint,
				Project = project,
				Category = category
			};

			_dbContext.Tasks.Add(task);
			_dbContext.NonSprintTasks.Remove(nonSprintTask);
			await _dbContext.SaveChangesAsync();

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
