using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class EnemySpawnData
    {
        public string name;
        public int level;
        public int quantity;

        public EnemySpawnData(string name, int level, int quantity)
        {
            this.name = name;
            this.level = level;
            this.quantity = quantity;
        }
    }
}
