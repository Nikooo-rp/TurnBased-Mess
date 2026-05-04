using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Enemy : Character
    {
        public int expOnDeath;
        public int hpScaleFactor;
        public int attackScaleFactor;
        
        // Evento que se dispara al morir el enemigo, para notificar al GameManager y otorgar experiencia al jugador
        // Action<int> es un delegado que representa un método que recibe un int (la experiencia otorgada) y no devuelve nada. El GameManager se suscribirá a este evento para recibir la experiencia cuando el enemigo muera.
        public Action<int> OnDeath;

        public Enemy(string name, int level) : base(name)
        {
            this.name = name;
            this.level = level;

            actions.Add(new SingleTargetAttack());
        }
        public override CharacterAction ChooseAction()
        {
            Random ran = new Random();
            int index = ran.Next(actions.Count);

            CharacterAction action = actions[index];
            action.user = this;
            return action;

        }

        public void ScaleStats()
        {
            maxHealth += hpScaleFactor * level;
            health = maxHealth;
            atk += attackScaleFactor * level;
            defense += Math.Min(0, level - 2);
            expOnDeath += (expOnDeath * level) - level*2;
            /*Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Creado {name} de nivel {level}, maxHP = {maxHP}");
            Console.ResetColor();*/
        }

    }
    public class Slime : Enemy
    {
        public Slime(int level) : base("Slime", level)
        {
            hpScaleFactor = 3;
            attackScaleFactor = 1;
            expOnDeath = 10;
            speed = 3;
            defense = 0;
            ScaleStats();
        }
    }
    public class Orc : Enemy
    {
        public Orc(int level) : base("Orco", level)
        {
            hpScaleFactor = 4;
            attackScaleFactor = 2;
            expOnDeath = 20;
            speed = 2;
            defense = 0;
            ScaleStats();
        }
    }
    public class Troll : Enemy
    {
        public Troll(int level) : base("Troll", level)
        {
            hpScaleFactor = 5;
            attackScaleFactor = 1;
            expOnDeath = 30;
            speed = 6;
            defense = 1;
            ScaleStats();
        }
    }
    public class MotherSlime : Enemy
    {
        public MotherSlime(int level) : base("Madre Slime", level)
        {
            hpScaleFactor = 8;
            attackScaleFactor = 2;
            expOnDeath = 50;
            speed = 1;
            defense = 2;
            ScaleStats();
        }
    }
    public class Giant : Enemy
    {
        public Giant(int level) : base("Gigante", level)
        {
            hpScaleFactor = 9;
            attackScaleFactor = 4;
            expOnDeath = 60;
            speed = 1;
            defense = 3;
            ScaleStats();
        }

    }

    public class DarkWizard : Enemy
    {
        public DarkWizard(int level) : base("Mago Oscuro", level)
        {
            hpScaleFactor = 7;
            attackScaleFactor = 6;
            expOnDeath = 70;
            speed = 4;
            defense = 2;
            ScaleStats();
        }
    }
}
