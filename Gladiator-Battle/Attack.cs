using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Battle
{
    public class Attack(string name, ElementArt kategorie, int basisSchaden)
    {
        public string Name { get; } = name;
        public ElementArt Kategorie { get; } = kategorie;
        public int BasisSchaden { get; } = basisSchaden;

        /// <summary>
        /// Berechnet den Grundschaden (ohne Crit, ohne Dodge), 
        /// z. B. anhand Levelunterschied, Angreifer.Attack, Verteidiger.Art usw.
        /// </summary>
        /// <param name="attacker">Der Angreifer</param>
        /// <param name="defender">Der Verteidiger</param>
        /// <returns>Grundschaden (vor Crit/Dodge/Variation)</returns>
        public int ComputeBaseDamage(Gladiator attacker, Gladiator defender)
        {
            // 1) Basis: Kombiniere Angreifer-Attackswert + diesen Attack
            // 2) Multiplikator je nach Levelunterschied
            int levelDiff = Math.Abs(attacker.Level - defender.Level);
            if (levelDiff == 0) levelDiff = 1;

            int damage = (attacker.Attack + BasisSchaden) * levelDiff;

            // 3) Elementbonus: Feuer -> Wasser = 2x, Wasser -> Feuer = 2x, 
            //    (Beispiel: Feuer->Neutral oder Wasser->Neutral kann man anpassen, 
            //     hier nur Standard 2x if Fire->Water)
            if (Kategorie == ElementArt.Wasser && defender.Art == ElementArt.Feuer)
            {
                damage = (int)(damage * 2.0);
            }
            else if (Kategorie == ElementArt.Feuer && defender.Art == ElementArt.Wasser)
            {
                damage = (int)(damage * 2.0);
            }

            // Du könntest noch mehr Sonderfälle hinzufügen (z. B. Feuer->Neutral = +50 %, etc.)

            return damage;
        }
    }
}
