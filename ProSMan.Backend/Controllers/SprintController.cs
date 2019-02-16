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

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id)
		{
			var entities = await _dbContext.Sprints
				.Where(x => x.ProjectId == id && !x.IsDeleted)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] SprintViewModel model)
		{
			Sprint sprint = _mapper.Map<Sprint>(model);
			sprint.Id = Guid.NewGuid();

			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();

			if (project != null)
			{
				sprint.Project = project;
				_dbContext.Sprints.Add(sprint);
				_dbContext.SaveChanges();
			}


			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateSprint([FromBody] SprintViewModel model)
		{
			var entity = await _dbContext.Sprints
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.Name = model.Name;
				entity.IsFinished = model.IsFinished;
				entity.FromDate = model.FromDate;
				entity.ToDate = model.ToDate;
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await _dbContext.Sprints
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
