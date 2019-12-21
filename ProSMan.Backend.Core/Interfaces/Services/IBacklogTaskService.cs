using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface IBacklogTaskService
    {
		IBacklogTask GetItemById(Guid id);
		List<IBacklogTask> GetListByProject(Guid id);
		bool Add(IBacklogTask model);
		bool Update(IBacklogTask model);
		bool Delete(IBacklogTask model);
	}
}
