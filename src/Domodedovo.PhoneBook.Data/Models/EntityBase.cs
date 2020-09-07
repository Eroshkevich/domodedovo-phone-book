namespace Domodedovo.PhoneBook.Data.Models
{
    public abstract class EntityBase<T> : IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<int>
    {
    }
}