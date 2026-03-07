using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoParejasPOO
{
    public class GameManager
    {
        public int currentStageIndex = 0;

        public List<Playable> playerCharacters = new List<Playable>();
        public List<Enemy> currentEnemies = new List<Enemy>();

        //public TurnManager turnManager = new TurnManager();
        Stages stagesList = new Stages();

        public void StartGame()
        {
            BattleUI.ShowGameIntro();
            CreateHero();
            LoadNextStage(); // Etapa 1
        }
        public void CreateHero()
        {
            playerCharacters.Clear();
            Playable hero = new Playable(BattleUI.CreateHeroName(), 10, 2, 5, 0, 1, 5, 1); // <-- Se definen las estadísticas bases del héroe.
            playerCharacters.Add(hero);
        }
        public void StartBattle()
        {
            Stage stage = stagesList.allStages[currentStageIndex];
            stage.GenerateEnemies();

            // Luego se añaden al turnManager junto al héroe...
        }
        public List<Character> GetAliveEnemies(Character requester)
        {
            if (requester is Playable)
                return currentEnemies.Where(e => e.isAlive).Cast<Character>().ToList();
            else
                return playerCharacters.Where(p => p.isAlive).Cast<Character>().ToList();
        }
        public CharacterAction RequestAction(Character character)
        {
            return character.ChooseAction();
        }
        public void ExecuteAction(Character user, CharacterAction action)
        {
            action.Execute(user, action.target);
        }
        public void LoadNextStage()
        {
            currentStageIndex++;
            if (currentStageIndex >= stagesList.allStages.Count)
            {
                BattleUI.ShowGameWin();
                StartGame();
            }
            else
            {
                BattleUI.ShowStageIntro(currentStageIndex);
                StartBattle();
            }
            // Cargar enemigos y demás elementos de la siguiente etapa
        }
        public void CheckBattleState()
        {
            if (currentEnemies.All(e => !e.isAlive))
            {
                BattleUI.ShowStageClear();
                LoadNextStage();
            }
            else if (playerCharacters.All(p => !p.isAlive))
            {
                BattleUI.ShowGameOver();
                StartGame();
            }
        }
    }
}
