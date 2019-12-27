using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface ICategoryService
    {
		ICategory GetItemById(Guid id);
		List<ICategory> GetListByProjectId(Guid id);
		bool Add(ICategory model);
		bool Update(ICategory model);
		bool Delete(ICategory model);
		void DeleteByProjectId(Guid id);
	}
}
