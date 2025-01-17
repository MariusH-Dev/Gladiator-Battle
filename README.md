# Gladiator Console Game

Willkommen zu den **Gladiatoren-Spielen**! Dieses kleine Konsolenprojekt dient als Demonstration für ein einfaches, aber erweiterbares Kampfsystem in C#. Dabei stehen zwei (oder mehr) Gladiatoren mit unterschiedlichen Eigenschaften gegeneinander im Kampf. Verschiedene Aktionen wie **Angriff**, **Heilen**, **Ausweichen**, **kritische Treffer** sowie **elementare Stärken/Schwächen** machen die Schlachten interessanter. Zusätzlich verfügt das Spiel über ein Level- und XP-System.

---

## Inhaltsverzeichnis

- [Überblick](#überblick)
- [Funktionen & Features](#funktionen--features)
- [Installation & Ausführung](#installation--ausführung)
- [Spielablauf](#spielablauf)
- [Erweiterungsmöglichkeiten](#erweiterungsmöglichkeiten)
- [Lizenz](#lizenz)

---

## Überblick

Dieses Projekt besteht aus mehreren Klassen, die jeweils eine bestimmte Aufgabe übernehmen:

- **Gladiator**  
  Repräsentiert einen Kämpfer mit Attributen wie `Level`, `Leben`, `Attack`, `Defense`, `CritChance`, `DodgeChance` und beinhaltet Methoden zum Erleiden von Schaden, Heilen, Level-Up etc.

- **Angriff**  
  Definiert einen **Attack Move**. Enthält einen Basis-Schaden und einen **ElementArt**-Typ (Feuer, Wasser, Neutral). Beim Ausführen eines Angriffs wird der Grundschaden auf Basis von Level-Unterschieden, Element-Effekten und Angriffs-/Verteidigungswerten berechnet.

- **ElementArt**  
  Enum, um die verschiedenen Kampf-Elemente zu verwalten: `Neutral`, `Feuer`, `Wasser`.

- **Battle**  
  Enthält die **rundenbasierte Logik**. Dort wird abgefragt, welche Aktion ein Gladiator in seinem Zug ausführen will (Angriff, Heiltrank benutzen, Status abfragen, ggf. Aufgeben). Der Sieger erhält XP, mit denen er sein Level steigern kann.

- **Program (Main)**  
  Einstiegspunkt des Programms. Erzeugt die Gladiatoren und Angriffe, startet dann den Kampf via `Battle.Fight(...)`.

---

## Funktionen & Features

1. **Rundenbasiertes Kampfsystem**  
   - Zwei Gladiatoren treten abwechselnd an.  
   - Jeder kann im eigenen Zug eine Aktion wählen:
     - **(A) Attack**: Angriff aus einer Liste vorhandener Moves.
     - **(H) Heal**: Heiltrank einsetzen, falls verfügbar.
     - **(S) Status**: Zwischenstand beider Kämpfer anzeigen.
     - **(Q) Quit**: Aufgeben (Kampf wird beendet).

2. **Element-Effekte**  
   - **Feuer** vs. **Wasser** → doppelter Schaden.  
   - Weitere Kombos (frei erweiterbar): z. B. Feuer vs. Neutral, Wasser vs. Neutral etc.

3. **Zufallsfaktoren**  
   - **Kritische Treffer**: `CritChance` (z. B. 15 % → doppelter Schaden).  
   - **DodgeChance**: Ausweichen (z. B. 5 %).  
   - **±20% Variationsfaktor** auf den Basisschaden.

4. **Heiltränke**  
   - Jeder Gladiator kann eine bestimmte Anzahl **Heiltränke** besitzen, die pro Einsatz z. B. +20 HP bringen.

5. **Level- und XP-System**  
   - Gewinner erhält **XP**.  
   - Beim Erreichen einer bestimmten XP-Schwelle: **Level Up** → steigert `Level`, `Attack`, `Defense`, `Leben`.

6. **Ausbaufähige Klassenstruktur**  
   - Klar getrennte Klassen **Gladiator**, **Angriff**, **Battle**.  
   - Einfache Anpassung/Erweiterung möglich (Inventar, Status-Effekte, Teamkämpfe, Turniermodus usw.).

---


**Kurzbeschreibung der Dateien**:

- **Program.cs**  
  Initialisiert zwei oder mehr Gladiatoren, definiert Angriffe und ruft die `Battle.Fight()`-Methode auf.

- **ElementArt.cs**  
  Enum für die Elemente (z. B. `Neutral`, `Feuer`, `Wasser`). So vermeidet man String-Vergleiche.

- **Angriff.cs**  
  - `Name`  
  - `Kategorie` (vom Typ `ElementArt`)  
  - `BasisSchaden`  
  - Methode `ComputeBaseDamage(...)` zur Berechnung des (noch **nicht** finalen) Schadens basierend auf dem Angreifer, Levelunterschieden, Element-Kombinationen.

- **Gladiator.cs**  
  - Wichtige Felder: `Leben`, `Level`, `Attack`, `Defense`, `CritChance`, `DodgeChance`, `HealPotions`, `XP`.  
  - Methoden: 
    - `ReceiveDamage(int)`, 
    - `DoDamageCalculation(int baseDamage, Gladiator defender)` (verarbeitet Crit, Dodge, Zufallsfaktor), 
    - `AttackTarget(Angriff, Gladiator)`, 
    - `UseHealPotion()`, 
    - `AddXP(int)`, 
    - `CheckLevelUp()` usw.

- **Battle.cs**  
  - Stellt den rundenbasierten Ablauf bereit (`Fight(…)`)  
  - Bietet ein kleines **Konsolenmenü** für Aktionen (Attack, Heal, Status, Quit).  
  - Wählt zwischen den beiden Gladiatoren abwechselnd den Angreifer, prüft, ob ein Gladiator `IsDown` ist, vergibt am Ende XP an den Sieger.

---

## Installation & Ausführung

1. **Clone / Download** dieses Repositories:
   ```bash
   git clone https://github.com/<YourUsername>/OOP_Exp_01_Gladiator.git
2. **Öffne das Projekt in Visual Studio oder einer IDE deiner Wahl.**
3. **Kompiliere das Projekt (Build).**
4. **Starte die Anwendung. Es öffnet sich eine Konsole, in der der Kampf abläuft.**

---

## Spielablauf

1. **Begrüßung**  
   „Willkommen bei den Gladiatoren-Spielen“

2. **Statusausgabe**  
   Das Programm zeigt den Status der Gladiatoren (Name, Level, HP, Attack, Defense, etc.) an.

3. **Rundenbasierte Aktionen**  
   In einer Schleife wird der aktuelle Gladiator nach seiner Aktion gefragt:
   - **(A) Angreifen**  
     Man wählt aus einer Liste von Angriffen (z. B. Feuerball, Wasserpeitsche, Neutraler Schlag).  
     Anschließend wird der Schaden berechnet → Der andere Gladiator verliert HP.
   - **(H) Heiltrank**  
     Der Gladiator regeneriert z. B. +20 HP, sofern noch Tränke im Besitz sind.
   - **(S) Status**  
     Gibt den aktuellen Status (HP, Level, etc.) aller Gladiatoren erneut aus.
   - **(Q) Quit**  
     Der aktuelle Gladiator gibt auf und ist sofort KO.

4. **Wechsel des Angreifers/Verteidigers**  
   Nach jedem Zug wechseln sich die Gladiatoren ab, bis einer von beiden keine HP mehr besitzt (IsDown == true).

5. **Kampfausgang**  
   Sobald ein Gladiator besiegt ist, erhält der Gewinner XP und kann unter Umständen ein **LevelUp** erreichen.

6. **Ende**  
   Anschließend kann das Programm beendet oder ein neuer Kampf gestartet werden.

---

## Erweiterungsmöglichkeiten

- **Teamkämpfe (2 vs. 2)** oder größere Runden
- **Turniere** bzw. mehrere Begegnungen in Folge
- **Spezialfähigkeiten** (Betäubung, Gift, Feuerresistenz, etc.)
- **Inventar-System** (weitere Gegenstände, Waffen, Rüstungen)
- **Speichern/Laden** der Gladiatoren (JSON, XML)
- **KI-Gegner** (automatische Zug-Entscheidungen)
- **Verfeinerte Level-Up-Mechanik** (manuelles Verteilen von Attributen)

---

## Lizenz

Dieses Projekt steht unter der [MIT License](https://opensource.org/licenses/MIT).  
Das bedeutet:  
- Der Code darf kopiert, modifiziert und verteilt werden,  
- sofern ein Hinweis auf den ursprünglichen Autor enthalten bleibt.  

Ich freue mich über eine Erwähnung im Quelltext oder in den Danksagungen!

