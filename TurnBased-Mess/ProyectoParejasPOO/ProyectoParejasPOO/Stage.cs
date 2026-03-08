using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Stage
    {
        public int stageNumber;
        public List<EnemySpawnData> enemies = new List<EnemySpawnData>();

        public Stage(int stageNumber)
        {
            this.stageNumber = stageNumber;
        }

        public List<Enemy> GenerateEnemies()
        {
            List<Enemy> generatedEnemies = new List<Enemy>();
            foreach (EnemySpawnData enemyData in enemies)
            {
                switch (enemyData.name)
                {
                    case "Slime":
                        for (int i = 0; i < enemyData.quantity; i++)
                        {
                            generatedEnemies.Add(new Slime(enemyData.level));
                        }
                        break;
                    case "Orc":
                        for (int i = 0; i < enemyData.quantity; i++)
                        {
                            generatedEnemies.Add(new Orc(enemyData.level));
                        }
                        break;
                    case "Troll":
                        for (int i = 0; i < enemyData.quantity; i++)
                        {
                            generatedEnemies.Add(new Troll(enemyData.level));
                        }
                        break;
                    default:
                        throw new Exception($"Tipo de enemigo desconocido: {enemyData.name}");
                        // Agregar más casos para otros tipos de enemigos
                }
            }
            return generatedEnemies;
        }
    }

    public class Stages
    {
        public List<Stage> allStages = new List<Stage>();
        public Stages()
        {
            // Aquí se pueden definir las etapas y los enemigos que aparecerán en cada una
            Stage stage1 = new Stage(1);
            stage1.enemies.Add(new EnemySpawnData("Slime", 1, 2));
            allStages.Add(stage1);
            Stage stage2 = new Stage(2);
            stage2.enemies.Add(new EnemySpawnData("Slime", 2, 4));
            stage2.enemies.Add(new EnemySpawnData("Orc", 3, 1));
            allStages.Add(stage2);
            // Agregar más etapas según sea necesario
        }

    }
}
