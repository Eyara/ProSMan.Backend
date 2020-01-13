using System;
namespace ProSMan.Backend.Core.Exceptions
{
	public class ActiveSprintAlreadyExistsException : Exception
	{
		public ActiveSprintAlreadyExistsException(string message) : base(message) { }
	}
}
