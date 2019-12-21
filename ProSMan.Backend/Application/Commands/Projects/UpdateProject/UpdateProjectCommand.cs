using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class UpdateProjectCommand : IRequest<bool>
	{
		public ProjectViewModel Project { get; set; }

		public UpdateProjectCommand(ProjectViewModel project)
		{
			Project = project;
		}
	}
}
