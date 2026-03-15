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


        public record LevelEntry(int level, int expRequired, CharacterAction? newAction, int hpBonus, int atkBonus, int spdBonus, int defenseBonus, int manaBonus); // Un record es una clase inmutable, ideal para representar datos que no cambian después de ser creados. Aquí se usa para definir las entradas de nivel, que contienen toda la información relevante para cada nivel, como la experiencia requerida, las bonificaciones de estadísticas y las nuevas acciones desbloqueadas.
        public readonly List<LevelEntry> LevelTable = new List<LevelEntry>
        {
            // Level, exp, action, hpBonus, atkBonus, spdBonus, defenseBonus, manaBonus
            // El primer nivel (nivel 1) es el nivel inicial del jugador, por lo que no requiere experiencia ni otorga bonificaciones o nuevas acciones.
            new(2, 10, new Defend(), 2, 0, 1, 1, 0),
            new(3, 30, null, 2, 1, 1, 1, 0),
            new(4, 60, new AllTargetAttack(), 3, 1, 1, 1, 1),
            new(5, 140, null, 3, 1, 1, 1, 1),
            new(6, 300, new Heal(), 4, 2, 1, 2, 2),
            new(7, 650, null, 4, 2, 1, 2, 3),
            new(8, 1400, new FireBall(), 5, 3, 2, 2, 3),
            // Agregar más niveles según sea necesario
        };

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

            actions.Add(new SingleTargetAttack());
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
            BattleUI.DisplayExperienceGain(this, ammount);
            CheckLevelUp();
        }
        public void CheckLevelUp()
        {
            int newLevel = level;
            foreach (LevelEntry entry in LevelTable)
            {
                if (exp >= entry.expRequired && level < entry.level)
                {
                    newLevel = entry.level;
                }
            }
            while (level < newLevel)
            {
                level++;
                LevelUp(level);
            }
        }
        public void LevelUp(int level)
        {
            LevelEntry entry = LevelTable.Find(e => e.level == level);
            if (entry != null)
            {
                maxHP += entry.hpBonus;
                atk += entry.atkBonus;
                spd += entry.spdBonus;
                defense += entry.defenseBonus;
                maxMana += entry.manaBonus;
                mana = maxMana; // Al subir de nivel, el jugador recupera todo su mana
                if (entry.newAction != null)
                {
                    actions.Add(entry.newAction);
                    BattleUI.DisplayLevelUp(this, entry.newAction);
                }
                else
                {
                    BattleUI.DisplayLevelUp(this, null);
                }

            }
        }
    }
}
