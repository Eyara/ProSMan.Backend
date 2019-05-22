using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Commands.NonSprintTasks;
using ProSMan.Backend.API.Application.Queries.NonSprintTasks;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class NonSprintTaskController : ApiController
	{
		public NonSprintTaskController(ILoggerFactory loggerFactory,
			IMediator mediator
			) : base(loggerFactory)
		{
			_mediator = mediator;
		}

		public IMediator _mediator { get; set; }

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
			return Ok(await _mediator.Send(new UpdateNonSprintTaskCommand(model)));
		}

		[HttpPost("ToggleFinishTask")]
		public async Task<IActionResult> ToggleFinishTask(Guid id)
		{
			return Ok(await _mediator.Send(new FinishNonSprintTaskCommand(id)));
		}

		[HttpPut("MoveToSprint")]
		public async Task<IActionResult> MoveToSprint([FromBody] TaskMoveModel model)
		{ 
			return Ok(await _mediator.Send(new MoveToSprintCommand(model)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteNonSprintTaskCommand(id)));
		}
	}
}
