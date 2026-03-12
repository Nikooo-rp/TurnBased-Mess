using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoParejasPOO
{
    
    public static class BattleUI
    {
        public static void ShowGameIntro()
        {
            Console.Clear();
            Console.WriteLine("=== Bienvenido a THE DUNGEON ===");
            Console.WriteLine("Preparárate para la batalla.");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public static string CreateHeroName()
        {
            Console.Write("Nombre del héroe: ");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
                name = "Heroe";

            return name;
        }

        public static void ShowEnemiesIntro(List<Enemy> enemies)
        {
            Console.Clear();
            if (enemies == null || enemies.Count == 0)
            {
                Console.WriteLine("No hay enemigos en esta etapa.");
                Console.WriteLine();
                return;
            }
            Console.WriteLine("Enemigos visibles en la sala:");
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy e = enemies[i];
                Console.WriteLine($"{i + 1}) {e.name} - Nivel {e.level}");
            }

            Console.WriteLine();
            Console.WriteLine("pulsa una tecla para iniciar el combate.");
            Console.ReadKey();
        }

        public static void ShowTurnStart(Character unit)
        {
            Console.WriteLine($"Es el turno de {unit.name}");
            Console.WriteLine();
        }
        public static void DisplayAttack(Character user, Character target, CharacterAction action)
        {
            Console.WriteLine($"{user.name} usa {action.name} sobre {target.name}.");
            Console.WriteLine($"{target.name} HP: {target.hp}/{target.maxHP}");
            Console.WriteLine();
        }
        public static void DisplayWideAttack(Character user, CharacterAction action)
        {
            Console.WriteLine($"{user.name} usa {action.name} sobre varios objetivos.");
            foreach (var t in action.targets)
            {
                Console.WriteLine($" - {t.name} HP: {t.hp}/{t.maxHP}");
            }
            Console.WriteLine();
        }

        public static void DisplayDefend(Playable p)
        {
            Console.WriteLine($"{p.name} Adopta una postura defensiva");
            Console.WriteLine();
        }
        public static void DisplayHeal(Character user, int healAmount)
        {
            Console.WriteLine($"{user.name} se cura {healAmount} puntos de vida.");
            Console.WriteLine($"{user.name} HP: {user.hp}/{user.maxHP}");
            Console.WriteLine();
        }
        public static void DisplayDefeat(Character unit)
        {
            unit.isAlive = false;
            Console.WriteLine($"{unit.name} ha sido eliminAdo.");
            Console.WriteLine();
        }
        public static void ShowStageClear()
        {
            Console.WriteLine("Etapa superada. Has limpiAdo la sala.");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }
        public static void ShowGameOver()
        {
            Console.Clear();
            Console.WriteLine("Juego terminado. Has sido derrotAdo.");
            Console.WriteLine("Presiona una tecla para reiniciar...");
            Console.ReadKey();
        }
        public static void ShowGameWin()
        {
            Console.Clear();
            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es");
            thread.sleep(2000);
            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es.");
            thread.sleep(1000);
            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es..");
            thread.sleep(1000);
            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es...");
            thread.sleep(4000);
            Console.WriteLine("¡Una palmadita en la espalda¡");
            thread.sleep(1000);
            Console.WriteLine("Presiona una tecla si quieres volver a jugar");
            Console.ReadKey();
        }
        public static CharacterAction GetPlayerAction(Playable player)
        {
            while (true)
            {
                Console.WriteLine($"Selecciona una acción para {player.name} (Mana: {player.mana}/{player.maxMana}):");
                for (int i = 0; i < player.actions.Count; i++)
                {
                    CharacterAction act = player.actions[i];
                    Console.WriteLine($"{i + 1}) {act.name} - {act.description} (Costo de mana: {act.manaCost})");
                }

                Console.Write("Opción: ");
                string? choice = Console.ReadLine();
                if (int.TryParse(choice, out int idx))
                {
                    idx -= 1;
                    if (idx >= 0 && idx < player.actions.Count)
                    {
                        CharacterAction selected = player.actions[idx];
                        if (selected.manaCost > player.mana)
                        {
                            Console.WriteLine("No tienes suficiente mana para esa acción. Elige otra.");
                            continue;
                        }

                        ShowActionInfo(selected); // Es preferible usar la función dedicada a esto.
                        Console.WriteLine("¿Deseas usar esta acción? (s/n)");
                        string? confirm = Console.ReadLine();
                        if (confirm != null && confirm.ToLower() == "s")
                            return selected;
                    }
                }
                Console.WriteLine("Opción inválida, intenta de nuevo.");
            }
        } //copilot (no tenía ni idea)

        public static void ShowActionInfo(CharacterAction action)
        {
            Console.WriteLine($"Acción: {action.name}");
            Console.WriteLine(action.description);
            Console.WriteLine($"Poder: {action.power}   Costo de mana: {action.manaCost}");

            //if (action.targets.Count > 0)
            //{
            //    Console.WriteLine("Objetivos:");
            //    foreach (var t in action.targets)
            //    {
            //        Console.WriteLine($" - {t.name} (HP: {t.hp}/{t.maxHP})");
            //    }
            //}

            // Se puede saltar, esto se llama al momento de seleccionar una acción.

            Console.WriteLine();
        }


        public static Character ChooseSingleTarget(List<Character> targets)
        {
            if (targets == null || targets.Count == 0)
                throw new ArgumentException("No hay objetivos disponibles.");

            Console.WriteLine("Selecciona un objetivo:");
            for (int i = 0; i < targets.Count; i++)
            {
                Character t = targets[i];
                Console.WriteLine($"{i + 1}) {t.name} - HP: {t.hp}/{t.maxHP}");
            }
            Console.Write("Opción: ");
            string? choice = Console.ReadLine();
            if (int.TryParse(choice, out int idx))
            {
                idx -= 1;
                if (idx >= 0 && idx < targets.Count)
                    return targets[idx];
            }
            // fallback: return first
            return targets[0];
        }

        public static void DisplayMiss()
        {
            Console.WriteLine("El ataque no atraviesa la defensa y no causa daño.");
            Console.WriteLine();
        }

        public static void DisplayDamage(Character target, int damage)
        {
            Console.WriteLine($"{target.name} recibe {damage} de daño.");
            Console.WriteLine($"{target.name} HP: {target.hp}/{target.maxHP}");
            if (target.hp <= 0)
            {
                DisplayDefeat(target);
            }
            Console.WriteLine();
        }


        public static void ShowStageIntro(int stageIndex)
        {
            Console.Clear();
            Console.WriteLine($"--- Etapa {stageIndex} ---");
            Console.WriteLine("Los enemigos se acercan y se preparan");
            Console.WriteLine("Presiona cualquier tecla para comenzar la batalla...");
            Console.ReadKey();
        }
    } 
}
