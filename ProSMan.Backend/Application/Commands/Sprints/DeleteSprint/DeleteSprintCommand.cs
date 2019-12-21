using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
    public class DeleteSprintCommand : IRequest<bool>
    {
		public Guid Id { get; set; }

		public DeleteSprintCommand(Guid id)
		{
			Id = id;
		}
    }
}
