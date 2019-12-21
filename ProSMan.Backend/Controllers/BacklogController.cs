using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Commands.BacklogTasks;
using ProSMan.Backend.API.Application.Queries.BacklogTasks;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class BacklogController : ApiController
	{
		public BacklogController(ILoggerFactory loggerFactory,
			IMediator mediator
			) : base(loggerFactory)
		{
			_mediator = mediator;
		}

		public IMediator _mediator { get; set; }


		[HttpGet]
		public async Task<IActionResult> GetBacklog(Guid projectId)
		{
			return Ok(await _mediator.Send(new GetBacklogByProjectQuery(projectId)));
		}

		[HttpPost("Backlog")]
		public async Task<IActionResult> Backlog([FromBody] BacklogTaskViewModel model)
		{
			return Ok(await _mediator.Send(new AddBacklogCommand(model)));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTask([FromBody] BacklogTaskViewModel model)
		{
			return Ok(await _mediator.Send(new UpdateBacklogTaskCommand(model)));
		}

		[HttpPut("MoveToSprint")]
		public async Task<IActionResult> MoveToSprint([FromBody] TaskMoveModel model)
		{ 
			return Ok(await _mediator.Send(new MoveToSprintCommand(model)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteBacklogTaskCommand(id)));
		}
	}
}
