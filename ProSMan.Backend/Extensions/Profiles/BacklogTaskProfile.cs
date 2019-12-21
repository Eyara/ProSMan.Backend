using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Extensions.Profiles
{
    public class BacklogTaskProfile: Profile
    {
		public BacklogTaskProfile()
		{
			CreateMap<BacklogTask, BacklogTaskViewModel>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId));

			CreateMap<BacklogTaskViewModel, BacklogTask>()
				.ForMember(dest => dest.Project, opts => opts.Ignore());

			CreateMap<TaskViewModel, BacklogTask>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId))
				.ForMember(dest => dest.Project, opts => opts.Ignore());
		}
    }
}
