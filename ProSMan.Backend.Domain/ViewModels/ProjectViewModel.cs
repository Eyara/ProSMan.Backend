using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class ProjectViewModel : IProject
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
    }
}
