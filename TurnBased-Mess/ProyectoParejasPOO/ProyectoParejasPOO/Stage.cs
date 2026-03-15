using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class Stage
    {
        public int stageNumber;
        public List<EnemySpawnData> enemies = new List<EnemySpawnData>();
        public bool respite;

        public Stage(int stageNumber, bool respite)
        {
            this.stageNumber = stageNumber;
            this.respite = respite;
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

            Stage stage1 = new Stage(1, false);
            stage1.enemies.Add(new EnemySpawnData("Slime", 1, 1));
            allStages.Add(stage1);

            Stage stage2 = new Stage(2, false);
            stage2.enemies.Add(new EnemySpawnData("Slime", 2, 1));
            allStages.Add(stage2);

            Stage stage3 = new Stage(3, false);
            stage3.enemies.Add(new EnemySpawnData("Slime", 2, 2));
            allStages.Add(stage3);

            Stage stage4 = new Stage(4, true);
            stage4.enemies.Add(new EnemySpawnData("Orc", 3, 1));
            allStages.Add(stage4);

            Stage stage5 = new Stage(5, false);
            stage5.enemies.Add(new EnemySpawnData("Orc", 2, 1));
            stage5.enemies.Add(new EnemySpawnData("Slime", 3, 1));
            allStages.Add(stage5);
            // Agregar más etapas según sea necesario
        }

    }
}
