﻿using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface IProjectService
    {
		IProject GetItemById(Guid id);
		List<IProject> GetListByUsername(string username);
		List<IProject> GetListById(Guid id);
		bool Add(IProject model, IUser user);
		bool Update(IProject model);
		bool Delete(IProject model);
	}
}
