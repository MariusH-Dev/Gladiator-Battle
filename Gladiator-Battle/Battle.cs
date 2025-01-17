using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Battle
{
    public class Battle
    {
        private Random rng = new Random();

        /// <summary>
        /// Führt einen rundenbasierten Kampf zwischen zwei Gladiatoren durch.
        /// Nutzt eine kleine Menülogik, um pro Runde Aktionen zu wählen (Attack, Heilen, Verteidigung ...).
        /// </summary>
        /// <param name="g1">Gladiator 1</param>
        /// <param name="g2">Gladiator 2</param>
        /// <param name="availableAttacks">Liste aller möglichen Attacke, aus denen man wählen kann</param>
        public void Fight(Gladiator g1, Gladiator g2, List<Attack> availableAttacks)
        {
            Console.WriteLine($"=== Kampf beginnt: {g1.Name} vs {g2.Name} ===");
            Console.WriteLine($"--- {g1}\n--- {g2}\n");

            // Runden abwechselnd, bis einer down ist
            Gladiator current = g1;
            Gladiator opponent = g2;

            while (!g1.IsDown && !g2.IsDown)
            {
                Console.WriteLine($"\n--- {current.Name} ist am Zug ---");
                PrintGladiatorStatus(g1, g2);

                // Zeige Menü
                Console.WriteLine("Wähle eine Aktion:");
                Console.WriteLine(" (A) Angreifen");
                Console.WriteLine(" (H) Heiltrank benutzen");
                Console.WriteLine(" (S) Status anzeigen");
                Console.WriteLine(" (Q) Kampf beenden (Aufgeben)");

                string input = Console.ReadLine().Trim().ToUpper();

                switch (input)
                {
                    case "A":
                        // Greife an: Wähle einen Attack aus der Liste
                        Attack chosenAttack = ChooseAttack(availableAttacks, current);
                        current.AttackTarget(chosenAttack, opponent);
                        break;
                    case "H":
                        current.UseHealPotion();
                        break;
                    case "S":
                        PrintGladiatorStatus(g1, g2);
                        // Gleiche Runde nochmal Aktion wählen
                        continue;
                    case "Q":
                        Console.WriteLine($"{current.Name} gibt auf!");
                        current.ReceiveDamage(current.Leben); // Im Prinzip = KO
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe. Bitte erneut versuchen.");
                        continue; // Aktion nochmal wählen
                }

                // Nach Aktion: prüfen, ob Gegner down ist
                if (opponent.IsDown) break;

                // Nächster Zug: Rollen tauschen
                (current, opponent) = (opponent, current);
            }

            // Kampfende
            Gladiator winner = null;
            Gladiator loser = null;
            if (g1.IsDown && !g2.IsDown)
            {
                winner = g2; loser = g1;
            }
            else if (g2.IsDown && !g1.IsDown)
            {
                winner = g1; loser = g2;
            }
            else
            {
                Console.WriteLine("Beide sind KO? Unentschieden!");
                PrintGladiatorStatus(g1, g2);
                return;
            }

            Console.WriteLine($"\n=== {winner.Name} hat gewonnen! ===");
            PrintGladiatorStatus(g1, g2);

            // Sieger erhält XP
            int xpGain = CalculateXPGain(winner, loser);
            winner.AddXP(xpGain);
        }

        /// <summary>
        /// Erlaubt dem Spieler, aus einer Liste einen Attack auszuwählen.
        /// </summary>
        private Attack ChooseAttack(List<Attack> attacks, Gladiator attacker)
        {
            Console.WriteLine("\nVerfügbare Attacke:");
            for (int i = 0; i < attacks.Count; i++)
            {
                Console.WriteLine($"  {i + 1}) {attacks[i].Name} (BasisSchaden {attacks[i].BasisSchaden}, Element {attacks[i].Kategorie})");
            }
            Console.WriteLine("Bitte Zahlen-ID eingeben:");

            int index;
            while (true)
            {
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out index))
                {
                    index--; // 0-basiert
                    if (index >= 0 && index < attacks.Count)
                    {
                        return attacks[index];
                    }
                }
                Console.WriteLine("Ungültige Auswahl, bitte erneut eingeben.");
            }
        }

        /// <summary>
        /// Zeigt den aktuellen Status beider Gladiatoren an.
        /// </summary>
        private void PrintGladiatorStatus(Gladiator g1, Gladiator g2)
        {
            Console.WriteLine($"\n--- Status ---");
            Console.WriteLine(g1.ToString());
            Console.WriteLine(g2.ToString());
            Console.WriteLine();
        }

        /// <summary>
        /// Bestimmt, wieviel XP der Gewinner bekommt.
        /// Einfaches Beispiel (u.a. abhängig vom Levelunterschied).
        /// </summary>
        private int CalculateXPGain(Gladiator winner, Gladiator loser)
        {
            int levelDiff = Math.Max(1, Math.Abs(winner.Level - loser.Level));
            // Grund-Formel: 50 + 10 * levelDiff
            int xp = 50 + 10 * levelDiff;
            return xp;
        }
    }
}
