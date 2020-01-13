namespace ProSMan.Backend.Core
{
	public interface IEntityBase
	{
	}

	public interface IEntityBase<TKey> : IEntityBase
	{
		TKey Id { get; set; }
	}
}
