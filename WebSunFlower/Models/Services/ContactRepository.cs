using WebSunFlower.Data;
using WebSunFlower.Models.Interfaces;

namespace WebSunFlower.Models.Services
{
    public class ContactRepository : IContactRepository
    {
        public SunFlowerDbContext _dbContext;

        public ContactRepository(SunFlowerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }
        public IEnumerable<EmailSubscribe> GetAllSubscriber()
        {
            return _dbContext.EmailSubscriptions.ToList();
        }

    }
}
