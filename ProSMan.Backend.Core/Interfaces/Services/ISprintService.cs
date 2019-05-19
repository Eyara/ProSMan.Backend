using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface ISprintService
    {
		ISprint GetItemById(Guid id);
		List<ISprint> GetListById(Guid id);
		List<ISprint> GetListByProjectId(Guid id);
		List<ISprint> GetUnfinishedListByProjectId(Guid id);
		bool Add(ISprint model);
		bool Update(ISprint model);
		bool Finish(ISprint model);
		bool Delete(ISprint model);
	}
}
