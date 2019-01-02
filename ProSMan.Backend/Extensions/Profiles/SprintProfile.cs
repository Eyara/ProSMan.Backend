using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Profiles
{
	public class SprintProfile : Profile
	{
		public SprintProfile()
		{
			CreateMap<Sprint, SprintViewModel>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.FromDate, opts => opts.MapFrom(src => src.FromDate))
				.ForMember(dest => dest.ToDate, opts => opts.MapFrom(src => src.ToDate))
				.ForMember(dest => dest.IsFinished, opts => opts.MapFrom(src => src.IsFinished))
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.Project.Id))
				.ReverseMap();
		}
	}
}
