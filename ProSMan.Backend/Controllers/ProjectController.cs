using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProSMan.Backend.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProSMan.Backend.Controllers
{
    public class ProjectController : ApiController
    {
		public ProjectController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper,
			UserManager<User> userManager
			) : base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
			_userManager = userManager;
		}

		private ProSManContext _dbContext;
		private UserManager<User> _userManager;
		private readonly IMapper _mapper;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userName = User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;


			if (userName == null)
			{
				return Unauthorized();
			}


			var entities = await _dbContext.Projects
				.Where(x => !x.IsDeleted && x.User.UserName == userName)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var entities = await _dbContext.Projects
				.Where(x => !x.IsDeleted && x.Id == id)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetAllWithDeleted")]
		public async Task<IActionResult> GetAllWithDeleted()
		{
			var user = await _userManager.GetUserAsync(User);

			var entities = await _dbContext.Projects
				.Where(x => x.UserId == user.Id)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
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

			Project project = _mapper.Map<Project>(model);
			project.Id = Guid.NewGuid();
			project.User = user;
			project.UserId = user.Id;

			await _dbContext.AddAsync(project);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProject([FromBody] ProjectViewModel model)
		{
			var entity = await _dbContext.Projects
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				Project project = _mapper.Map(model, entity);
				_dbContext.Update(project);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await _dbContext.Projects
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.IsDeleted = true;
				_dbContext.Update(entity);
				_dbContext.SaveChanges();
			}

			return Ok();
		}
	}
}
