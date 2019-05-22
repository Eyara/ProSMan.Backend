using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class CategoryViewModel : ICategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool IsDeleted { get; set; }

		public Guid ProjectId { get; set; }
	}
}
