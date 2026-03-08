using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Enemy : Character
    {
        public string chosenAction = string.Empty;
        public int expOnDeath;
        public int hpScaleFactor;
        public int atkScaleFactor;
        public int level;
        public List<CharacterAction> actions = new List<CharacterAction>();

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
    public class Slime : Enemy
    {
        public Slime(int level) : base("Slime", level)
        {
            hpScaleFactor = 3;
            atkScaleFactor = 1;
            expOnDeath = 10;

        }
    }
    public class Orc : Enemy
    {
        public Orc(int level) : base("Duende", level)
        {
            hpScaleFactor = 4;
            atkScaleFactor = 2;
            expOnDeath = 20;

        }
    }
    public class Troll : Enemy
    {
        public Troll(int level) : base("Orco", level)
        {
            hpScaleFactor = 10;
            atkScaleFactor = 3;
            expOnDeath = 30;

        }
    }
    public class Wizard : Enemy
    {
        public Wizard(int level) : base("Mago", level)
        {
            hpScaleFactor = 5;
            atkScaleFactor = 4;
            expOnDeath = 40;

        }
    }
    public class MotherSlime : Enemy
    {
        public MotherSlime(int level) : base("Madre Slime", level)
        {
            hpScaleFactor = 8;
            atkScaleFactor = 2;
            expOnDeath = 50;

        }
    }
    public class Giant : Enemy
    {
        public Giant(int level) : base("Gigante", level)
        {
            hpScaleFactor = 12;
            atkScaleFactor = 5;
            expOnDeath = 60;

        }

    }

    public class DarkWizard : Enemy
    {
        public DarkWizard(int level) : base("Mago Oscuro", level)
        {
            hpScaleFactor = 7;
            atkScaleFactor = 6;
            expOnDeath = 70;

        }
    }
}
