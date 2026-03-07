using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class BattleUI
    {
        public static void ShowGameIntro()
        {
            Console.WriteLine("En una mazmorra lejana, un valiente aventurero se adentra en las profundidades para enfrentarse a peligrosas criaturas y descubrir tesoros ocultos.");
            Thread.Sleep(1000);
            Console.WriteLine("Cada paso que da lo acerca más a su destino, pero también aumenta el riesgo de encontrarse con enemigos mortales.");
            Thread.Sleep(1000);
            Console.WriteLine("¿Serán capaces de superar los desafíos que les esperan y salir victoriosos?");
        }
        public static string CreateHeroName()
        {
            string nombre = string.Empty;
            bool valid = false;

            while (!valid)
            {
                Console.WriteLine("¿Cómo se llama tu héroe?");
                nombre = Console.ReadLine();

                if (nombre == string.Empty || string.IsNullOrWhiteSpace(nombre))
                {
                    DisplayError("Ingresa un nombre válido.");
                }
                else
                {
                    nombre = nombre.Trim();
                    valid = true;
                }
            }
            return nombre;
        }
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void ShowGameWin()
        {
            Console.WriteLine("Finalmente en el fondo de la mazmorra... encuentras un cofre del tesoro.");
            Thread.Sleep(1000);
            Console.WriteLine("Es pesado, pero logras levantar la tapa...");
            Thread.Sleep(1000);
            Console.WriteLine("Oh..");
            Thread.Sleep(1000);
            Console.WriteLine("Dios mio");
            Thread.Sleep(3000);
            Console.WriteLine("Adentro del cofre, encuentras...");
            Thread.Sleep(1000);
            Console.WriteLine("https://www.youtube.com/watch?v=xvFZjo5PgG0");
        }
        public static void ShowGameOver()
        {
            Console.WriteLine("Has sido derrotado en la mazmorra. ¡Inténtalo de nuevo y demuestra tu valía!");
        }
        public static void ShowStageClear()
        {
            Console.WriteLine("Parece que ya no quedan enemigos...");
        }
        public static void ShowStageIntro(int stageNumber)
        {
            if (stageNumber != 1)
            {
                Console.WriteLine("Desciendes por unas escaleras...");

            }
            Thread.Sleep(1000);
            Console.WriteLine($"¡Has entrado en la etapa {stageNumber} de la mazmorra!");
        }
        public static CharacterAction GetPlayerAction(Playable player)
        {
            Console.WriteLine("Qué acción quieres realizar?");
            bool decided = false;
            CharacterAction? chosenAction = null;
            while (!decided)
            {
                foreach (CharacterAction action in player.actions)
                {
                    Console.WriteLine(action.name);
                }
                string actionName = Console.ReadLine();
                foreach (CharacterAction action in player.actions)
                {
                    if (action.name == actionName)
                    {
                        chosenAction = action;
                        decided = true;
                        break;
                    }
                }
                if (!decided)
                {
                    Console.WriteLine("Ingresa una acción válida");
                }
            }
            return chosenAction!;
        }

        public static Character ChooseTarget(List<Character> possibleTargets)
        {
            Console.WriteLine("Elige un objetivo:");
            bool decided = false;
            Character? chosenTarget = null;
            while (!decided)
            {
                foreach (Character target in possibleTargets)
                {
                    Console.WriteLine(target.name);
                }
                string targetName = Console.ReadLine();
                foreach (Character target in possibleTargets)
                {
                    if (target.name == targetName)
                    {
                        chosenTarget = target;
                        decided = true;
                        break;
                    }
                }
                if (!decided)
                {
                    Console.WriteLine("Ingresa un objetivo válido");
                }
            }
            return chosenTarget!;
        }

        public static void DisplayBattleStatus(List<Character> players, List<Character> enemies)
        {
            Console.WriteLine("Estado de la batalla:");
            Console.WriteLine("Jugadores:");
            foreach (Character player in players)
            {
                Console.WriteLine($"{player.name} - HP: {player.hp}/{player.maxHP} - Mana: {(player is Playable p ? p.mana : 0)}/{(player is Playable p2 ? p2.maxMana : 0)}");
            }
            Console.WriteLine("Enemigos:");
            foreach (Character enemy in enemies)
            {
                Console.WriteLine($"{enemy.name} - HP: {enemy.hp}/{enemy.maxHP}");
            }
        }

        public static void DisplayAttack(Character user, Character target, CharacterAction action)
        {
            Console.WriteLine($"{user.name} usa {action.name} en {target.name}!");
        }

        public static void DisplayDamage(Character target, int damage)
        {
            switch (damage)
            {
                case < 10:
                    Console.WriteLine("¡Un golpe débil!");
                    break;
                case < 30:
                    Console.WriteLine("¡Que fuerte! Ha temblado un poco el suelo");
                    break;
                case < 50:
                    Console.WriteLine("¡Un golpe poderoso! El impacto resuena por la mazmorra");
                    break;
                case > 80:
                    Console.WriteLine("¡BONK!");
                    break;
            }
            Console.WriteLine($"{target.name} recibe {damage} de daño!");
        }

        public static void DisplayEnemyTurn()
        {
            string[] flavor = new string[]
            {
                "El enemigo se prepara para atacar.",
                "¿Qué hará el enemigo ahora?",
                "¡El enemigo está a punto de hacer su movimiento!",
                "¡Cuidado, el enemigo se aproxima!"
            };
            Random random = new Random();
            int index = random.Next(flavor.Length);
            Console.WriteLine(flavor[index]);
        }

        public static void DisplayPlayerTurn()
        {
            string[] flavor = new string[]
            {
                "Es tu turno, elige sabiamente.",
                "¿Qué harás ahora?",
                "¡Es tu momento de brillar!",
                "¡Vamos, tú puedes hacerlo!"
            };

            Random random = new Random();
            int index = random.Next(flavor.Length);

            Console.WriteLine(flavor[index]);
        }
    } 
}
