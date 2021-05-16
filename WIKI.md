# Algorytm rozwiązywania problemów CSP

[1. Opis](home#1-opis)

[2. Zródła](home#2-%C5%B9r%C3%B3d%C5%82a)

[3. Przykładowe dane](home#3-przyk%C5%82adowe-dane)

[4. Opis wywołania](home#4-opis-wywo%C5%82ania)

[5. Uruchomienie kontenera z usługą](home#5-uruchomienie-kontenera-z-us%C5%82ug%C4%85)

[6. Linki](home#6-linki)

## 1. Opis

### Opis problemu
Każdy zespół w grze World of Warcraft chcący uczestniczyć w elemencie gry o nazwie Mythic Dungeon("Mityczny Loch")
musi spełniać następujące ograniczenia:
1. Musi być złożony z dokładnie 5 graczy.
2. Musi zawierać 1 gracza pełniącego rolę Tank, 1 gracza pełniącego rolę Healer, oraz 3 graczy pełniących rolę DPS.

Role w grze World of Warcraft wzajemnie się wykluczają, tzn. gracz może pełnić tylko jedną rolę w danym momencie.
Rola jest również predeterminowana przez klasę oraz specjalizację postaci gracza. Aktualnie w grze znajduje się
12 klas postaci podzielonych na w sumie 36 specjalizacji(pewnego rodzaju podklas). Przykładowo: gracz gra postacią klasy
Hunter(Łowca), specjalizacja: Marksmanship(Celność). Rola takiej postaci to DPS.

Problem, który rozwiązuje mój program polega na tym, żeby podając jedną konkretną klasę i specjalizację, dostać jako
wynik wszystkie możliwe sensowne(spełniające podane ograniczenia) kombinacje 5-osobowych zespołów. Następnie 
korzystając z zewnętrznych danych pochodzących z symulacji, program jest w stanie obliczyć potencjalną siłę wszystkich 
możliwych zespołów, uszeregować je, i wyświetlić użytkownikowi najbardziej interesujące propozycje z góry rankingu.

Jeśli przeszukiwałbym wszystkie możliwe kombinacje metodą brute-force, wykonanie algorytmu trwałoby bardzo długo. 
Postanowiłem zatem zastosować pewne sposoby optymalizacji. Szczegóły implementacji znajdują się w następnym
akapicie.

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

[1] M. Antkiewicz, M. Piasecki, Opracowanie materiałów do laboratorium i wykładów ze Sztucznej Inteligencji, 2021.\
[2] S. Russel, P. Novig, "Artificial Intelligence: A Modern Approach", Fourth edition, 2020.\
[3] C. Lecoutre, "Constraint Networks: Techniques and Algorithms.", 2013

## 3. Przykładowe dane*


```json
{
  "dungeonName": "Sanguine Depths",
  "characterClass": "Hunter",
  "characterSpec": "Marksmanship"
}
```

dungeonName - nazwa lochu, dla którego chcemy znaleźć optymalny zespół \
characterClass - nazwa klasy postaci użytkownika \
characterSpec - nazwa specjalizacji klasy postaci użytkownika

### *sprostowanie
Usługa realizująca algorytm CSP zawiera również mechanizm komunikacji z innymi, zewnętrznymi interfejsami REST 
z których pobiera dane, a następnie na wykonuje na nich funkcję oceny i wybiera najlepiej ocenionych osobników.
Jest to mechanizm niezależny, który dla lepszego działania wymaga dodatkowych danych wejściowych. Stąd - dodatkowa wartość
"dungeonName".

## 4. Opis wywołania

### Bezpośrednie wywołanie wysyłając dane w formacie JSON

Po uruchomieniu usługi można sprawdzić jej działanie korzystając np. z programu Postman.

Ścieżka do algorytmu: /api/Teams

<img src="file:///data/images/postman_post_request_test.png" width="640"/>

Algorytm zwraca obiekt z elementami:

raiderioAnalysysResult - obiekt zawierający wynik działania mechanizmu dodatkowego, wyciągającego wnioski prosto z danych
zewnętrznych.\
raiderioAnalysysResult.dungeonName - nazwa lochu, dla którego została przeprowadzona analiza.\
raiderioAnalysysResult.teamMembers - lista zawierająca klasy i specjalizacje postaci w zespole wynikowym.\
raidbotsAnalysysResult - obiekt zawierający wynik działania algorytmu CSP, a właściwie wynik skrócony zawierający
10 najlepszych składów pod kątem możliwych zadawanych obrażeń w jeden cel(punkt 1. wiki).\
raidbotsAnalysysResult.teamMembers - lista zawierająca klasy i specjalizacje postaci w zespole wynikowym.\
raidbotsAnalysysResult.score - wartość liczbowa sumy możliwych zadawanych obrażeń przez zespół


### Wywołanie przy użyciu aplikacji klienckiej

Usługę można również przetestować przy użyciu załączonej aplikacji klienckiej.

Ścieżka do aplikacji: \src\CLIENT&#8209;APP\release_build\MythicTeamOptimizerClient\MythicTeamOptimizerClient.exe

Aplikacja pobiera dane od użytkownika za pomocą przycisków, w ten sposób budując dane do wysłania w formacie JSON.
Po pobraniu wszystkich danych, wysyła zapytanie do interfejsu REST, a użytkownikowi wyświetla animację ładowania.
Po otrzymaniu wyniku z interfejsu, analizuje go i wyświetla w sposób graficzny.

Przykładowe użycie aplikacji klienckiej:

Na pierwszym ekranie możemy podać adres interfejsu REST:\
<img src="file:///data/images/client_url.png" width="640"/>

Po potwierdzeniu adresu możemy wybrać dungeon(loch) wybierając przycisk.\
<img src="file:///data/images/client_dungeons.png" width="640"/>

Następnie stajemy przed wyborem specjalizacji - aplikacja sama wydedukuje klasę postaci.\
<img src="file:///data/images/client_specs.png" width="640"/>

Po wybraniu specjalizacji musimy chwilę odczekać na wynik, zatem wyświetlony jest ekran ładowania.\
<img src="file:///data/images/client_loading.png" width="640"/>

Jeśli operacja przebiegła pomyślnie wyświetlane są wyniki.\
<img src="file:///data/images/client_results.png" width="640"/>

## 5. Uruchomienie kontenera z usługą
Korzystając z Docker CLI obraz może zostać uruchomiony przez użycie polecenia:

`docker run -p PORT_ZEW:80 zukerr/mythicteamoptimiser`

oraz bezpośrednio pobrany z repozytorium przy użyciu polecenia:

`docker pull zukerr/mythicteamoptimiser`

## 6. Linki

Obraz w repozytorium obrazów:\
https://hub.docker.com/r/zukerr/mythicteamoptimiser

Całość projektu w repozytorium na github.com:\
https://github.com/zukerr/mythic-team-optimizer-demo

Przykładowe dane znajdują się w folderze data/raw_json_examples.
