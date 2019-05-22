using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProSMan.Backend.API.Services
{
    public class ProjectService: IProjectService
    {
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public ProjectService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public IProject GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}

		public List<IProject> GetListByUsername(string username)
		{
			return GetList(x => !x.IsDeleted && x.User.UserName == username);
		}

		public List<IProject> GetListById(Guid id)
		{
			return GetList(x => !x.IsDeleted && x.Id == id);
		}

		public bool Add(IProject model, IUser user)
		{
			try
			{
				var castedUser = user as User;
				var project = _mapper.Map<Project>(model as ProjectViewModel);

				project.Id = Guid.NewGuid();
				project.User = castedUser;
				project.UserId = castedUser.Id;

				_dbContext.Projects.Add(project);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Update(IProject model)
		{
			try
			{
				var project = _mapper.Map<Project>(model as ProjectViewModel);
				_dbContext.Projects.Update(project);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Delete(IProject model)
		{
			try
			{
				var project = _mapper.Map<Project>(model as ProjectViewModel);
				_dbContext.Projects.Remove(project);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private List<IProject> GetList(Expression<Func<Project, Boolean>> predicate)
		{
			return _dbContext.Projects
				.Where(predicate)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.OfType<IProject>()
				.ToList();
		}

		private IProject GetItem(Expression<Func<Project, bool>> predicate)
		{
			return _dbContext.Projects
				.Where(predicate)
				.ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
