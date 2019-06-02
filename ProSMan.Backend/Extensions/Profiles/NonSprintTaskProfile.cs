using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Extensions.Profiles
{
    public class NonSprintTaskProfile: Profile
    {
		public NonSprintTaskProfile()
		{
			CreateMap<NonSprintTask, NonSprintTaskViewModel>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId));

			CreateMap<NonSprintTaskViewModel, NonSprintTask>()
				.ForMember(dest => dest.Project, opts => opts.Ignore());

			CreateMap<TaskViewModel, NonSprintTask>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId))
				.ForMember(dest => dest.Project, opts => opts.Ignore())
				.ForMember(dest => dest.IsBacklog, opts => opts.Ignore());
		}
    }
}
