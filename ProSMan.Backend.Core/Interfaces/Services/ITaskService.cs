using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface ITaskService
    {
		ITask GetItemById(Guid id);
		List<ITask> GetListBySprint(Guid id);
		List<ITask> GetListToday(string username);
		bool Add(ITask model);
		bool Update(ITask model);
		bool Delete(ITask model);
	}
}
