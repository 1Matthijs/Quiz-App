# Coding Conventions


# 1. Algemene Code Stijl
## 1.1 Indentatie
Gebruik 4 spaties voor elke indentatie in de code.
Vermijd het gebruik van tabs.
## 1.2 Haakjes
Gebruik blokhaakjes {} bij elke control flow (if, else, for, while), zelfs als deze slechts één regel bevat.


```
if (condition)
{
    // Code
}
```
# 2. Naamgevingsconventies
## 2.1 Klassen en Methodes
Klassen worden geschreven in PascalCase.

Methodes worden geschreven in PascalCase.

```
public class MyClass
{
    public void MyMethod()
    {
        // Code
    }
}
```

## 2.2 Variabelen en Parameters
Lokale variabelen en parameters worden geschreven in camelCase.

Gebruik betekenisvolle namen die de bedoeling van de variabele duidelijk maken.

csharp
Copy code
int itemCount;
string firstName;
## 2.3 Interfaces
Interface namen beginnen met een I en volgen PascalCase.

```
public interface IRepository
{
    // Interface members
}
```

# 3. Toegankelijkheidsniveaus
Geef expliciet het toegankelijkheidsniveau (public, private, protected, internal) aan voor alle klassen, methoden en velden.

```
public class MyClass
{
    private int itemCount;
    
    public void MyMethod()
    {
        // Code
    }
}
```
# 4. Documentatie
## 4.1 XML Commentaar
Gebruik commentaar voor alle publieke klassen en methodes.

```

/// Dit is een voorbeeldmethode.

public void MyMethod()
{
    // Code
}

```
## 4.2 Markdown Documentatie
Markdown-documenten (.md) zoals README.md en deze CONVENTIONS.md moeten duidelijke kopjes, lijsten en codeblokken bevatten.
# 5. Error Handling
## 5.1 Exceptions
Gebruik exceptions voor foutafhandeling in plaats van foutcodes.

Zorg ervoor dat exceptions duidelijke en betekenisvolle berichten bevatten.

```
throw new ArgumentException("Parameter cannot be null", nameof(parameter));
```
# 6. Best Practices
## 6.1 Asynchroon programmeren
Gebruik async/await patronen waar mogelijk voor IO-bound en langlopende operaties.
```
public async Task MyMethodAsync()
{
    await Task.Delay(1000);
}
```
## 6.2 Dependency Injection
Volg het Dependency Injection patroon voor het beheren van afhankelijkheden binnen je applicatie.
Gebruik de ingebouwde .NET DI container of een alternatief zoals Autofac indien nodig.
# 7. Versiebeheer en Git
# 7.1 Commit Berichten
Schrijf korte en beschrijvende commit berichten.

Gebruik Engelse taal en de tegenwoordige tijd (present tense).

plaintext
Copy code
Add logging for user authentication
Fix issue with date parsing
7.2 Branch Naming
Gebruik feature-, bugfix-, hotfix- en release- prefixes gevolgd door een korte beschrijving.

plaintext
Copy code
feature/user-authentication
bugfix/date-parsing-error


