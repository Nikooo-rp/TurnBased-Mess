using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public abstract class Character
    {
        public int hp, maxHP, atk, spd, exp, defense;
        public string name;
        public bool isAlive = true;
        // public List<CharacterAction> actions;

        public Character(string name)
        {
            this.name = name;
        }

        public void TakeDamage(int damage)
        {
            int damageTaken = Math.Max(0, damage - this.defense);
            this.hp = Math.Max(0, this.hp - damageTaken);
        }
        public abstract CharacterAction ChooseAction();
    }
}
