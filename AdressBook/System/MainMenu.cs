using AdressBook.Models;
using Newtonsoft.Json;

namespace AdressBook.System;

internal class MainMenu
{
    private List<Contact> contacts = new List<Contact>();
    private FileService fileService = new FileService();
    public string Path { get; set; } = null!;
    public void WelcomeMenu()
    {
        PopulateContacts();

        Console.Clear();
        Console.WriteLine("Välkommen till Adressboken");
        Console.WriteLine("1. Skapa en kontakt");
        Console.WriteLine("2. Visa alla kontakter");
        Console.WriteLine("3. Visa en specifik kontakt");
        Console.WriteLine("4. Ta bort en specifik kontakt");
        Console.WriteLine("Välj ett av alternativen ovan: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1": CreateContact(); break;
            case "2": ShowAllContacts(); break;
            case "3": SearchContact(); break;
            case "4": RemoveContact(); break;
            default:
                Console.WriteLine("Inte ett ordentligt val, tryck valfri tangent för att återvända.");
                Console.ReadKey(); break;
        }
    }

    private void PopulateContacts()
    {
        try
        {
            var items = JsonConvert.DeserializeObject<List<Contact>>(fileService.Read(Path));
            if (items != null)

                contacts = items;
        }
        catch
        {
            Console.WriteLine("ERROR");
        }

    }

    private void CreateContact()
    {
        Contact contact = new Contact();

        Console.Clear();
        Console.WriteLine("Skapa en kontakt");
        Console.WriteLine("Förnamn: ");
        contact.FirstName = Console.ReadLine() ?? "";
        Console.WriteLine("Efternamn: ");
        contact.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("Email: ");
        contact.Email = Console.ReadLine() ?? "";
        Console.WriteLine("Adress: ");
        contact.Adress = Console.ReadLine() ?? "";
        Console.WriteLine("Telefon nummer: ");
        contact.Number = Console.ReadLine() ?? "";


        Console.WriteLine("Följande kontakt har skapats: ");
        Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
        Console.WriteLine($"Email: {contact.Email}");
        Console.WriteLine($"Adress: {contact.Adress}");
        Console.WriteLine($"Nummer: {contact.Number}");

        Console.WriteLine("Tryck valfri knapp för att återgå till menyn.");
        Console.ReadKey();

        contacts.Add(contact);
        fileService.Save(Path, JsonConvert.SerializeObject(contacts));
    }

    private void ShowAllContacts()
    {
        int nr = 0;
        //Load list against content.json on desktop
        if (contacts.Count > 0)
        {
            Console.WriteLine("Alla kontakter:");
            foreach (Contact contact in contacts)
            {
                nr++;
                Console.WriteLine("");
                Console.WriteLine($"{nr}. Namn: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Telefon: {contact.Number}");

            }
        } else
        {
            Console.WriteLine("Det finns inga kontakter här!");
            Console.ReadKey();
        }

        Console.WriteLine("Det var alla kontakter som finns, tryck valfri tangent för att återgå till huvudmenyn.");
        Console.ReadKey();
    }

    private void SearchContact()
    {
        //Search for a contact in list...
        if (contacts.Count == 0) 
        {
            Console.WriteLine("Det finns inga kontakter här än.");
            Console.ReadKey();
        }
        else 
        {
            int nr = 0;
            foreach (Contact contact in contacts)
            {
                nr++;
                Console.WriteLine("");
                Console.WriteLine($"Kontakt nr {nr}: {contact.FirstName} {contact.LastName}");
            }
            Console.WriteLine("Sök efter kontakt, enligt förnamn:");
            var SearchingName = Console.ReadLine();

            if (SearchingName != null && SearchingName != "")
            {
                var searchContact = contacts.FindAll(x => x.FirstName.Contains(SearchingName)).ToList();

                if (searchContact != null)
                {
                    Console.Clear();
                    Console.WriteLine("Dessa kontakter matchar din sökning: ");
                    foreach (var contact in searchContact)
                    {
                        Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
                        Console.WriteLine($"Email: {contact.Email}");
                        Console.WriteLine($"Telefonnummer: {contact.Number}");
                        Console.WriteLine($"Adress: {contact.Adress}");
                        Console.WriteLine("");
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Det finns ingen kontakt med detta namn: {SearchingName}, skrev du in rätt?");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Något gick snett, skrev du in ett namn?");
                Console.ReadKey();
            }
        }

    }


    private void RemoveContact()
    {
        //Search and remove a contact from list... 
        if (contacts.Count == 0)
        {
            Console.WriteLine("Det finns inga kontakter här än.");
            Console.ReadKey();
        }
        else
        {
            foreach (Contact contact in contacts)
            {
                Console.WriteLine("");
                Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Telefon nummer: {contact.Number}");
            }
            Console.WriteLine("Ta bort kontakt efter telefonnummer:");
            var removeNr = Console.ReadLine();

            if (removeNr != null && removeNr != "")
            {
                var removeContact = contacts.FirstOrDefault(x => x.Number == removeNr);


                if (removeContact != null)
                {
                    Console.Clear();
                    Console.WriteLine("Vill du ta bort denna kontakt?");
                    Console.WriteLine($"Adress: {removeContact.Id}");
                    Console.WriteLine($"Namn: {removeContact.FirstName} {removeContact.LastName}");
                    Console.WriteLine($"Email: {removeContact.Email}");
                    Console.WriteLine($"Telefon nummer: {removeContact.Number}");
                    Console.WriteLine($"Adress: {removeContact.Adress}");
                    Console.WriteLine("Skriv ''JA'' för att ta bort denna kontakt.");
                    var removeChoice = Console.ReadLine();

                    switch (removeChoice)
                    {
                        case "JA":
                            contacts.Remove(removeContact);
                            fileService.Save(Path, JsonConvert.SerializeObject(contacts));
                            Console.WriteLine("Kontakten har tagits bort!");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Kontakten har ej tagits bort!");
                            Console.ReadKey();
                            break;
                    }


                }
                else
                {
                    Console.WriteLine($"Det finns ingen kontakt med telefonnummret: {removeNr}, skrev du in rätt?");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Något gick snett, skrev du in ett telefonnummer?");
                Console.ReadKey();
            }
        }
    }



}
