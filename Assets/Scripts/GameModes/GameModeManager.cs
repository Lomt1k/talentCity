using System.Collections.Generic;
using TalentCity.GameModes.ColoredWords;
using TalentCity.GameModes.LivingUpWords;
using TalentCity.GameModes.Multiplication;
using UnityEngine;

namespace TalentCity.GameModes
{
    public enum GameMode
    {
        LivingUpWords = 0,
        ColoredWords = 1,
        Multiplication = 2
    }
    
    public class GameModeManager : MonoBehaviour
    {
        public GameModeManager instance;

        private Dictionary<GameMode, string> _gameModesPrefabsDictionary;

        private void Awake()
        {
            instance = this;
            InitGameModesDictionary();
        }

        private void InitGameModesDictionary()
        {
            _gameModesPrefabsDictionary = new Dictionary<GameMode, string>()
            {
                {GameMode.LivingUpWords, LivingUpWordsGameMode.pathPrefab},
                {GameMode.ColoredWords, ColoredWordsGameMode.pathPrefab},
                {GameMode.Multiplication, MultiplicationGameMode.pathPrefab},
            };
        }
        

        public void ExecuteGameMode(GameMode gameMode)
        {
            var pathPrefab = _gameModesPrefabsDictionary[gameMode];
            var objectPrefab = Resources.Load<GameObject>(pathPrefab);
            var gameModeBase = Instantiate(objectPrefab, transform).GetComponent<GameModeBase>();
            gameModeBase.Execute();
        }

        //unity event
        public void StartLivingUpWords()
        {
            ExecuteGameMode(GameMode.LivingUpWords);
        }
        
        //unity event
        public void StartColoredWords()
        {
            ExecuteGameMode(GameMode.ColoredWords);
        }
        
        //unity event
        public void StartMultiplication()
        {
            ExecuteGameMode(GameMode.Multiplication);
        }
        
    
    }
}
