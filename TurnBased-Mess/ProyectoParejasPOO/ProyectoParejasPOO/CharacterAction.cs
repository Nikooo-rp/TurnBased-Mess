using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class CharacterAction
    {
        public string name;
        public int power, manaCost;
        public Character? user;
        public Character? target;
        // public string targetType; // "self", "enemy", "ally"

        public CharacterAction(string name, int power, int manaCost)
        {
            this.name = name;
            this.power = power;
            this.manaCost = manaCost;
        }


        public virtual void ChooseTarget()
        {
            List<Character> possibleTargets = new List<Character>();

            target = BattleUI.ChooseTarget(possibleTargets);
        }
        public void Execute(Character user, Character target)
        {
            target.TakeDamage(power);
        }

        public static CharacterAction Atacar = new CharacterAction("Atacar", 4, 0);
    }
}
