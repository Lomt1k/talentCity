using TMPro;
using UnityEngine;

namespace TalentCity.GameModes.ColoredWords
{
    public enum WordColor : int
    {
        Black = 0,
        Blue = 1,
        Red = 2,
        Green = 3,
        Yellow = 4
    }

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ColoredWord : MonoBehaviour
    {
        [HideInInspector] public WordColor realColor { get; private set; }
        [HideInInspector] public WordColor fakeColor { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _black;
        [SerializeField] private Color _blue;
        [SerializeField] private Color _red;
        [SerializeField] private Color _green;
        [SerializeField] private Color _yellow;

        public void SetupRealColor(WordColor color)
        {
            realColor = color;
            switch (color)
            {
                case WordColor.Black:
                    _text.color = _black;
                    break;
                case WordColor.Blue:
                    _text.color = _blue;
                    break;
                case WordColor.Red:
                    _text.color = _red;
                    break;
                case WordColor.Green:
                    _text.color = _green;
                    break;
                case WordColor.Yellow:
                    _text.color = _yellow;
                    break;
                
                default:
                    Debug.LogError($"unknown realColor {color}");
                    break;
            }
        }

        public void SetupFakeColor(WordColor color)
        {
            fakeColor = color;
            switch (color)
            {
                case WordColor.Black:
                    _text.text = "черный";
                    break;
                case WordColor.Blue:
                    _text.text = "синий";
                    break;
                case WordColor.Red:
                    _text.text = "красный";
                    break;
                case WordColor.Green:
                    _text.text = "зеленый";
                    break;
                case WordColor.Yellow:
                    _text.text = "желтый";
                    break;
                
                default:
                    Debug.LogError($"unknown fakeColor {color}");
                    break;
            }
        }
        
    }
}
