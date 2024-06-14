using System;
using System.Collections.Generic;
using System.Linq;

namespace Contact_Manager.ContactManager
{
    public enum ContactType
    {
        Family,
        Friend,
        Work
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Contact> contactList = new List<Contact>
            {
                new Contact("Mohammad Alhariri", "111-222-3333", ContactType.Family),
                new Contact("ali mhawich", "111-222-4444", ContactType.Family),
                new Contact("Moath Abu-Shanab", "111-222-5555", ContactType.Family),
                new Contact("Rawan Mostafa", "222-333-4444", ContactType.Friend),
                new Contact("omar zain", "222-333-5555", ContactType.Friend),
                new Contact("Frank Brown", "222-333-6666", ContactType.Friend),
                new Contact("Grace White", "333-444-5555", ContactType.Work),
                new Contact("Henry White", "333-444-6666", ContactType.Work),
                new Contact("Ivy White", "333-444-7777", ContactType.Work)
            };

            foreach (var contact in contactList)
            {
                ContactsManager.AddContact(contact);
            }

            List<string> categoryContacts = ContactsManager.GetByCategory(ContactType.Family);
            foreach (var contact in categoryContacts)
            {
                Console.WriteLine(contact);
            }
        }
    }

    public static class ContactsManager
    {
        private static List<Contact> contacts = new List<Contact>();

        public static List<string> ViewAllContacts()
        {
            return contacts.Select(contact => contact.ToString()).ToList();
        }

        public static object AddContact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                return "Name can't be empty.";
            }
            else if (string.IsNullOrWhiteSpace(contact.PhoneNumber))
            {
                return "PhoneNumber can't be empty.";
            }

            if (Search(contact.Name, true) != "Contact not found" ||
                Search(contact.PhoneNumber, false) != "Contact not found")
            {
                return "Contact is already exist";
            }

            contacts.Add(contact);
            return ViewAllContacts();
        }

        public static List<string> RemoveContact(string name)
        {
            Contact contactToRemove = contacts.FirstOrDefault(contact =>
                contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
            }

            return ViewAllContacts();
        }

        public static string Search(string property, bool isNameProperty)
        {
            Contact contactToFind = contacts.FirstOrDefault(contact => {
                string contactProperty = isNameProperty ? contact.Name : contact.PhoneNumber;
                return contactProperty.Equals(property, StringComparison.OrdinalIgnoreCase);
            });

            return contactToFind != null ? contactToFind.ToString() : "Contact not found";
        }

        public static List<string> GetByCategory(ContactType category)
        {
            return contacts.Where(contact => contact.Type == category)
                           .Select(contact => contact.ToString())
                           .ToList();
        }
    }

    public class Contact
    {
        private string name;
        private string phoneNumber;
        private ContactType type;

        public Contact(string name, string phoneNumber, ContactType type)
        {
            this.name = name.Trim();
            this.phoneNumber = phoneNumber.Trim();
            this.type = type;
        }

        public string Name => name;
        public string PhoneNumber => phoneNumber;
        public ContactType Type => type;

        public override string ToString()
        {
            return $"Name: {name}, Phone number: {phoneNumber}, Type: {type}";
        }
    }
}
