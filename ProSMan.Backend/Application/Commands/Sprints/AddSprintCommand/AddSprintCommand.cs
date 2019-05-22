using MediatR;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
    public class AddSprintCommand : IRequest<bool>
    {
		public SprintViewModel Sprint { get; set; }

		public AddSprintCommand(SprintViewModel sprint)
		{
			Sprint = sprint;
		}
    }
}
