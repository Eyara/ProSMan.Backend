using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
    public class AddTaskCommand : IRequest<bool>
    {
		public TaskViewModel Task { get; set; }

		public AddTaskCommand(TaskViewModel task)
		{
			Task = task;
		}
    }
}
