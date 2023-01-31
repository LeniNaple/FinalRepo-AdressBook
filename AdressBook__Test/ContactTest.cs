using AdressBook.System;
using AdressBook.Models;

namespace AdressBook__Test;

[TestClass]
public class ContactTest
{
    [TestMethod]
    public void Should_Add_Contact_To_List()
    {
        // Arrange
        MainMenu mainMenu = new MainMenu();
        Contact contact = new Contact();

        // Act
        mainMenu.contacts.Add(contact);

        // Assert
        Assert.AreEqual(1, mainMenu.contacts.Count);


    }
}

