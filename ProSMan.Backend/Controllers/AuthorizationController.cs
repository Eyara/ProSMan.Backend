using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using ProSMan.Backend.Model;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Domain.ViewModels;
using System.Text;

namespace HealthyWorkout.Backend.Controllers
{
	[Route("api/[controller]")]
	public class AuthorizationController : Controller
	{
		public AuthorizationController(ILoggerFactory loggerFactory,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			IAuthorizationService authorizationService,
			ProSManContext dbContext,
			IOptions<IdentityOptions> identityOptions
			)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_dbContext = dbContext;
			_identityOptions = identityOptions;
		}

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ProSManContext _dbContext;
		private readonly IOptions<IdentityOptions> _identityOptions;

		[HttpPost("token")]
		public async Task<IActionResult> Token(OpenIdConnectRequest request)
		{
			if (request.IsPasswordGrantType())
			{
				var user = await _userManager.FindByNameAsync(request.Username);

				if (user == null)
				{
					return BadRequest(new
					{
						ErrorDescription = "Данного пользователя не существует"
					});
				}

				var result = await _userManager.CheckPasswordAsync(user, request.Password);
				if (result)
				{
					return await SignIn(request);
				}
				else
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "Неправильные логин/пароль"
					});
				}
			}

			if (request.IsRefreshTokenGrantType())
			{
				var info = await HttpContext.AuthenticateAsync(OpenIddictServerDefaults.AuthenticationScheme);
				var user = await _userManager.GetUserAsync(info.Principal);
				if (user != null)
				{
					return await SignIn(request);
				}
				else
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The refresh token is no longer valid."
					});
				}
			}

			return BadRequest(new OpenIdConnectResponse
			{
				Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
				ErrorDescription = "The specified grant type is not supported"
			});
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegistrationViewModel model)
		{
			Console.WriteLine(model.Fullname);
			Console.WriteLine(model.Password);
			Console.WriteLine(model.Username);
			var userIsExist = (await _userManager.FindByNameAsync(model.Username)) != null;

			if (userIsExist)
			{
				return BadRequest(new
				{
					ErrorDescription = "Такой пользователь уже существует"
				});
			}

			var user = new User { UserName = model.Username, Fullname = model.Fullname };
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
				return Ok();
			}
			else
			{
				return BadRequest(new
				{
					ErrorDescription = "Произошла неизвестная ошибка"
				});
			}
		}

		private async Task<IActionResult> SignIn(OpenIdConnectRequest request)
		{
			var user = new User()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = request.Username,
				NormalizedUserName = request.Username,
			};

			user.Fullname = (await _userManager.FindByNameAsync(user.UserName))?.Fullname;

			var ticket = await CreateTicketAsync(request, user);
			return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
		}

		private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, User user, AuthenticationProperties properties = null)
		{
			var principal = await _signInManager.CreateUserPrincipalAsync(user);

			var ticket = new AuthenticationTicket(principal, properties ?? new AuthenticationProperties(),
				OpenIdConnectServerDefaults.AuthenticationScheme);

			ticket.SetScopes(new[]
			{
				OpenIdConnectConstants.Scopes.OpenId,
				OpenIdConnectConstants.Scopes.Email,
				OpenIdConnectConstants.Scopes.Profile,
				OpenIdConnectConstants.Scopes.OfflineAccess,
				OpenIddictConstants.Scopes.Roles
			}.Intersect(request.GetScopes()));

			var claims = ticket.Principal.Claims
				.Where(item => item.Type != _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
				.ToList();

			foreach (var claim in claims)
			{
				claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
					OpenIdConnectConstants.Destinations.IdentityToken);
			}

			var identity = principal.Identity as ClaimsIdentity;

			identity.AddClaim(CustomClaimTypes.Id, user.Id, OpenIdConnectConstants.Destinations.IdentityToken);
			identity.AddClaim(CustomClaimTypes.Username, user.UserName, OpenIdConnectConstants.Destinations.IdentityToken);
			identity.AddClaim(CustomClaimTypes.Fullname, user.Fullname, OpenIdConnectConstants.Destinations.IdentityToken);

			return ticket;
		}

		public static class CustomClaimTypes
		{
			public const string Id = "id";
			public const string Username = "username";
			public const string Fullname = "fullname";
		}

	}
}
