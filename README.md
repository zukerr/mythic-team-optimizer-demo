# Mythic Team Optimizer - usługa implementująca algorytm rozwiązywania problemów CSP(ang. Constraint Satisfaction Problem)

## Opis

Projekt ten zawiera implementację algorytmu rozwiązywania problemów CSP, czyli problemów spełniania ograniczeń.
Algorytm ten został zaimplementowany w języku C#. Projekt ten zawiera również
implementację usługi webowej, która poprzez interfejs REST oferuje możliwość
uruchomienia algorytmu dla danych przesłanych w formacie JSON.
Niezależnie usługa obejmuje też prosty moduł komunikacji z zewnętrznymi danymi, na których realizuje funkcję oceny,
a następnie wybiera najlepszego osobnika.
Dodatkowo w skład projektu wchodzi mini-aplikacja kliencka z interfejsem graficznym, 
która na podstawie interakcji z użytkownikiem buduje zapytanie w formacie JSON i komunikuje się z interfejsem REST.

Celem tego projektu jest usprawnienie dobierania członków zespołu w grze online World of Warcraft.

Szczegółowy opis usługi można znaleźć w wiki tego projektu.

## Licencja

Niniejszy algorytm wraz z usługą zostały stworzone w ramach laboratorium z rozproszonych systemów informatycznych 
Wydziału Informatyki i Zarządzania Politechniki Wrocławskiej. 
Szczegóły licencji na jakiej ten projekt został udostępniony można znaleźć w pliku LICENSE(licencja nie dotyczy silnika Unity).
