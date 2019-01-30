using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;

namespace ProSMan.Backend.API.Profiles
{
    public class TaskProfile : Profile
    {
		public TaskProfile()
		{
			CreateMap<Task, TaskViewModel>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(dest => dest.ActualSpentTime, opts => opts.MapFrom(src => src.ActualSpentTime))
				.ForMember(dest => dest.TimeEstimate, opts => opts.MapFrom(src => src.TimeEstimate))
				.ForMember(dest => dest.Priority, opts => opts.MapFrom(src => (ProSMan.Backend.Domain.ViewModels.Priority) src.Priority))
				.ForMember(dest => dest.IsFinished, opts => opts.MapFrom(src => src.IsFinished))
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.Project.Id))
				.ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id))
				.ForMember(dest => dest.SprintId, opts => opts.MapFrom(src => src.Sprint.Id))
				.ReverseMap();
		}
	}
}
