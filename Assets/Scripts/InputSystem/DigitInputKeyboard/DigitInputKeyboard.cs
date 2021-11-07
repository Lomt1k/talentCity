using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TalentCity.InputSystem.DigitInputKeyboard
{
    public class DigitInputKeyboard : MonoBehaviour
    {
        [SerializeField] private Button[] _digitButtons;

        public Action<int> onDigitInput;

        private void Start()
        {
            SetupInputEvents();
        }

        private void SetupInputEvents()
        {
            var digitsByButtons = new Dictionary<Button, int>();
            for (int i = 0; i <= 9; i++)
            {
                var button = _digitButtons[i];
                digitsByButtons[button] = i;
            }
            
            foreach (var button in _digitButtons)
            {
                button.onClick.AddListener(() => OnButtonPressed(digitsByButtons[button]));
            }
        }

        private void OnButtonPressed(int digit)
        {
            Debug.Log($"OnButtonPressed {digit}");
            onDigitInput?.Invoke(digit);
        }
        
    }
}

