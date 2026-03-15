import java.util.ArrayList;
import java.util.List;

public abstract class Character {

    public int hp, maxHP, atk, spd, exp, defense, level;
    public String name;
    public boolean isAlive = true;
    public List<CharacterAction> actions = new ArrayList<>();

    public Character(String name) {
        this.name = name;
    }

    public void takeDamage(int damage) {
        int oDefense = 0;
        if (this instanceof Playable p) {
            if (p.isDefending) {
                oDefense  = defense;
                defense  += level - 1;
            }
        }

        int damageTaken = Math.max(0, damage - this.defense);

        if (damageTaken <= 0) {
            BattleUI.displayMiss();
        } else {
            this.hp = Math.max(0, this.hp - damageTaken);
            BattleUI.displayDamage(this, damageTaken);
            if (this instanceof Playable p) {
                if (p.isDefending) {
                    p.isDefending = false;
                    p.defense     = oDefense;
                }
            }
        }

        if (this.hp <= 0) {
            this.isAlive = false;
            if (this instanceof Enemy enemy) {
                if (enemy.onDeath != null)
                    enemy.onDeath.onDeath(enemy.expOnDeath);
            }
        }
    }

    public abstract CharacterAction chooseAction();
}
