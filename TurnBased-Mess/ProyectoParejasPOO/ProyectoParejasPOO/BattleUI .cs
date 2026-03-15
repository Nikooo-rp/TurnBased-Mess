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
            Console.ReadKey(false);
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
            if (unit is Playable)
                Console.WriteLine("Es tu turno!");
            else
                Console.WriteLine($"Es el turno de {unit.name}");

        }
        public static void DisplayAttack(Character user, Character target, CharacterAction action)
        {
            Console.WriteLine($"{user.name} usa {action.name} sobre {target.name}.");
            Console.ReadLine();
        }
        public static void DisplayWideAttack(Character user, CharacterAction action)
        {
            Console.WriteLine($"{user.name} usa {action.name} sobre varios objetivos.");
            /*foreach (var t in action.targets)
            {
                Console.WriteLine($" - {t.name} HP: {t.hp}/{t.maxHP}");
            }*/
        }
        public static void DisplayFireBall(Character user)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"El cielo se enciende y todo retumba, {user.name} lanza una gigante bola de fuego sobre los enemigos");
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void DisplayDefend(Playable p)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{p.name} Adopta una postura defensiva");
            Console.ResetColor();
        }
        public static void DisplayHeal(Character user, int healAmount)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{user.name} se cura {healAmount} puntos de vida.");
            Console.ResetColor();
        }
        public static void DisplayDefeat(Character unit)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            unit.isAlive = false;
            Console.WriteLine($"{unit.name} ha sido eliminAdo.");
            Console.ResetColor();
            Console.ReadKey();
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
            Thread.Sleep(1000);

            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es.");
            Thread.Sleep(1000);

            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es..");
            Thread.Sleep(1000);

            Console.WriteLine("¡Felicidades! Por tu perseveración has completAdo todas las etapas, tu premio es...");
            Thread.Sleep(3000);

            Console.WriteLine("¡Una palmadita en la espalda¡");
            Console.WriteLine("Presiona una tecla si quieres volver a jugar");
            Console.ReadKey();
        }
        public static CharacterAction GetPlayerAction(Playable player)
        {
            while (true)
            {
                Console.WriteLine($"¿Qué hará {player.name}? (HP: {player.hp}/{player.maxHP}) - (Mana: {player.mana}/{player.maxMana})");
                for (int i = 0; i < player.actions.Count; i++)
                {
                    CharacterAction act = player.actions[i];
                    Console.WriteLine($"{i + 1}) {act.name} (Costo maná: {act.manaCost})");
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
                        return selected;
                        // ShowActionInfo generaba mucho clutter en la consola, así que la info se movió a la lista inicial de acciones.
                        ShowActionInfo(selected); // Es preferible usar la función dedicada a esto.
                        Console.WriteLine("¿Deseas usar esta acción? (s/n)");
                        string? confirm = Console.ReadLine();
                        if (confirm != null && confirm.ToLower() == "s")
                            return selected;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opción inválida, intenta de nuevo.");
                Console.ResetColor();
            }
        } //copilot (no tenía ni idea)

        public static void ShowActionInfo(CharacterAction action) // En desuso.
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
        }


        public static Character ChooseSingleTarget(List<Character> targets)
        {
            if (targets == null || targets.Count == 0)
                throw new ArgumentException("No hay objetivos disponibles.");
            if (targets.Count == 1)
                return targets[0];

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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{target.name} recibe {damage} de daño.");
            Console.WriteLine($"{target.name} HP: {target.hp}/{target.maxHP}");
            if (target.hp <= 0)
            {
                DisplayDefeat(target);
            }
            Console.WriteLine();
            Console.ResetColor();
        }


        public static void ShowStageIntro(int stageIndex)
        {
            Console.Clear();
            Console.WriteLine($"--- Etapa {stageIndex} ---");
            Console.WriteLine("Los enemigos se acercan y se preparan");
            Console.WriteLine("Presiona cualquier tecla para comenzar la batalla...");
            Console.ReadKey(false);
        }

        public static void DisplayExperienceGain(Playable player, int exp)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{player.name} gana {exp} puntos de experiencia!");
            Console.ResetColor();
        }

        public static void DisplayLevelUp(Playable player, CharacterAction? action)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{player.name} sube al nivel {player.level}!");
            Console.WriteLine($"HP: {player.hp}/{player.maxHP}  ATK: {player.atk}  SPD: {player.spd}  Mana: {player.mana}/{player.maxMana}");

            if (action != null)
            {
                Console.WriteLine($"¡{player.name} aprendió una nueva acción: {action.name}!");
                Console.WriteLine(action.description);
            }
            Console.ResetColor();
        }

        public static void ShowRespite()
        {
            Console.Clear();
            Console.WriteLine("Después de la batalla, tienes un momento para descansar y prepararte para lo que viene...");
            Console.WriteLine("¡Recuperas tu vida y maná!");
            Console.ReadKey(false);
        }
    }
}
