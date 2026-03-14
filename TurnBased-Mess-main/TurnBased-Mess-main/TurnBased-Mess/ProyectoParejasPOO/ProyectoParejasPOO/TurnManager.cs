using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoParejasPOO
{
    public class TurnManager
    {
        public List<Character> allUnits = new List<Character>();
        public Queue<Character> turnOrder = new Queue<Character>();
        public int totalTurns = 0;

        public void CalculateOrder()
        {
            turnOrder.Clear();

            List<Character> ordered = allUnits
                .Where(unit => unit != null && unit.isAlive)
                .OrderByDescending(unit => unit.spd)
                .ToList();

            foreach (var unit in ordered)
                turnOrder.Enqueue(unit);
        }

        public Character NextTurn()
        {
            if (turnOrder.Count == 0)
                StartRound();

            RemoveDeadCharacters();

            Character unit = turnOrder.Dequeue();
            totalTurns++;
            BattleUI.ShowTurnStart(unit);
            return unit;
        }

        public void RemoveDeadCharacters()
        {
            allUnits = allUnits.Where(unit => unit != null && unit.isAlive).ToList();

            if (turnOrder.Count > 0)
            {
                var remaining = turnOrder.Where(unit => unit != null && unit.isAlive).ToList();
                turnOrder.Clear();
                foreach (var unit in remaining)
                    turnOrder.Enqueue(unit);
            }
        }

        public void StartRound()
        {
            RemoveDeadCharacters();
            CalculateOrder();
        }

        public void AddUnit(Character c)
        {
            if (c == null)
                return;

            if (!allUnits.Contains(c))
                allUnits.Add(c);
        }

        public void ClearUnits()
        {
            allUnits.Clear();
            turnOrder.Clear();
            totalTurns = 0;
        }
    }
}
