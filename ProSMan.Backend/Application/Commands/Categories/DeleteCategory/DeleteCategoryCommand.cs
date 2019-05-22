using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
	public class DeleteCategoryCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteCategoryCommand(Guid id)
		{
			Id = id;
		}
	}
}
