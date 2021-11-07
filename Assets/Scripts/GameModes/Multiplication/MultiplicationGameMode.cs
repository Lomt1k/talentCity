using TalentCity.InputSystem.DigitInputKeyboard;
using TalentCity.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TalentCity.GameModes.Multiplication
{
    public class MultiplicationGameMode : GameModeBase
    {
        public static string pathPrefab => "Prefabs/GameModes/Multiplication/MultiplicationGameMode";

        [SerializeField] private MultiplicationExampleGroup _exampleGroup;
        [SerializeField] private DigitInputKeyboard _digitInputKeyBoard;
        [SerializeField] private TapToContinueAnim _tapToContinueAnim;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Button _replayButton;

        private int _difficulty = 3;
        private int _currentProgress = 0;
        private readonly int _totalProgress = 3;
        
        public override void Execute()
        {
            _digitInputKeyBoard.onDigitInput += OnDigitInput;
        }

        private void OnDigitInput(int digit)
        {
            _exampleGroup.OnDigitInput(digit);
        }
        
        //unity event
        public void StartWithDifficulty3()
        {
            _difficulty = 3;
            StartNextExample();
        }
        
        //unity event
        public void StartWithDifficulty4()
        {
            _difficulty = 4;
            StartNextExample();
        }
        
        //unity event
        public void StartWithDifficulty5()
        {
            _difficulty = 5;
            StartNextExample();
        }

        //untity event
        public void StartNextExample()
        {
            if (_currentProgress < _totalProgress)
            {
                _tapToContinueAnim.transform.parent.gameObject.SetActive(false);
                _digitInputKeyBoard.gameObject.SetActive(true);
                _replayButton.gameObject.SetActive(true);
                
                _exampleGroup.StartNextExample(_difficulty);
            }
            else
            {
                Exit();
            }
        }

        public void OnCompleteExample()
        {
            _tapToContinueAnim.transform.parent.gameObject.SetActive(true);
            _tapToContinueAnim.ReplayAnimation();
            _digitInputKeyBoard.gameObject.SetActive(false);
            _replayButton.gameObject.SetActive(false);
            
            _currentProgress++;
            _progressText.text = $"Нажмите для продолжения" +
                                 $"\n{_currentProgress} / {_totalProgress}";
        }

        public override void Exit()
        {
            _digitInputKeyBoard.onDigitInput -= OnDigitInput;
            base.Exit();
        }
    }

}
