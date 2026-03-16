import java.util.ArrayList;
import java.util.List;

public class Stage {

    public int stageNumber;
    public List<EnemySpawnData> enemies = new ArrayList<>();
    public boolean respite;

    public Stage(int stageNumber, boolean respite) {
        this.stageNumber = stageNumber;
        this.respite     = respite;
    }

    public List<Enemy> generateEnemies() {
        List<Enemy> generated = new ArrayList<>();
        for (EnemySpawnData data : enemies) {
            for (int i = 0; i < data.quantity; i++) {
                switch (data.name) {
                    case "Slime": generated.add(new Slime(data.level)); break;
                    case "Orc":   generated.add(new Orc(data.level));   break;
                    case "Troll": generated.add(new Troll(data.level)); break;
                    case "Mother Slime": generated.add(new MotherSlime(data.level)); break;
                    case "Giant": generated.add(new Giant(data.level)); break;
                    default: throw new RuntimeException("Tipo de enemigo desconocido: " + data.name);
                }
            }
        }
        return generated;
    }
}

class Stages {
    public List<Stage> allStages = new ArrayList<>();

    public Stages() {
        Stage stage1 = new Stage(1, false);
        stage1.enemies.add(new EnemySpawnData("Slime", 1, 1));
        allStages.add(stage1);

        Stage stage2 = new Stage(2, false);
        stage2.enemies.add(new EnemySpawnData("Slime", 2, 1));
        allStages.add(stage2);

        Stage stage3 = new Stage(3, false);
        stage3.enemies.add(new EnemySpawnData("Slime", 2, 2));
        allStages.add(stage3);

        Stage stage4 = new Stage(4, true);
        stage4.enemies.add(new EnemySpawnData("Orc", 3, 1));
        allStages.add(stage4);

        Stage stage5 = new Stage(5, false);
        stage5.enemies.add(new EnemySpawnData("Orc", 2, 1));
        stage5.enemies.add(new EnemySpawnData("Slime", 3, 1));
        allStages.add(stage5);

        Stage stage6 = new Stage(6, false);
        stage6.enemies.add(new EnemySpawnData("Slime", 3, 2));
        stage6.enemies.add(new EnemySpawnData("Slime", 4, 1));
        allStages.add(stage6);

        Stage stage7 = new Stage(7, true);
        stage7.enemies.add(new EnemySpawnData("Mother Slime", 4, 1));
        stage7.enemies.add(new EnemySpawnData("Slime", 4, 3));
        allStages.add(stage7);

        Stage stage8 = new Stage(8, false);
        stage8.enemies.add(new EnemySpawnData("Mother Slime", 4, 1));
        stage8.enemies.add(new EnemySpawnData("Orc", 4, 2));
        allStages.add(stage8);

        Stage stage9 = new Stage(9, false);
        stage9.enemies.add(new EnemySpawnData("Troll", 4, 1));
        stage9.enemies.add(new EnemySpawnData("Orc", 4, 2));
        allStages.add(stage9);

        Stage stage10 = new Stage(10, true);
        stage10.enemies.add(new EnemySpawnData("Giant", 7, 2));
        stage10.enemies.add(new EnemySpawnData("Giant", 5, 4));
        stage10.enemies.add(new EnemySpawnData("Troll", 4, 4));
        stage10.enemies.add(new EnemySpawnData("Orc", 5, 3));
        stage10.enemies.add(new EnemySpawnData("Slime", 8, 10));
        allStages.add(stage10);
    }
}