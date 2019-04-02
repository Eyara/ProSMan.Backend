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
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(dest => dest.TimeEstimate, opts => opts.MapFrom(src => src.TimeEstimate))
				.ForMember(dest => dest.Priority, opts => opts.MapFrom(src => (ProSMan.Backend.Domain.ViewModels.Priority)src.Priority))
				.ForMember(dest => dest.IsFinished, opts => opts.MapFrom(src => src.IsFinished))
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.Project.Id))
				.ReverseMap();
		}
    }
}
