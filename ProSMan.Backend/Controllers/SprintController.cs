using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Commands.Sprints;
using ProSMan.Backend.API.Application.Queries.Sprints;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class SprintController : ApiController
	{
		public SprintController(ILoggerFactory loggerFactory,
			IMediator mediator
			) : base(loggerFactory)
		{
			_mediator = mediator;
		}
		
		private IMediator _mediator { get; set; }

		[HttpGet("GetById")]
		public async Task<IActionResult> GetById(Guid id)
		{

			return Ok(await _mediator.Send(new GetSprintsByIdQuery(id)));
		}

		[HttpGet("GetUnfinished")]
		public async Task<IActionResult> GetUnfinishedByProjectId(Guid id)
		{
			return Ok(await _mediator.Send(new GetUnfinishedQuery(id)));
		}

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id, int currentPage, int pageCount)
		{
			var model = new SprintListPagination
			{
				ProjectId = id,
				CurrentPage = currentPage,
				PageCount = pageCount
			};

			return Ok(await _mediator.Send(new GetSprintsByProjectQuery(model)));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] SprintViewModel model)
		{
			return Ok(await _mediator.Send(new AddSprintCommand(model)));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] SprintViewModel model)
		{
			return Ok(await _mediator.Send(new UpdateSprintCommand(model)));
		}

		[HttpPut("Finish")]
		public async Task<IActionResult> Finish(Guid id)
		{
			return Ok(await _mediator.Send(new FinishSprintCommand(id)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteSprintCommand(id)));
		}
	}
}
