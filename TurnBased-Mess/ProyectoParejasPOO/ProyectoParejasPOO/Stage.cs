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
                    case "Mother Slime":
                        for (int i = 0; i < enemyData.quantity; i++)
                        {
                            generatedEnemies.Add(new MotherSlime(enemyData.level));
                        }
                        break;
                    case "Giant":
                        for (int i = 0; i < enemyData.quantity; i++)
                        {
                            generatedEnemies.Add(new Giant(enemyData.level));
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

            Stage stage6 = new Stage(6, false);
            stage6.enemies.Add(new EnemySpawnData("Slime", 3, 2));
            stage6.enemies.Add(new EnemySpawnData("Slime", 4, 1));
            allStages.Add(stage6);

            Stage stage7 = new Stage(7, true);
            stage7.enemies.Add(new EnemySpawnData("Mother Slime", 4, 1));
            stage7.enemies.Add(new EnemySpawnData("Slime", 4, 3));
            allStages.Add(stage7);

            Stage stage8 = new Stage(8, false);
            stage8.enemies.Add(new EnemySpawnData("Mother Slime", 4, 1));
            stage8.enemies.Add(new EnemySpawnData("Orc", 4, 2));
            allStages.Add(stage8);

            Stage stage9 = new Stage(9, false);
            stage9.enemies.Add(new EnemySpawnData("Troll", 4, 1));
            stage9.enemies.Add(new EnemySpawnData("Orc", 4, 2));
            allStages.Add(stage9);

            Stage stage10 = new Stage(10, true);
            stage10.enemies.Add(new EnemySpawnData("Giant", 7, 2));
            stage10.enemies.Add(new EnemySpawnData("Giant", 5, 4));
            stage10.enemies.Add(new EnemySpawnData("Troll", 4, 4));
            stage10.enemies.Add(new EnemySpawnData("Orc", 5, 3));
            stage10.enemies.Add(new EnemySpawnData("Slime", 8, 10));
            allStages.Add(stage10);
            // Agregar más etapas según sea necesario
        }

    }
}
