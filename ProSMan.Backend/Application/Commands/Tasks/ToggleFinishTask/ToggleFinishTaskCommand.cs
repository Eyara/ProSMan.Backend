using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class ToggleFinishTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public ToggleFinishTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
