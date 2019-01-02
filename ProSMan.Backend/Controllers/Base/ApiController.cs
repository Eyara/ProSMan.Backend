using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ProSMan.Backend.Controllers
{
	[Route("api/[controller]")]
	public abstract class ApiController : Controller
	{
		protected ApiController(
			ILoggerFactory loggerFactory
		)
		{
			Logger = loggerFactory.CreateLogger(GetType());
		}

		protected ILogger Logger { get; }

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Exception != null)
			{
				Logger.LogError(context.Exception, "Unhandled API exception");
			}
			base.OnActionExecuted(context);
		}
	}
}
