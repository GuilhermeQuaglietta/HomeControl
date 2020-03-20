namespace HomeControl.Core.Infrastructure.Repository
{
    public interface IEntity
    {
        int Id { get; }
        int OwnerId { get; }
    }
}
