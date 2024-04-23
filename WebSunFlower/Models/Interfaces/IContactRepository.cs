namespace WebSunFlower.Models.Interfaces
{
    public interface IContactRepository
    {
        public void AddContact(Contact contact);
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<EmailSubscribe> GetAllSubscriber();
    }
}
