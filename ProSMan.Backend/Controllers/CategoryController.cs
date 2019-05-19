using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProSMan.Backend.API.Application.Commands.Categories;
using ProSMan.Backend.API.Application.Queries.Category;
using ProSMan.Backend.Controllers;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Controllers
{
	public class CategoryController : ApiController
	{
		public CategoryController(ILoggerFactory loggerFactory,
			ProSManContext dbContext,
			IMapper autoMapper,
			IMediator mediator
			) : base(loggerFactory)
		{
			_dbContext = dbContext;
			_mapper = autoMapper;
			_mediator = mediator;
		}

		public ProSManContext _dbContext { get; set; }
		public IMediator _mediator { get; set; }
		private readonly IMapper _mapper;

		[HttpGet("GetByProjectId")]
		public async Task<IActionResult> GetByProjectId(Guid id)
		{
			return Ok(await _mediator.Send(new GetCategoryByProjectIdQuery(id)));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CategoryViewModel model)
		{
			return Ok(await _mediator.Send(new AddCategoryCommand(model)));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CategoryViewModel model)
		{
			return Ok(await _mediator.Send(new UpdateCategoryCommand(model)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteCategoryCommand(id)));
		}

	}
}
