import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

public class TurnManager {

    public List<Character> allUnits  = new ArrayList<>();
    public Queue<Character> turnOrder = new LinkedList<>();
    public int totalTurns = 0;

    public void calculateOrder() {
        turnOrder.clear();
        List<Character> alive = new ArrayList<>();
        for (Character u : allUnits)
            if (u != null && u.isAlive) alive.add(u);

        // Sort by spd descending
        alive.sort((a, b) -> b.spd - a.spd);

        turnOrder.addAll(alive);
    }

    public Character nextTurn() {
        if (turnOrder.isEmpty()) startRound();
        removeDeadCharacters();
        Character unit = turnOrder.poll();
        totalTurns++;
        BattleUI.showTurnStart(unit);
        return unit;
    }

    public void removeDeadCharacters() {
        allUnits.removeIf(u -> u == null || !u.isAlive);

        List<Character> remaining = new ArrayList<>();
        for (Character u : turnOrder)
            if (u != null && u.isAlive) remaining.add(u);
        turnOrder.clear();
        turnOrder.addAll(remaining);
    }

    public void startRound() {
        removeDeadCharacters();
        calculateOrder();
    }

    public void addUnit(Character c) {
        if (c == null) return;
        if (!allUnits.contains(c)) allUnits.add(c);
    }

    public void clearUnits() {
        allUnits.clear();
        turnOrder.clear();
        totalTurns = 0;
    }
}
