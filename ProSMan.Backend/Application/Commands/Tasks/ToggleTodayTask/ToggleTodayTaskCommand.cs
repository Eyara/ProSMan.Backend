using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class ToggleTodayTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public ToggleTodayTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
