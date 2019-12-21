using AutoMapper;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Model;
using System;

namespace ProSMan.Backend.API.Profiles
{
    public class TaskProfile : Profile
    {
		public TaskProfile()
		{
			CreateMap<Task, TaskViewModel>()
				.ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.Project.Id))
				.ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id))
				.ForMember(dest => dest.SprintId, opts => opts.MapFrom(src => src.Sprint.Id));

			CreateMap<TaskViewModel, Task>()
				.ForMember(dest => dest.Project, opts => opts.Ignore())
				.ForMember(dest => dest.Category, opts => opts.Ignore())
				.ForMember(dest => dest.Sprint, opts => opts.Ignore());

			CreateMap<Task, NonSprintTask>()
				.ForMember(dest => dest.Project, opts => opts.Ignore());
		}
	}
}
