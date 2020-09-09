namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class SortingParameter<T>
    {
        public T SortingKey { get; set; }
        public bool IsDescending { get; set; }
    }
}