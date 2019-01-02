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
    public class CategoryController : ApiController
    {
		public CategoryController(ILoggerFactory loggerFactory,
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
			var entities = await _dbContext.Categories
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> Get(int id)
		{
			var entities = await _dbContext.Categories
				.Where(x => x.Id == id)
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CategoryViewModel model)
		{
			Category category = _mapper.Map<Category>(model);
			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();

			if (project != null)
			{
				category.Project = project;
			}
			
			await _dbContext.AddAsync(category);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var entity = await _dbContext.Categories
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
