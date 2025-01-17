using System;
using System.Collections.Generic;

namespace Gladiator_Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Willkommen bei den Gladiatoren-Spielen! ===\n");

            // Erzeuge ein paar Gladiatoren (Name, Element, Level, Leben, Attack, Defense, CritChance, DodgeChance)
            Gladiator g1 = new(
                name: "Maximus",
                art: ElementArt.Feuer,
                level: 3,
                leben: 100,
                attack: 12,
                defense: 5,
                critChance: 0.15,    // 15%
                dodgeChance: 0.05,   // 5%
                healPotions: 2       // 2 Heiltränke
            );

            Gladiator g2 = new(
                name: "Aquaman",
                art: ElementArt.Wasser,
                level: 3,
                leben: 100,
                attack: 10,
                defense: 7,
                critChance: 0.10,    // 10%
                dodgeChance: 0.08,   // 8%
                healPotions: 2
            );

            // BeispielAttacke (Name, Kategorie, BasisSchaden)
            Attack feuerball = new("Feuerball", ElementArt.Feuer, 10);
            Attack wasserpeitsche = new("Wasserpeitsche", ElementArt.Wasser, 9);
            Attack neutralerSchlag = new("Neutraler Schlag", ElementArt.Neutral, 7);

            // Wir legen sie hier in eine Liste, falls du mehr Attacke haben willst.
            List<Attack> alleAttacke = [feuerball, wasserpeitsche, neutralerSchlag];

            // Starte den Kampf:
            Battle battle = new();
            battle.Fight(g1, g2, alleAttacke);

            Console.WriteLine("\n=== Programmende. Drücke eine Taste zum Beenden. ===");
            Console.ReadKey();
        }
    }
}