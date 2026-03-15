import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public abstract class CharacterAction {
    public String name        = "";
    public String description = "";
    public int power, manaCost;
    public Character user;
    public List<Character> targets = new ArrayList<>();
    public static Random random    = new Random();

    public abstract void chooseTargets(GameManager gm);
    public abstract void execute();
}

class SingleTargetAttack extends CharacterAction {
    public SingleTargetAttack() {
        name        = "Ataque simple";
        description = "Tajo sencillo, un solo objetivo";
        power       = 2;
        manaCost    = 0;
    }
    @Override
    public void chooseTargets(GameManager gm) {
        targets.clear();
        List<Character> enemies = gm.getAliveEnemies(user);
        Character target;
        if (user instanceof Enemy)
            target = enemies.get(random.nextInt(enemies.size()));
        else
            target = BattleUI.chooseSingleTarget(enemies);
        targets.add(target);
    }
    @Override
    public void execute() {
        Character target = targets.getFirst();
        BattleUI.displayAttack(user, target, this);
        target.takeDamage(user.atk * power);
    }
}

class AllTargetAttack extends CharacterAction {
    public AllTargetAttack() {
        name        = "Tajo amplio";
        description = "Dispara una ola de fuerza desde tu espada, atacando a todos los enemigos a la vez";
        power       = 1;
        manaCost    = 3;
    }
    @Override
    public void chooseTargets(GameManager gm) {
        targets.clear();
        targets.addAll(gm.getAliveEnemies(user));
    }
    @Override
    public void execute() {
        BattleUI.displayWideAttack(user, this);
        for (Character target : targets)
            target.takeDamage(user.atk * power);

        if (user instanceof Playable p){
            p.mana = Math.max(0, p.mana - manaCost);
        }
    }
}

class Defend extends CharacterAction {
    public Defend() {
        name        = "Defender";
        description = "Asumes una postura defensiva, reduciendo el daño recibido en el próximo turno enemigo";
        power       = 0;
        manaCost    = 2;
    }
    @Override
    public void chooseTargets(GameManager gm) {
        targets.clear();
        targets.add(user);
    }
    @Override
    public void execute() {
        if (user instanceof Playable p) {
            p.isDefending = true;
            p.mana = Math.max(0, p.mana - manaCost);
            BattleUI.displayDefend(p);
        } else {
            throw new RuntimeException("Solo los personajes jugables pueden defenderse");
        }
    }
}

class Heal extends CharacterAction {
    public Heal() {
        name        = "Curar heridas";
        description = "Recupera parte de tu salud";
        power       = 3;
        manaCost    = 5;
    }
    @Override
    public void chooseTargets(GameManager gm) {
        targets.clear();
        targets.add(user);
    }
    @Override
    public void execute() {
        int healAmount = user.atk * power;
        user.hp        = Math.min(user.maxHP, user.hp + healAmount);
        BattleUI.displayHeal(user, healAmount);

        if (user instanceof Playable p){
            p.mana = Math.max(0, p.mana - manaCost);
        }
    }
}

class FireBall extends CharacterAction {
    public FireBall() {
        name        = "Bola de fuego";
        description = "Lanzas una bola de fuego que daña a todos los enemigos, con una potencia que aumenta con tu nivel";
        power       = 10;
        manaCost    = 15;
    }
    @Override
    public void chooseTargets(GameManager gm) {
        targets.clear();
        targets.addAll(gm.getAliveEnemies(user));
    }
    @Override
    public void execute() {
        BattleUI.displayFireBall(user);
        for (Character target : targets) {
            int damage = (user.atk * power) + (user.level * 2);
            target.takeDamage(damage);
        }

        if (user instanceof Playable p){
            p.mana = Math.max(0, p.mana - manaCost);
        }
    }
}
