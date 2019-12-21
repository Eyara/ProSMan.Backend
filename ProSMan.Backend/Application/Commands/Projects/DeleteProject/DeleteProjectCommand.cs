using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class DeleteProjectCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteProjectCommand(Guid id)
		{
			Id = id;
		}
	}
}
