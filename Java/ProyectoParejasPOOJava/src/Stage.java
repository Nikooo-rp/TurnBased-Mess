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
    }
}