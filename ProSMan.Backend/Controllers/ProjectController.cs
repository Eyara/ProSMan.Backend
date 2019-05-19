using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;
using Microsoft.AspNetCore.Identity;
using MediatR;
using ProSMan.Backend.API.Application.Queries.Projects;
using ProSMan.Backend.API.Application.Commands.Projects;

namespace ProSMan.Backend.Controllers
{
    public class ProjectController : ApiController
    {
		public ProjectController(ILoggerFactory loggerFactory,
			IMediator mediator,
			UserManager<User> userManager
			) : base(loggerFactory)
		{
			_userManager = userManager;
			_mediator = mediator;
		}
		
		private UserManager<User> _userManager;
		private IMediator _mediator;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			if (userName == null)
			{
				return Unauthorized();
			}

			return Ok(await _mediator.Send(new GetProjectsQuery(userName)));
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(new GetProjectsByIdQuery(id));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ProjectViewModel model)
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			if (userName == null)
			{
				return Unauthorized();
			}

			var user = await _userManager.FindByNameAsync(userName);

			return Ok(await _mediator.Send(new AddProjectCommand(model, user)));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] ProjectViewModel model)
		{
			return Ok(await _mediator.Send(new UpdateProjectCommand(model)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteProjectCommand(id)));
		}
	}
}
