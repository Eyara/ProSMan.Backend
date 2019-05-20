using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface INonSprintTaskService
    {
		INonSprintTask GetItemById(Guid id);
		List<INonSprintTask> GetListByProject(Guid id);
		List<INonSprintTask> GetBacklogListByProject(Guid id);
		bool Add(INonSprintTask model);
		bool Update(INonSprintTask model);
		bool Delete(INonSprintTask model);
	}
}
