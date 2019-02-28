# ZPO_Project

Jak zrobić bazę danych?
-> dodaj lokalną bazę danych o nazwie 'ZPO_Projekt'
-> Package Manager Console w VS
> Add-Migration MigracjaInicjalizacyjna -ProjectName ZPO_Project.Data -StartupProject ZPO_Project
> Update-Database MigracjaInicjalizacyjna -ProjectName ZPO_Project.Data -StartupProject ZPO_Project
-> uruchomienie aplikacji (Ctrl + F5)
-> wejscie w Akcje localhost:xxx/Account/Seed
Więcej nie jest potrzebne.

Zastosowane wzorce projektowe:
- MVC (struktura projektu)
- Fabryka (ZPO_Projekt.Helpers.FoodFactory)
- Strategia
    - rozne opisy przy zamawianiu w zaleznosci od rodzaju jedzenia