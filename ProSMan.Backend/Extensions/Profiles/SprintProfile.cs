using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Profiles
{
	public class SprintProfile : Profile
	{
		public SprintProfile()
		{
			CreateMap<SprintViewModel, Sprint>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId))
				.ForMember(dest => dest.Project, opts => opts.Ignore())
				.ForMember(dest => dest.Tasks, opts => opts.Ignore());
		}
	}
}
