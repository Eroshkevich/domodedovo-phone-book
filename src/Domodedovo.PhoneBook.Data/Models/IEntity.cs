namespace Domodedovo.PhoneBook.Data.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}