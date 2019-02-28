# ZPO_Project
Serwis do zamawiania posilku.
Aplikacja webowa w MVC 5
Zastosowany ORM: EntityFramework

Zastosowane wzorce projektowe:
- MVC (struktura projektu)
- Fabryka (ZPO_Projekt.Helpers.FoodFactory)
- Strategia 
    - rozne opisy przy zamawianiu w zaleznosci od rodzaju jedzenia
- Singleton (DebugLogger)
- Adapter (FoodAdapter)

Jak zrobić bazę danych?
-> dodaj lokalną bazę danych o nazwie 'ZPO_Projekt'
-> Package Manager Console w VS
> Add-Migration MigracjaInicjalizacyjna -ProjectName ZPO_Project.Data -StartupProject ZPO_Project
> Update-Database MigracjaInicjalizacyjna -ProjectName ZPO_Project.Data -StartupProject ZPO_Project
-> uruchomienie aplikacji (Ctrl + F5)
-> wejscie w Akcje localhost:xxx/Account/Seed
Więcej nie jest potrzebne.

Jak skorzystac z serwisu?
1. Zarejestruj sie
2. kliknij 'Order' lub wejdź w listę zamówień w prawym górnym rogu, zostaniesz przekierowany do tworzenia pierwszego zamowienia
3. Wybierz co chcesz zamowic, po czym kliknij 'Order'
4. Na liscie pojawia sie nowe zlozone zamowienie

Istnieje opcja:
- edycji hasla uzytkownika
- dodanie niestandardowego dania do menu
- zmiany rodzaju dostarczenia zamowienia