using Xunit;
using System.Collections.Generic;
using Contact_Manager.ContactManager;

namespace ContactManagerTests
{
    public class ContactManagerTests
    {
        public static IEnumerable<object[]> ContactData =>
            new List<object[]>
            {
                new object[] { new Contact("Mohammad Alhariri", "111-222-3333", ContactType.Family) },
                new object[] { new Contact("ali mhawich", "111-222-4444", ContactType.Family) },
                new object[] { new Contact("Moath Abu-Shanab", "111-222-5555", ContactType.Family) },
                new object[] { new Contact("Rawan Mostafa", "222-333-4444", ContactType.Friend) },
                new object[] { new Contact("omar zain", "222-333-5555", ContactType.Friend) },
                new object[] { new Contact("Frank Brown", "222-333-6666", ContactType.Friend) },
                new object[] { new Contact("Grace White", "333-444-5555", ContactType.Work) },
                new object[] { new Contact("Henry White", "333-444-6666", ContactType.Work) },
                new object[] { new Contact("Ivy White", "333-444-7777", ContactType.Work) }
            };

        [Theory]
        [MemberData(nameof(ContactData))]
        public void Test_AddAndDuplication(Contact contact)
        {
            // Act & Assert for Add
            object addOutput = ContactsManager.AddContact(contact);
            Assert.Equal(ContactsManager.ViewAllContacts(), addOutput);

            // Act & Assert for Duplication
            object duplicationOutput = ContactsManager.AddContact(contact);
            Assert.Equal("Contact is already exist", duplicationOutput);
        }

        [Fact]
        public void Test_Empty_Contact()
        {
            // Act 1
            object output = ContactsManager.AddContact(new Contact("", "", ContactType.Work));

            // Assert 1
            Assert.Equal("Name can't be empty.", output);

            // Act 2
            output = ContactsManager.AddContact(new Contact("", "00000", ContactType.Work));

            // Assert 2
            Assert.Equal("Name can't be empty.", output);

            // Act 3
            output = ContactsManager.AddContact(new Contact("Muath", "", ContactType.Work));

            // Assert 3
            Assert.Equal("PhoneNumber can't be empty.", output);
        }

        [Theory]
        [MemberData(nameof(ContactData))]
        public void Test_Remove(Contact contact)
        {
            // Act
            object removeOutput = ContactsManager.RemoveContact(contact.Name);

            // Assert
            Assert.Equal(ContactsManager.ViewAllContacts(), removeOutput);
        }

        [Fact]
        public void Test_View_All()
        {
            // Act
            List<string> output = ContactsManager.ViewAllContacts();
            // Assert
            Assert.Equal(ContactsManager.ViewAllContacts(), output);
        }
    }
}
