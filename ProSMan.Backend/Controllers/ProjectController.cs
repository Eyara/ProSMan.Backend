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
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> Get(int id)
		{
			var entities = await _dbContext.Projects
				.Where(x => x.Id == id)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ProjectViewModel model)
		{
			Project project = _mapper.Map<Project>(model);
			await _dbContext.AddAsync(project);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var entity = await _dbContext.Projects
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
