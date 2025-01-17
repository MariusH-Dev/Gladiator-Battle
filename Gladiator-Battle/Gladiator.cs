using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Battle
{
    public class Gladiator(string name, ElementArt art, int level, int leben,
                     int attack, int defense, double critChance, double dodgeChance, int healPotions)
    {
        // --- Eigenschaften / Felder ---
        public string Name { get; } = name;
        public ElementArt Art { get; } = art;
        public int Level { get; private set; } = level;
        public int Leben { get; private set; } = leben;
        public bool IsDown { get; private set; } = false;

        // Weitere Attribute
        public int Attack { get; private set; } = attack;
        public int Defense { get; private set; } = defense;

        // Chancen (zwischen 0.0 und 1.0)
        public double CritChance { get; private set; } = critChance;
        public double DodgeChance { get; private set; } = dodgeChance;

        // Heiltränke
        public int HealPotions { get; private set; } = healPotions;

        // XP / Level Up
        public int XP { get; private set; } = 0; // Initial 0
        // z.B. simple Formel: XPForNextLevel = 100 * Level
        private int XPforNextLevel => 100 * Level;

        private readonly Random rng = new();

        // --- Methoden ---

        /// <summary>
        /// Nimmt Schaden (bereits berechnet) und prüft, ob der Gladiator KO geht.
        /// </summary>
        public void ReceiveDamage(int damage)
        {
            if (IsDown) return; // Keine Wirkung, falls schon besiegt

            Leben = Math.Max(0, Leben - damage);
            if (Leben <= 0)
            {
                Leben = 0;
                IsDown = true;
                Console.WriteLine($"{Name} ist besiegt!");
            }
        }

        /// <summary>
        /// Standardmethoden, um den Schaden final zu berechnen und anzuwenden.
        /// Enthält Crit-/Dodge-Logik, random ±20% Variation, etc.
        /// </summary>
        /// <param name="baseDamage">Der aus Attack + Verteidigung + Element etc. abgeleitete Basisschaden</param>
        /// <param name="defender">Der Verteidiger</param>
        public void DoDamageCalculation(int baseDamage, Gladiator defender)
        {
            if (IsDown)
            {
                Console.WriteLine($"{Name} kann nicht angreifen, da er bereits besiegt ist!");
                return;
            }

            // Verteidiger kann ausweichen?
            double dodgeRoll = rng.NextDouble();
            if (dodgeRoll < defender.DodgeChance)
            {
                Console.WriteLine($"{defender.Name} weicht dem Attack aus!");
                return;
            }

            // Kleine +/-20% Variation
            double variationFactor = 1.0 + (rng.NextDouble() * 0.4 - 0.2); // -0.2 .. +0.2
            int variedDamage = (int)Math.Round(baseDamage * variationFactor);

            // Kritischer Treffer?
            double critRoll = rng.NextDouble();
            if (critRoll < CritChance)
            {
                variedDamage *= 2;
                Console.WriteLine("** Kritischer Treffer! **");
            }

            // Wende Schaden an
            defender.ReceiveDamage(variedDamage);
            Console.WriteLine($"{Name} verursacht {variedDamage} Schaden bei {defender.Name} (Basis {baseDamage}, Variation {variationFactor:F2}).");
        }

        /// <summary>
        /// Führt einen Attack mit dem angegebenen Attackstyp aus.
        /// </summary>
        public void AttackTarget(Attack Attack, Gladiator defender)
        {
            // Basisschaden vom Attack (abh. von Element + Levelunterschied + Attack + Defense)
            int baseDamage = Attack.ComputeBaseDamage(this, defender);

            // Defensive des Verteidigers einbeziehen:
            // z.B. Reduziere den berechneten Schaden um (defender.Defense / 2)
            // (Nur als Beispiel, kannst du anpassen.)
            int defenseEffect = defender.Defense / 2;
            baseDamage = Math.Max(0, baseDamage - defenseEffect);

            // Dann final im Angreifer den Schaden anwenden lassen (Crit, Dodge, Variation)
            DoDamageCalculation(baseDamage, defender);
        }

        /// <summary>
        /// Heilt den Gladiator mithilfe eines Heiltranks.
        /// </summary>
        public void UseHealPotion()
        {
            if (HealPotions <= 0)
            {
                Console.WriteLine($"{Name} hat keine Heiltränke mehr!");
                return;
            }
            if (IsDown)
            {
                Console.WriteLine($"{Name} ist bereits besiegt und kann sich nicht heilen!");
                return;
            }

            HealPotions--;
            int healAmount = 20; // Feste Heilung um 20
            Leben += healAmount;
            Console.WriteLine($"{Name} trinkt einen Heiltrank (+{healAmount} Leben). ({HealPotions} Tränke übrig)");
        }

        /// <summary>
        /// Simpler LevelUp-Mechanismus: Erhöhe Level, setze XP zurück, steigere Attribute.
        /// </summary>
        public void CheckLevelUp()
        {
            while (XP >= XPforNextLevel)
            {
                XP -= XPforNextLevel;
                Level++;
                // Erhöhe ein paar Attribute
                Attack += 2;
                Defense += 1;
                Leben += 10; // Max. Leben könnte man getrennt pflegen
                Console.WriteLine($"{Name} steigt auf Level {Level} auf! (Attack: {Attack}, Defense: {Defense}, Leben: {Leben})");
            }
        }

        /// <summary>
        /// XP-Gewinn nach gewonnener Schlacht (o.ä.)
        /// </summary>
        /// <param name="amount">Anzahl XP</param>
        public void AddXP(int amount)
        {
            XP += amount;
            Console.WriteLine($"{Name} erhält {amount} XP (Total: {XP}, Next: {XPforNextLevel}).");
            CheckLevelUp();
        }

        public override string ToString()
        {
            return $"{Name} [Lv {Level}] HP: {Leben} | ATK: {Attack} | DEF: {Defense} | Crit: {CritChance:P0} | Dodge: {DodgeChance:P0} | Potions: {HealPotions} | Down: {IsDown}";
        }
    }
}
