﻿using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Projects
{
	public class GetProjectsByIdHandler : IRequestHandler<GetProjectsByIdQuery, ListRequest<ProjectViewModel>>
	{
		private IProjectService _projectService { get; set; }

		public GetProjectsByIdHandler(
			IProjectService projectService)
		{
			_projectService = projectService;
		}

		public async Task<ListRequest<ProjectViewModel>> Handle(GetProjectsByIdQuery request,
			CancellationToken cancellationToken)
		{
			var result = _projectService
				.GetListById(request.Id);
			return new ListRequest<ProjectViewModel>(result
				.ConvertAll(x => x as ProjectViewModel));
		}
	}
}