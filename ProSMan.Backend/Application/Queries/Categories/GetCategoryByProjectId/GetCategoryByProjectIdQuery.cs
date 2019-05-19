using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.Category
{
    public class GetCategoryByProjectIdQuery : IRequest<ListRequest<CategoryViewModel>>
	{
		public Guid Id { get; set; }

		public GetCategoryByProjectIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
