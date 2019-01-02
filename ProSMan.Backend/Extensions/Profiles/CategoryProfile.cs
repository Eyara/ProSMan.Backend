using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Profiles
{
    public class CategoryProfile : Profile
    {
		public CategoryProfile()
		{
			CreateMap<Category, CategoryViewModel>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.Project.Id))
				.ReverseMap();
		}
	}
}
