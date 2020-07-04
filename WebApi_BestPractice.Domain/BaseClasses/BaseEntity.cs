namespace WebApi_BestPractice.Domain.BaseClasses
{

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
