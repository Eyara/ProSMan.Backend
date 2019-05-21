using MediatR;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class FinishNonSprintTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public FinishNonSprintTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
