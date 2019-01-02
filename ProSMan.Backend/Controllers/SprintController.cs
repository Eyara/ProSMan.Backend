using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
    public class SprintController : ApiController
    {
		public SprintController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper
			) : base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
		}

		public ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var entities = await _dbContext.Sprints
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> Get(int id)
		{
			var entities = await _dbContext.Sprints
				.Where(x => x.Id == id)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] SprintViewModel model)
		{
			Sprint sprint = _mapper.Map<Sprint>(model);
			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();

			if (project != null)
			{
				sprint.Project = project;
			}

			await _dbContext.AddAsync(sprint);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var entity = await _dbContext.Sprints
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				_dbContext.Remove(entity);
				_dbContext.SaveChanges();
			}

			return Ok();
		}
	}
}
