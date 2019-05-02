﻿using AutoMapper;
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
		public async Task<IActionResult> GetById(Guid id)
		{
			var entities = await _dbContext.Sprints
				.Where(x => !x.IsDeleted && x.Id == id)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetUnfinished")]
		public async Task<IActionResult> GetUnfinishedByProjectId(Guid id)
		{
			var entities = await _dbContext.Sprints
				.Where(x => !x.IsDeleted && !x.IsFinished && x.ProjectId == id)
				.OrderBy(x => x.FromDate)
				.ProjectTo<SprintViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return Ok(new { data = entities });
		}

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id)
		{
			var entities = await _dbContext.Sprints
				.Where(x => x.ProjectId == id && !x.IsDeleted)
				.OrderBy(x => x.FromDate)
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

		[HttpPut("Finish")]
		public async Task<IActionResult> Finish(Guid id)
		{
			var sprint = await _dbContext.Sprints.FirstOrDefaultAsync(x => x.Id == id);
			
			if (sprint == null)
			{
				return BadRequest();
			}

			sprint.IsFinished = true;

			var nonFinishedTasks = await _dbContext.Tasks
				.Where(x => x.SprintId == id && !x.IsFinished)
				.ToListAsync();

			var nonSprintTasks = _mapper
				.Map<List<NonSprintTask>>(nonFinishedTasks);

			nonSprintTasks.ForEach(x => x.IsBacklog = true);

			_dbContext.Sprints.Update(sprint);
			_dbContext.NonSprintTasks.AddRange(nonSprintTasks);
			_dbContext.Tasks.RemoveRange(nonFinishedTasks);
			_dbContext.SaveChanges();

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
