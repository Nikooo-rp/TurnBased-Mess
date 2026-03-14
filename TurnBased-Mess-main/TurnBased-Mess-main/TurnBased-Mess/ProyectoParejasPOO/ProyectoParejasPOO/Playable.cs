using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Playable: Character
    {
        CharacterAction? chosenAction;
        public int mana;
        public int maxMana;
        public bool isDefending = false;

        public Playable(string name, int maxHp, int atk, int spd, int exp, int defense, int maxMana, int level) : base(name)
        {
            this.name = name;
            this.maxHP = maxHp;
            this.hp = this.maxHP;
            this.atk = atk;
            this.spd = spd;
            this.exp = exp;
            this.defense = defense;
            this.maxMana = maxMana;
            this.mana = maxMana;
            this.level = level;

            this.actions.Add(new SingleTargetAttack());
        }

        public override CharacterAction ChooseAction() 
        {
            CharacterAction action = BattleUI.GetPlayerAction(this);
            action.user = this;
            return action;
        }
        public void GainExperience(int ammount)
        {
            exp += ammount;
            // Lógica para subir de nivel....
        }
        public void LevelUp()
        {
            level++;
            maxHP += 5;
            hp = maxHP;
            atk += level * 2;
            spd += 1;
            maxMana += 3;
            mana = maxMana;
            defense += 1;

        }
    }
}
