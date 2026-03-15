using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProyectoParejasPOO
{
    public abstract class CharacterAction
    {
        public string name = String.Empty;
        public string description = String.Empty;
        public int power, manaCost;
        public Character? user;
        public List<Character> targets = new List<Character>();
        public static Random random = new Random();
        public abstract void ChooseTargets(GameManager gm);
        public abstract void Execute();
    }

    public class SingleTargetAttack : CharacterAction
    {
        public SingleTargetAttack()
        {
            name = "Ataque simple";
            description = "Tajo sencillo, un solo objetivo";
            power = 2;
            manaCost = 0;
        }
        public override void ChooseTargets(GameManager gm)
        {
            targets.Clear();
            List<Character> enemies = gm.GetAliveEnemies(user);

            Character target;

            if (user is Enemy)
                target = enemies[random.Next(enemies.Count)];
            else
                target = BattleUI.ChooseSingleTarget(enemies);

            targets.Add(target);
        }
        public override void Execute()
        {
            Character target = targets[0];
            int damage = (user.atk * power);
            BattleUI.DisplayAttack(user, target, this);
            target.TakeDamage(damage);
        }
    }
    public class AllTargetAttack : CharacterAction
    {
        public AllTargetAttack()
        {
            name = "Tajo amplio";
            description = "Dispara una ola de fuerza desde tu espada, atacando a todos los enemigos a la vez";
            power = 1;
            manaCost = 3;
        }
        public override void ChooseTargets(GameManager gm)
        {
            targets.Clear();
            List<Character> enemies = gm.GetAliveEnemies(user);
            targets.AddRange(enemies);
        }
        public override void Execute()
        {
            BattleUI.DisplayWideAttack(user, this);
            foreach (Character target in targets)
            {
                int damage = (user.atk * power);
                target.TakeDamage(damage);
            }
        }
    }
    public class Defend : CharacterAction
    {
        public Defend()
        {
            name = "Defender";
            description = "Asumes una postura defensiva, reduciendo el daño recibido en el próximo turno enemigo";
            power = 0;
            manaCost = 2;
        }
        public override void ChooseTargets(GameManager gm)
        {
            targets.Clear();
            targets.Add(user);
        }
        public override void Execute()
        {
            if (user is Playable)
            {
                Playable p = (Playable)user;
                p.isDefending = true;
                BattleUI.DisplayDefend(p);
            }
            else
            {
                Exception ex = new Exception("Solo los personajes jugables pueden defenderse");
                throw ex;
            }
        }
    }
    public class Heal : CharacterAction
    {
        public Heal()
        {
            name = "Curar heridas";
            description = "Recupera parte de tu salud";
            power = 3;
            manaCost = 5;
        }
        public override void ChooseTargets(GameManager gm)
        {
            targets.Clear();
            targets.Add(user);
        }
        public override void Execute()
        {
            int healAmount = (user.atk * power);
            user.hp = Math.Min(user.maxHP, user.hp + healAmount);
            BattleUI.DisplayHeal(user, healAmount);
        }
    }

    public class FireBall : CharacterAction
    {
        public FireBall()
        {
            name = "Bola de fuego";
            description = "Lanzas una bola de fuego que daña a todos los enemigos, con una potencia que aumenta con tu nivel";
            power = 10;
            manaCost = 15;
        }
        public override void ChooseTargets(GameManager gm)
        {
            targets.Clear();
            List<Character> enemies = gm.GetAliveEnemies(user);
            targets.AddRange(enemies);
        }
        public override void Execute()
        {
            BattleUI.DisplayFireBall(user);
            foreach (Character target in targets)
            {
                int damage = (user.atk * power) + (user.level * 2); // El daño aumenta con el nivel del usuario
                target.TakeDamage(damage);
            }
        }
    }
}
