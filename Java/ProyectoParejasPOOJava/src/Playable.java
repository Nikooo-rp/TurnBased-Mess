import java.util.ArrayList;
import java.util.List;

public class Playable extends Character {

    public int mana;
    public int maxMana;
    public boolean isDefending = false;

    public static class LevelEntry {
        public int level, expRequired, hpBonus, atkBonus, spdBonus, defenseBonus, manaBonus;
        public CharacterAction newAction;

        public LevelEntry(int level, int expRequired, CharacterAction newAction,
                          int hpBonus, int atkBonus, int spdBonus, int defenseBonus, int manaBonus) {
            this.level        = level;
            this.expRequired  = expRequired;
            this.newAction    = newAction;
            this.hpBonus      = hpBonus;
            this.atkBonus     = atkBonus;
            this.spdBonus     = spdBonus;
            this.defenseBonus = defenseBonus;
            this.manaBonus    = manaBonus;
        }
    }

    public final List<LevelEntry> levelTable = new ArrayList<>();

    public Playable(String name, int maxHp, int atk, int spd,
                    int exp, int defense, int maxMana, int level) {
        super(name);
        this.maxHP   = maxHp;
        this.hp      = maxHp;
        this.atk     = atk;
        this.spd     = spd;
        this.exp     = exp;
        this.defense = defense;
        this.maxMana = maxMana;
        this.mana    = maxMana;
        this.level   = level;
        actions.add(new SingleTargetAttack());

        levelTable.add(new LevelEntry(2, 10,  new Defend(),          2, 0, 1, 1, 0));
        levelTable.add(new LevelEntry(3, 30,  null,                  2, 1, 1, 1, 0));
        levelTable.add(new LevelEntry(4, 60,  new AllTargetAttack(), 3, 1, 1, 1, 1));
        levelTable.add(new LevelEntry(5, 140, null,                  3, 1, 1, 1, 1));
        levelTable.add(new LevelEntry(6, 300, new Heal(),            4, 2, 1, 2, 2));
        levelTable.add(new LevelEntry(7, 650, null,                  4, 2, 1, 2, 3));
        levelTable.add(new LevelEntry(8, 1400,new FireBall(),        5, 3, 2, 2, 3));
    }

    @Override
    public CharacterAction chooseAction() {
        CharacterAction action = BattleUI.getPlayerAction(this);
        action.user = this;
        return action;
    }

    public void gainExperience(int amount) {
        exp += amount;
        BattleUI.displayExperienceGain(this, amount);
        checkLevelUp();
    }

    public void checkLevelUp() {
        int newLevel = level;
        for (LevelEntry entry : levelTable) {
            if (exp >= entry.expRequired && level < entry.level)
                newLevel = entry.level;
        }
        while (level < newLevel) {
            level++;
            levelUp(level);
        }
    }

    public void levelUp(int level) {
        LevelEntry entry = null;
        for (LevelEntry e : levelTable) {
            if (e.level == level) { entry = e; break; }
        }
        if (entry == null) return;

        maxHP    += entry.hpBonus;
        atk      += entry.atkBonus;
        spd      += entry.spdBonus;
        defense  += entry.defenseBonus;
        maxMana  += entry.manaBonus;
        mana      = maxMana;

        if (entry.newAction != null)
            actions.add(entry.newAction);

        BattleUI.displayLevelUp(this, entry.newAction);
    }
}
