using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Queries.Tasks;
using ProSMan.Backend.API.Application.Commands.Tasks;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class TaskController : ApiController
	{
		public TaskController(ILoggerFactory loggerFactory,
			IMediator mediator
			) : base(loggerFactory)
		{
			_mediator = mediator;
		}

		private IMediator _mediator;

		[HttpGet("GetBySprintId")]
		public async Task<IActionResult> GetBySprintId(Guid id)
		{
			return Ok(await _mediator.Send(new GetTasksBySprintQuery(id)));
		}

		[HttpGet("getTodayTasks")]
		public async Task<IActionResult> GetTodayTasks()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			if (userName == null)
			{
				return Unauthorized();
			}

			return Ok(await _mediator.Send(new GetTodayTasksQuery(userName)));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TaskViewModel model)
		{
			return Ok(await _mediator.Send(new AddTaskCommand(model)));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] TaskViewModel model)
		{
			return Ok(await _mediator.Send(new UpdateTaskCommand(model)));
		}

		[HttpPost("ToggleFinishTask")]
		public async Task<IActionResult> ToggleFinish(Guid id)
		{
			return Ok(await _mediator.Send(new ToggleFinishTaskCommand(id)));
		}

		[HttpPost("ToggleTodayTask")]
		public async Task<IActionResult> ToggleToday(Guid id)
		{
			return Ok(await _mediator.Send(new ToggleTodayTaskCommand(id)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteTaskCommand(id)));
		}
	}
}
