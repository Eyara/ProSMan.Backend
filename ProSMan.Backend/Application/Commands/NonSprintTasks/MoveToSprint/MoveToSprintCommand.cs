﻿using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class MoveToSprintCommand : IRequest<bool>
	{
		public TaskMoveModel Task { get; set; }

		public MoveToSprintCommand(TaskMoveModel task)
		{
			Task = task;
		}
	}
}
