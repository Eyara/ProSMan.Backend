using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class DeleteTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
