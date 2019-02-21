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

namespace ProSMan.Backend.Controllers
{
    public class ProjectController : ApiController
    {
		public ProjectController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper
			): base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
		}

		public ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var entities = await _dbContext.Projects
				.Where(x => !x.IsDeleted)
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
			var entities = await _dbContext.Projects
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ProjectViewModel model)
		{
			Project project = _mapper.Map<Project>(model);
			project.Id = Guid.NewGuid();
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
