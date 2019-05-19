using MediatR;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
    public class AddProjectCommand : IRequest<bool>
    {
		public ProjectViewModel Project { get; set; }
		public User User { get; set; }

		public AddProjectCommand(ProjectViewModel project, User user)
		{
			Project = project;
			User = user;
		}
    }
}
