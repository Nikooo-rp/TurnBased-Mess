import java.util.ArrayList;
import java.util.List;

public class GameManager {

    public int currentStageIndex = 0;
    public List<Playable> playerCharacters = new ArrayList<>();
    public List<Enemy> currentEnemies = new ArrayList<>();
    public TurnManager turnManager = new TurnManager();
    Stages stagesList = new Stages();

    public void startGame() throws InterruptedException {
        BattleUI.showGameIntro();
        createHero();
        loadNextStage();
    }

    public void createHero() {
        playerCharacters.clear();
        Playable hero = new Playable(BattleUI.createHeroName(), 10, 2, 5, 0, 1, 5, 1);
        playerCharacters.add(hero);
    }

    public void runBattle() throws InterruptedException {
        Stage stage = stagesList.allStages.get(currentStageIndex - 1);
        currentEnemies.clear();
        currentEnemies.addAll(stage.generateEnemies());
        BattleUI.showEnemiesIntro(currentEnemies);

        for (Enemy e : currentEnemies) {
            e.onDeath = this::givePlayerExperience;
        }

        saveCurrentUnits();
        while (!isBattleOver()) {
            processTurn();
        }
        checkBattleState();
    }

    public void processTurn() {
        Character actor = turnManager.nextTurn();
        if (!actor.isAlive) return;
        CharacterAction action = requestAction(actor);
        action.chooseTargets(this);
        action.execute();
    }

    public boolean isBattleOver() {
        boolean allEnemiesDead = true;
        for (Enemy e : currentEnemies) {
            if (e.isAlive) { allEnemiesDead = false; break; }
        }
        boolean allPlayersDead = true;
        for (Playable p : playerCharacters) {
            if (p.isAlive) { allPlayersDead = false; break; }
        }
        return allEnemiesDead || allPlayersDead;
    }

    public void saveCurrentUnits() {
        if (!playerCharacters.isEmpty() && !currentEnemies.isEmpty()) {
            turnManager.clearUnits();
            for (Playable p : playerCharacters) turnManager.addUnit(p);
            for (Enemy e : currentEnemies)     turnManager.addUnit(e);
        }
    }

    public List<Character> getAliveEnemies(Character requester) {
        List<Character> result = new ArrayList<>();
        if (requester instanceof Playable) {
            for (Enemy e : currentEnemies)
                if (e.isAlive) result.add(e);
        } else {
            for (Playable p : playerCharacters)
                if (p.isAlive) result.add(p);
        }
        return result;
    }

    public CharacterAction requestAction(Character character) {
        return character.chooseAction();
    }

    public void loadNextStage() throws InterruptedException {
        currentStageIndex++;
        if ((currentStageIndex - 1) >= stagesList.allStages.size()) {
            BattleUI.showGameWin();
            startGame();
        } else {
            if (stagesList.allStages.get(currentStageIndex - 1).respite) {
                respite();
            }
            BattleUI.showStageIntro(currentStageIndex);
            runBattle();
        }
    }

    public void checkBattleState() throws InterruptedException {
        boolean allEnemiesDead = true;
        for (Enemy e : currentEnemies)
            if (e.isAlive) { allEnemiesDead = false; break; }

        boolean allPlayersDead = true;
        for (Playable p : playerCharacters)
            if (p.isAlive) { allPlayersDead = false; break; }

        if (allEnemiesDead) {
            BattleUI.showStageClear();
            loadNextStage();
        } else if (allPlayersDead) {
            BattleUI.showGameOver();
            startGame();
        }
    }

    public void givePlayerExperience(int exp) {
        for (Playable p : playerCharacters) {
            p.gainExperience(exp);
        }
    }

    public void respite() {
        for (Playable p : playerCharacters) {
            p.hp    = p.maxHP;
            p.mana  = p.maxMana;
        }
        BattleUI.showRespite();
    }
}
