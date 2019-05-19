using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Profiles
{
    public class CategoryProfile : Profile
    {
		public CategoryProfile()
		{
			CreateMap<Category, CategoryViewModel>();
			CreateMap<CategoryViewModel, Category>()
				.ForMember(dest => dest.Project, opts => opts.Ignore())
				.ForMember(dest => dest.Tasks, opts => opts.Ignore());
		}

	}
}
