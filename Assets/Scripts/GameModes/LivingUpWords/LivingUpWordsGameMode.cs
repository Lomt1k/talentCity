using System.Collections.Generic;
using TalentCity.UI.Animations;
using TMPro;
using UnityEngine;

namespace TalentCity.GameModes.LivingUpWords
{
    public class LivingUpWordsGameMode : GameModeBase
    {
        public static string pathPrefab => "Prefabs/GameModes/LivingUpWords/LivingUpWordsGameMode";
        
        [SerializeField] private TextMeshProUGUI _currentWord;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TapToContinueAnim _tapToContinueAnim;

        private List<string> _usedWords;
        private int _totalWords = 50;

        public override void Execute()
        {
            _usedWords = new List<string>();
            SetupNextWord();
        }

        //unity event
        public void OnClickNextWord()
        {
            if (_usedWords.Count >= 50)
            {
                Exit();
            }
            else
            {
                SetupNextWord();
            }
        }

        private void SetupNextWord()
        {
            string nextWord;
            do
            {
                nextWord = LivingUpWordsArray.GetRandomWord();
            } while (_usedWords.Contains(nextWord));

            _currentWord.text = nextWord;
            _usedWords.Add(nextWord);
            _progressText.text = $"Нажмите для продолжения" +
                                 $"\n{_usedWords.Count} / {_totalWords}";
            _tapToContinueAnim.ReplayAnimation();
        }
    }
}
