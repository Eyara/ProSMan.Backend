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
    public class CategoryService: ICategoryService
    {
		private ProSManContext _dbContext { get; set; }
		private readonly IMapper _mapper;

		public CategoryService(ProSManContext context,
			IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public ICategory GetItemById(Guid id)
		{
			return GetItem(x => x.Id == id);
		}
		
		public List<ICategory> GetListById(Guid id)
		{
			return GetOrderedList(x => !x.IsDeleted && x.Id == id);
		}

		public List<ICategory> GetListByProjectId(Guid id)
		{
			return GetOrderedList(x => !x.IsDeleted && x.ProjectId == id);
		}

		public bool Add(ICategory model)
		{
			try
			{
				var category = _mapper.Map<Category>(model as CategoryViewModel);

				category.Id = Guid.NewGuid();

				_dbContext.Categories.Add(category);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool Update(ICategory model)
		{
			try
			{
				var category = _mapper.Map<Category>(model as CategoryViewModel);
				_dbContext.Categories.Update(category);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		
		public bool Delete(ICategory model)
		{
			try
			{
				var category = _mapper.Map<Category>(model as CategoryViewModel);
				category.IsDeleted = true;

				_dbContext.Categories.Update(category);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private List<ICategory> GetOrderedList(Expression<Func<Category, Boolean>> predicate)
		{
			return _dbContext.Categories
				.Where(predicate)
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.OfType<ICategory>()
				.ToList();
		}

		private ICategory GetItem(Expression<Func<Category, Boolean>> predicate)
		{
			return _dbContext.Categories
				.Where(predicate)
				.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefault();
		}
	}
}
