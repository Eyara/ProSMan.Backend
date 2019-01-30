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
		public async Task<IActionResult> Get(Guid id)
		{
			var entities = await _dbContext.Categories
				.Where(x => x.Id == id)
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id)
		{
			var entities = await _dbContext.Categories
				.Where(x => x.ProjectId == id)
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CategoryViewModel model)
		{
			Category category = _mapper.Map<Category>(model);
			category.Id = Guid.NewGuid();

			Project project = await _dbContext.Projects
				.Where(x => x.Id == model.ProjectId)
				.SingleOrDefaultAsync();

			if (project != null)
			{
				category.Project = project;
				_dbContext.Categories.Add(category);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel model)
		{
			var entity = await _dbContext.Categories
				.Where(x => x.Id == model.Id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				entity.Name = model.Name;
				_dbContext.SaveChanges();
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await _dbContext.Categories
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();

			if (entity != null)
			{
				_dbContext.Categories.Remove(entity);
				_dbContext.SaveChanges();
			}

			return Ok();
		}

	}
}
