import java.util.List;
import java.util.Scanner;

public class BattleUI {

    private static final Scanner scanner = new Scanner(System.in);

    public static void showGameIntro() {
        System.out.print("\033[H\033[2J"); // Este es el equivalente a un Console.Clear() con ANSI escape codes.
        System.out.flush();
        System.out.println("=== Bienvenido a THE DUNGEON ===");
        System.out.println("Preparárate para la batalla.");
        System.out.println("Presiona Enter para continuar...");
        scanner.nextLine();
    }

    public static String getHeroName() {
        System.out.print("Nombre del héroe: ");
        String name = scanner.nextLine();
        if (name == null || name.isEmpty()) name = "Héroe";
        return name;
    }

    public static void showEnemiesIntro(List<Enemy> enemies) {
        if (enemies == null || enemies.isEmpty()) {
            System.out.println("No hay enemigos en esta etapa.\n");
            return;
        }
        System.out.println("Enemigos visibles en la sala:");
        for (int i = 0; i < enemies.size(); i++) {
            Enemy e = enemies.get(i);
            System.out.println((i + 1) + ") " + e.name + " - Nivel " + e.level);
        }
        System.out.println("\nPulsa Enter para iniciar el combate.");
        scanner.nextLine();
    }

    public static void showTurnStart(Character unit) {
        if (unit instanceof Playable)
            System.out.println("¡Es tu turno!");
        else
            System.out.println("Es el turno de " + unit.name);
    }

    public static void displaySingleAttack(Character user, Character target, CharacterAction action) {
        System.out.println(user.name + " usa " + action.name + " sobre " + target.name + ".");
        scanner.nextLine();
    }

    public static void displayWideAttack(Character user, CharacterAction action) {
        System.out.println(user.name + " usa " + action.name + " sobre varios objetivos.");
    }

    public static void displayFireBall(Character user) {
        System.out.println("\u001B[35mEl cielo se enciende y todo retumba, " + user.name + " lanza una gigante bola de fuego sobre los enemigos\u001B[0m");
        scanner.nextLine();
    }

    public static void displayDefend(Playable p) {
        System.out.println("\u001B[36m" + p.name + " adopta una postura defensiva\u001B[0m");
    }

    public static void displayHeal(Character user, int healAmount) {
        System.out.println("\u001B[32m" + user.name + " se cura " + healAmount + " puntos de vida.\u001B[0m");
    }

    public static void displayDefeat(Character unit) {
        unit.isAlive = false;
        System.out.println("\u001B[31m" + unit.name + " ha sido eliminado.\u001B[0m");
        scanner.nextLine();
    }

    public static void showStageClear() {
        System.out.println("Etapa superada. Has limpiado la sala.");
        System.out.println("Presiona Enter para continuar...");
        scanner.nextLine();
    }

    public static void showGameOver() {
        System.out.print("\033[H\033[2J");
        System.out.flush();
        System.out.println("Juego terminado. Has sido derrotado.");
        System.out.println("Presiona Enter para reiniciar...");
        scanner.nextLine();
    }

    public static void showGameWin() throws InterruptedException {
        System.out.print("\033[H\033[2J");
        System.out.flush();
        String base = "¡Felicidades! Por tu perseveración has completado todas las etapas, tu premio es";
        System.out.println(base);         Thread.sleep(1000);
        System.out.println(base + ".");   Thread.sleep(1000);
        System.out.println(base + "..");  Thread.sleep(1000);
        System.out.println(base + "..."); Thread.sleep(3000);
        System.out.println("¡Una palmadita en la espalda!");
        System.out.println("Presiona Enter para volver a jugar.");
        scanner.nextLine();
    }

    public static CharacterAction getPlayerAction(Playable player) {
        while (true) {
            System.out.println("¿Qué hará " + player.name + "? (HP: " + player.hp + "/" + player.maxHP + ") - (Mana: " + player.mana + "/" + player.maxMana + ")");
            for (int i = 0; i < player.actions.size(); i++) {
                CharacterAction act = player.actions.get(i);
                System.out.println((i + 1) + ") " + act.name + "(Costo maná: " + act.manaCost + ")");
            }
            System.out.print("Opción: ");
            String input = scanner.nextLine();
            try {
                int idx = Integer.parseInt(input) - 1;
                if (idx >= 0 && idx < player.actions.size()) {
                    CharacterAction selected = player.actions.get(idx);
                    if (selected.manaCost > player.mana) {
                        System.out.println("No tienes suficiente mana para esa acción. Elige otra.");
                        continue;
                    }
                    return selected;
                }
            } catch (NumberFormatException ignored) {}
            System.out.println("\u001B[31mOpción inválida, intenta de nuevo.\u001B[0m");
        }
    }

    public static Character chooseSingleTarget(List<Character> targets) {
        if (targets == null || targets.isEmpty())
            throw new IllegalArgumentException("No hay objetivos disponibles.");
        if (targets.size() == 1) return targets.getFirst();

        System.out.println("Selecciona un objetivo:");
        for (int i = 0; i < targets.size(); i++) {
            Character t = targets.get(i);
            System.out.println((i + 1) + ") " + t.name + " - HP: " + t.hp + "/" + t.maxHP);
        }
        System.out.print("Opción: ");
        String input = scanner.nextLine();
        try {
            int idx = Integer.parseInt(input) - 1;
            if (idx >= 0 && idx < targets.size()) return targets.get(idx);
        } catch (NumberFormatException ignored) {}
        return targets.getFirst();
    }

    public static void displayMiss() {
        System.out.println("El ataque no atraviesa la defensa y no causa daño.\n");
    }

    public static void displayDamage(Character target, int damage) {
        System.out.println("\u001B[31m" + target.name + " recibe " + damage + " de daño.");
        System.out.println(target.name + " HP: " + target.hp + "/" + target.maxHP + "\u001B[0m");
        if (target.hp <= 0) displayDefeat(target);
        System.out.println();
    }

    public static void showNewStageIntro(int stageIndex) {
        System.out.print("\033[H\033[2J");
        System.out.flush();
        System.out.println("--- Etapa " + stageIndex + " ---");
        System.out.println("Los enemigos se acercan y se preparan.");
        System.out.println("Presiona Enter para comenzar la batalla...");
        scanner.nextLine();
    }

    public static void displayExperienceGain(Playable player, int exp) {
        System.out.println("\u001B[33m" + player.name + " gana " + exp + " puntos de experiencia!\u001B[0m");
        System.out.println("\u001B[33mexp total: " + player.exp + "\u001B[0m");
    }

    public static void displayLevelUp(Playable player, CharacterAction action) {
        System.out.println("\u001B[33m" + player.name + " sube al nivel " + player.level + "!");
        System.out.println("HP: " + player.hp + "/" + player.maxHP + "  ATK: " + player.atk + "  SPD: " + player.spd + "  Mana: " + player.mana + "/" + player.maxMana + "\u001B[0m");
        if (action != null) {
            System.out.println("¡" + player.name + " aprendió una nueva acción: " + action.name + "!");
            System.out.println(action.description);
        }
    }

    public static void showRespite() {
        System.out.print("\033[H\033[2J");
        System.out.flush();
        System.out.println("Después de la batalla, tienes un momento para descansar...");
        System.out.println("¡Recuperas tu vida y maná!");
        scanner.nextLine();
    }
}
