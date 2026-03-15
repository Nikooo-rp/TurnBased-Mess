public abstract class Enemy extends Character {

    public int expOnDeath;
    public int hpScaleFactor;
    public int atkScaleFactor;
    public DeathCallBack onDeath;

    public Enemy(String name, int level) {
        super(name);
        this.level = level;
        actions.add(new SingleTargetAttack());
    }

    @Override
    public CharacterAction chooseAction() {
        java.util.Random ran = new java.util.Random();
        int index = ran.nextInt(actions.size());
        CharacterAction action = actions.get(index);
        action.user = this;
        return action;
    }

    public void scaleStats() {
        maxHP      += hpScaleFactor * level;
        hp          = maxHP;
        atk        += atkScaleFactor * level;
        defense    += defense * level;
        expOnDeath += (expOnDeath * level) - level * 2;
    }
}

class Slime extends Enemy {
    public Slime(int level) {
        super("Slime", level);
        hpScaleFactor  = 3;
        atkScaleFactor = 1;
        expOnDeath     = 10;
        scaleStats();
    }
}

class Orc extends Enemy {
    public Orc(int level) {
        super("Orco", level);
        hpScaleFactor  = 4;
        atkScaleFactor = 2;
        expOnDeath     = 20;
        scaleStats();
    }
}

class Troll extends Enemy {
    public Troll(int level) {
        super("Troll", level);
        hpScaleFactor  = 10;
        atkScaleFactor = 2;
        expOnDeath     = 30;
        scaleStats();
    }
}

class MotherSlime extends Enemy {
    public MotherSlime(int level) {
        super("Madre Slime", level);
        hpScaleFactor  = 8;
        atkScaleFactor = 2;
        expOnDeath     = 50;
        scaleStats();
    }
}

class Giant extends Enemy {
    public Giant(int level) {
        super("Gigante", level);
        hpScaleFactor  = 12;
        atkScaleFactor = 5;
        expOnDeath     = 60;
        scaleStats();
    }
}

class DarkWizard extends Enemy {
    public DarkWizard(int level) {
        super("Mago Oscuro", level);
        hpScaleFactor  = 7;
        atkScaleFactor = 6;
        expOnDeath     = 70;
        scaleStats();
    }
}
