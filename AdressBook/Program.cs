using AdressBook.System;

MainMenu mainMenu= new MainMenu();

mainMenu.Path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\content.json";

while (true)
{
    mainMenu.WelcomeMenu();
}
