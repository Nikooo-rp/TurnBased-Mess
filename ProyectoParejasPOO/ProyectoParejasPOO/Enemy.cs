using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Enemy: Character
    {
        string chosenAction = string.Empty;
        int expOnDeath;
        int hpScaleFactor;
        int atkScaleFactor;
        int level;
        List<CharacterAction> actions = new List<CharacterAction>();

        public Enemy(string name, int level) : base(name)
        {
            this.name = name;
            this.level = level;

            actions.Add(CharacterAction.Atacar);
            ScaleStats();
        }
        public override CharacterAction ChooseAction()
        {
            Random ran = new Random();
            int index = ran.Next(actions.Count);

            return actions[index];

        }

        public void ScaleStats()
        {
            maxHP += hpScaleFactor * level;
            hp = maxHP;
            atk += atkScaleFactor * level;
            defense += defense * level;
        }
    }
}
