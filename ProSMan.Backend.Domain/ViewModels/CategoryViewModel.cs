using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class CategoryViewModel
    {
		public Guid Id { get; set; }
		public string Name { get; set; }

		public Guid? ProjectId { get; set; }
	}
}
