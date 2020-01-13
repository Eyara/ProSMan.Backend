namespace ProSMan.Backend.Core.Interfaces.Entities
{
	public interface IPagination
	{
		int CurrentPage { get; set; }
		int PageCount { get; set; }
	}
}
