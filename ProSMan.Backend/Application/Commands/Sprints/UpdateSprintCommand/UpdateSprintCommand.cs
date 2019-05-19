using MediatR;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
    public class UpdateSprintCommand : IRequest<bool>
    {
		public SprintViewModel Sprint { get; set; }

		public UpdateSprintCommand(SprintViewModel sprint)
		{
			Sprint = sprint;
		}
    }
}
