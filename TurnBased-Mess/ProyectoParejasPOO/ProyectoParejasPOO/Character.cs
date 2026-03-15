using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public abstract class Character
    {
        public int hp, maxHP, atk, spd, exp, defense, level;
        public string name;
        public bool isAlive = true;
        public List<CharacterAction> actions = new List<CharacterAction>();

        public Character(string name)
        {
            this.name = name;
        }

        public void TakeDamage(int damage)
        {
            if (this is Playable) // Si es jugador, revisamos si está defendiendo
            {
                Playable p = (Playable)this;
                if (p.isDefending)
                {
                    defense += (int)MathF.Ceiling(defense / 6);
                }
                p.isDefending = false; // La defensa solo dura un turno, se resetea después de aplicarla
            }

            int damageTaken = Math.Max(0, damage - this.defense);
            if (damageTaken <= 0) // Si la defensa anula o supera el daño, no se resta HP, el ataque falla
            {
                BattleUI.DisplayMiss();
            }
            else // De lo contrario, informamos que acierta.
            {
                this.hp = Math.Max(0, this.hp - damageTaken);
                BattleUI.DisplayDamage(this, damageTaken);
            }

            if (this.hp <= 0)
            {
                this.isAlive = false;
                if (this is Enemy)
                {
                    Enemy enemy = (Enemy)this;
                    enemy.OnDeath?.Invoke(enemy.expOnDeath); // Disparamos el evento de muerte para otorgar experiencia al jugador
                }
            }
        }
        public abstract CharacterAction ChooseAction();
    }
}
