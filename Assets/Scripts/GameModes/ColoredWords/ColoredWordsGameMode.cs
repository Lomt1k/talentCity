using TalentCity.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TalentCity.GameModes.ColoredWords
{
    public class ColoredWordsGameMode : GameModeBase
    {
        public static string pathPrefab => "Prefabs/GameModes/ColoredWords/ColoredWordsGameMode";
        
        [SerializeField] private GridLayoutGroup _colorsGrid;
        [SerializeField] private TapToContinueAnim _tapToContinue;
        [SerializeField] private TextMeshProUGUI _tapToContinueText;

        private ColoredWord[] _coloredWords;
        private int _currentProgress;
        private int _totalProgress = 5;
        
        public override void Execute()
        {
            SetupColoredWordsArray();
            SetNextColoredWordsGroup();
        }

        private void SetupColoredWordsArray()
        {
            int coloredWordsCount = _colorsGrid.transform.childCount;
            _coloredWords = new ColoredWord[coloredWordsCount];
            for (int i = 0; i < coloredWordsCount; i++)
            {
                _coloredWords[i] = _colorsGrid.transform.GetChild(i).GetComponent<ColoredWord>();
            }
        }

        private void SetNextColoredWordsGroup()
        {
            for (int i = 0; i < _coloredWords.Length; i++)
            {
                SetNewRealColor(i);
                SetNewFakeColor(i);
            }
            _tapToContinue.ReplayAnimation();
            _tapToContinueText.text = $"Нажмите для продолжения" +
                                      $"\n{_currentProgress + 1} / {_totalProgress}";
        }

        private void SetNewRealColor(int index)
        {
            WordColor wordColor;
            do
            {
                wordColor = (WordColor) Random.Range(0, 5);
            } while ((index - 1 >= 0 && _coloredWords[index - 1].realColor == wordColor)
                || (index - 4 >= 0 && _coloredWords[index - 4].realColor == wordColor));
            
            _coloredWords[index].SetupRealColor(wordColor);
        }
        
        private void SetNewFakeColor(int index)
        {
            WordColor wordColor;
            do
            {
                wordColor = (WordColor) Random.Range(0, 5);
            } while ((index - 1 >= 0 && _coloredWords[index - 1].fakeColor == wordColor)
                     || (index - 4 >= 0 && _coloredWords[index - 4].fakeColor == wordColor)
                     || wordColor == _coloredWords[index].realColor);
            
            _coloredWords[index].SetupFakeColor(wordColor);
        }
        
        //unity event
        public void OnClickNextWord()
        {
            _currentProgress++;
            if (_currentProgress >= _totalProgress)
            {
                Exit();
            }
            else
            {
                SetNextColoredWordsGroup();
            }
        }
        
        
    }
}
