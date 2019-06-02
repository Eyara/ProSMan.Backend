using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.Model;
using Microsoft.AspNetCore.Identity;
using MediatR;
using ProSMan.Backend.API.Application.Queries.Dashboards;

namespace ProSMan.Backend.Controllers
{
    public class DashboardController : ApiController
    {
		public DashboardController(ILoggerFactory loggerFactory,
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
		public async Task<IActionResult> Get()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;

			//if (userName == null)
			//{
			//	return Unauthorized();
			//}

			userName = "Eyara";

			return Ok(await _mediator.Send(new GetDashboardQuery(userName)));
		}
	}
}
