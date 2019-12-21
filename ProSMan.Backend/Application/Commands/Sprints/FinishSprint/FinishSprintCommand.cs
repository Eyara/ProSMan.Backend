using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
    public class FinishSprintCommand : IRequest<bool>
    {
		public Guid Id { get; set; }

		public FinishSprintCommand(Guid id)
		{
			Id = id;
		}
    }
}
