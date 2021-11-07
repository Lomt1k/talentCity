using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TalentCity.GameModes.Multiplication
{
    public class InputLine
    {
        public int textLineIndex;
        public string expectedResult;
        public string enteredResult;
        public string emptySpaceTail;
        public int leftDigitsToEnter;
    }
    
    public class MultiplicationExampleGroup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _textLines;
        [SerializeField] private MultiplicationGameMode _multiplicationGameMode;
        [SerializeField] private TextMeshProUGUI _exampleResultInfo;

        private int _difficulty;
        private long _firstValue;
        private long _secondValue;

        private int _activeLinesCount;
        private List<InputLine> _inputLines;
        private int _currentInputLineIndex = 0;

        public void StartNextExample(int difficulty = 3)
        {
            _difficulty = difficulty;
            GenerateValues();
            SetupActiveTextLines();
            SetupExampleBodyTextLines();
            SetupInputLines();

            _exampleResultInfo.gameObject.SetActive(false);
            StartInputLine(0);
        }

        private void GenerateValues()
        {
            var minValue = (int)Mathf.Pow(10, _difficulty - 1);
            var maxValue = (int)Mathf.Pow(10, _difficulty);
            Debug.Log($"min: {minValue} max: {maxValue}");

            do
            {
                _firstValue = Random.Range(minValue, maxValue);
            } while (_firstValue % 10 == 0);
            do
            {
                _secondValue = Random.Range(minValue, maxValue);
            } while (_secondValue % 10 == 0 || _secondValue == _firstValue);
            
            Debug.Log($"Example: {_firstValue} x {_secondValue}");
        }

        private void SetupActiveTextLines()
        {
            int requiredLines = _difficulty + 5;
            for (int i = 0; i < _textLines.Length; i++)
            {
                bool activeState = i < requiredLines;
                _textLines[i].gameObject.SetActive(activeState);
            }
            _activeLinesCount = requiredLines;
        }

        private void SetupExampleBodyTextLines()
        {
            _textLines[0].text = _firstValue.ToString();
            _textLines[1].text = _secondValue.ToString();
            _textLines[2].text = "------------------";
        }

        private void SetupInputLines()
        {
            _inputLines = new List<InputLine>(_difficulty + 1);

            char[] secondValueChars = _secondValue.ToString().ToCharArray();
            var particalMultipliers = new List<int>();
            for (int i = secondValueChars.Length - 1; i >= 0; i--)
            {
                int multiplier = int.Parse(secondValueChars[i].ToString());
                particalMultipliers.Add(multiplier);
            }

            for (int i = 0; i < _difficulty; i++)
            {
                var particalRelult = (particalMultipliers[i] * _firstValue).ToString();
                string emptySpace = string.Empty;
                for (int j = 0; j < i; j++)
                {
                    emptySpace += "_";
                }
                
                var inputLine = new InputLine()
                {
                    textLineIndex = 3 + i,
                    emptySpaceTail = emptySpace,
                    expectedResult = particalRelult,
                    enteredResult = string.Empty,
                    leftDigitsToEnter = particalRelult.Length
                };
                _inputLines.Add(inputLine);
            }

            var expectedResult = (_firstValue * _secondValue).ToString();
            var resultInputLine = new InputLine()
            {
                textLineIndex = _activeLinesCount - 1,
                emptySpaceTail = string.Empty,
                expectedResult = expectedResult,
                enteredResult = string.Empty,
                leftDigitsToEnter = expectedResult.Length
            };
            _inputLines.Add(resultInputLine);
        }

        private void StartInputLine(int lineIndex)
        {
            _currentInputLineIndex = lineIndex;

            //показываем уже введенные строки
            for (int line = 0; line < _currentInputLineIndex; line++)
            {
                int textLineIndex = _inputLines[line].textLineIndex;
                _textLines[textLineIndex].text = _inputLines[line].enteredResult + "<color=white>" + _inputLines[line].emptySpaceTail;
            }

            //показываем нижнюю разделительную черту если осталось посчитать итоговый результат (сумма промежуточных значений)
            _textLines[_activeLinesCount - 2].text = _currentInputLineIndex == _inputLines.Count - 1
                ? "------------------"
                : "<color=white>_";
            
            //скрываем те линии, до которых еще не дошел ввод
            for (int line = _currentInputLineIndex + 1; line < _inputLines.Count; line++)
            {
                int textLineIndex = _inputLines[line].textLineIndex;
                _textLines[textLineIndex].text = "<color=white>_";
            }
            
            RefreshCurrentInputLine();
        }

        private void RefreshCurrentInputLine()
        {
            var currentInputLine = _inputLines[_currentInputLineIndex];
            string textToShow = currentInputLine.enteredResult + "<color=white>" + currentInputLine.emptySpaceTail;
            if (currentInputLine.leftDigitsToEnter > 0)
            {
                textToShow = "<color=#007FFF>?</color>" + textToShow;

                //если осталось только посчитать итоговый результат (этап суммирования строк)
                if (_currentInputLineIndex == _inputLines.Count - 1)
                {
                    var columnIndex = currentInputLine.expectedResult.Length - currentInputLine.leftDigitsToEnter;
                    HighlightColumn(columnIndex);
                }
            }
            
            int textLineIndex = currentInputLine.textLineIndex;
            _textLines[textLineIndex].text = textToShow;
        }

        private void HighlightColumn(int columnIndex)
        {
            for (int i = 0; i < _inputLines.Count - 1; i++)
            {
                var textLineIndex = _inputLines[i].textLineIndex;
                string highlightedResult = _inputLines[i].enteredResult + _inputLines[i].emptySpaceTail;
                if (highlightedResult.Length > columnIndex)
                {
                    int invertedIndex = highlightedResult.Length - columnIndex - 1;
                    char symbol = highlightedResult[invertedIndex];
                    if (char.IsDigit(symbol))
                    {
                        highlightedResult = highlightedResult.Insert(invertedIndex + 1, "</color>");
                        highlightedResult = highlightedResult.Insert(invertedIndex, "<color=#007FFF>");
                    }
                }
                
                if (_inputLines[i].emptySpaceTail.Length > 0)
                {
                    highlightedResult = highlightedResult.Insert(highlightedResult.Length - _inputLines[i].emptySpaceTail.Length, "<color=white>");
                }

                _textLines[textLineIndex].text = highlightedResult;
            }
        }
        
        private void DisableColumnHighlighting()
        {
            for (int i = 0; i < _inputLines.Count - 1; i++)
            {
                var textLineIndex = _inputLines[i].textLineIndex;
                string unhighlightedResult = _inputLines[i].enteredResult + "<color=white>" + _inputLines[i].emptySpaceTail;
                _textLines[textLineIndex].text = unhighlightedResult;
            }
        }

        public void OnDigitInput(int digit)
        {
            var currentInputLine = _inputLines[_currentInputLineIndex];
            currentInputLine.enteredResult = digit + currentInputLine.enteredResult;
            currentInputLine.leftDigitsToEnter--;

            RefreshCurrentInputLine();
            if (currentInputLine.leftDigitsToEnter < 1)
            {
                _currentInputLineIndex++;
                if (_currentInputLineIndex < _inputLines.Count)
                {
                    StartInputLine(_currentInputLineIndex);
                }
                else
                {
                    DisableColumnHighlighting();
                    OnCompleteExample();
                }
            }
        }
        

        public void OnCompleteExample()
        {
            var resultLine = _inputLines[_inputLines.Count - 1];
            bool success = resultLine.enteredResult.Equals(resultLine.expectedResult);

            if (!success)
            {
                int correctDigits = 0;
                string textToShow = string.Empty;
                for (int i = 0; i < resultLine.enteredResult.Length; i++)
                {
                    bool isCorrectDigit = resultLine.enteredResult[i] == resultLine.expectedResult[i];
                    textToShow += isCorrectDigit ? string.Empty : "<color=red>";
                    textToShow += resultLine.enteredResult[i];
                    textToShow += isCorrectDigit ? string.Empty : "</color>";

                    if (isCorrectDigit)
                        correctDigits++;
                }
                
                int textLineIndex = resultLine.textLineIndex;
                _textLines[textLineIndex].text = textToShow;

                _exampleResultInfo.text = "Результат: " +
                                          $"\n<color=orange>{correctDigits}</color> / {resultLine.expectedResult.Length}";
            }
            else
            {
                _exampleResultInfo.text = "Результат: " +
                                          "\n<color=#008D00>Решено верно!</color>";
            }
            
            _exampleResultInfo.gameObject.SetActive(true);
            _multiplicationGameMode.OnCompleteExample();
        }

        //unity event
        public void RestartExample()
        {
            SetupInputLines();
            StartInputLine(0);
        }
        
        
    }
}
