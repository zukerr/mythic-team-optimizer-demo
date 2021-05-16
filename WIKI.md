# Algorytm rozwiązywania problemów CSP

[1. Opis](home#1-opis)

[2. Zródła](home#2-%C5%B9r%C3%B3d%C5%82a)

[3. Przykładowe dane](home#3-przyk%C5%82adowe-dane)

[4. Opis wywołania](home#4-opis-wywo%C5%82ania)

[5. Opis wersji](home#5-opis-wersji)

[6. Uruchomienie kontenera z usługą](home#6-uruchomienie-kontenera-zus%C5%82ug%C4%85)

[7. Linki](home#7-linki)

## 1. Opis

### Opis problemu


### Opis metody rozwiązywania i algorytmu
Wybrana przeze mnie metoda rozwiązywania zadanego problemu CSP(ang. Constraint Satisfaction Problem) wykorzystuje 
algorytm przeszukiwania z nawrotami(ang. backtracking) oraz algorytm propagacji ograniczeń w postaci uproszczonego 
sprawdzania w przód(ang. forward checking). Sprawdzanie w przód bazuje na samodzielnej implementacji heurystyk wyboru
zmiennej oraz wartości. Algorytm łączący obie metody opiera się na tym, 
że dla wejścia w postaci danych o postaci użytkownika wykonywane są czynności:
1. Zamiana postaci gracza na reprezentację w postaci liczby całkowitej i dodanie jej do składu wynikowego zespołu.
2. Jeśli jakikolwiek członek wynikowego składu zespołu łamie którekolwiek ograniczenie,
 odrzuć rozwiązanie(w tym miejscu znajduje się również sprawdzanie w przód).
3. Jeśli zespół wynikowy ma odpowiednią wielkość(5 graczy) i nie łamie żadnych ograniczeń,
 dodaj go do listy finalnych rozwiązań.
4. Skopiuj zawartość aktualnego zespołu wynikowego do listy tymczasowej 's' i dodaj do niego reprezentację
 postaci gracza o indeksie 0(pierwsza wartość ze zbioru danych o wszystkich możliwych postaciach).
 Jeśli aktualny zespół wynikowy jest już pełny, zamiast tego podstaw pod 's' listę pustą.
5. Jeśli lista tymczasowa 's' nie jest pusta, to przejdź do kroku 2. dla listy 's'. Następnie zwiększ o jeden 
wartość reprezentacji ostatniej na liście postaci gracza.

## 2. Źródła

## 3. Przykładowe dane*

### Format JSON

### *sprostowanie
Usługa realizująca algorytm CSP zawiera również mechanizm komunikacji z innymi, zewnętrznymi interfejsami REST 
z których pobiera dane, a następnie na wykonuje na nich funkcję oceny i wybiera najlepiej ocenionych osobników.
Jest to mechanizm niezależny, który dla lepszego działania wymaga dodatkowych danych wejściowych. Stąd - dodatkowa wartość
"dungeonName".