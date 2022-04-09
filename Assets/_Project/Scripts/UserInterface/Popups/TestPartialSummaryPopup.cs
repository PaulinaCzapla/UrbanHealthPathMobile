using System;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a test partial summary popup.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class TestPartialSummaryPopup : MonoBehaviour, IPopup
    {
        public RectTransform PopupArea { get; }
        [SerializeField] private Button[] _repetitionsButtons;
        [SerializeField] private TextMeshProUGUI _textRepetitions;

        [SerializeField] private Button[] _rangeOfMotionButtons;
        [SerializeField] private TextMeshProUGUI _textRangeOfMotion;

        [SerializeField] private Button[] _bottlesButtons;
        [SerializeField] private TextMeshProUGUI _textBottles;

        private string[] _motionRanges = {"mały", "średni", "duży"};
        private int _motionRangesIndex;

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnEnable()
        {
            _textRepetitions.text = "5";
            _textRangeOfMotion.text = _motionRanges[1];
            _motionRangesIndex = 1;
            _textBottles.text = "1,5";

            _repetitionsButtons[0].onClick.AddListener(OnButtonRepetitionsPrev);
            _repetitionsButtons[1].onClick.AddListener(OnButtonRepetitionsNext);
            _rangeOfMotionButtons[0].onClick.AddListener(OnButtonMotionRangePrev);
            _rangeOfMotionButtons[1].onClick.AddListener(OnButtonMotionRangeNext);
            _bottlesButtons[0].onClick.AddListener(OnButtonBottlesPrev);
            _bottlesButtons[1].onClick.AddListener(OnButtonBottlesNext);
        }

        private void OnDisable()
        {
            _repetitionsButtons[0].onClick.RemoveListener(OnButtonRepetitionsPrev);
            _repetitionsButtons[1].onClick.RemoveListener(OnButtonRepetitionsNext);
            _rangeOfMotionButtons[0].onClick.RemoveListener(OnButtonMotionRangePrev);
            _rangeOfMotionButtons[1].onClick.RemoveListener(OnButtonMotionRangeNext);
            _bottlesButtons[0].onClick.RemoveListener(OnButtonBottlesPrev);
            _bottlesButtons[1].onClick.RemoveListener(OnButtonBottlesNext);
        }

        private void OnButtonRepetitionsNext()
        {
            int curr = Int32.Parse(_textRepetitions.text);
            curr++;

            _textRepetitions.text = curr.ToString();
        }

        private void OnButtonRepetitionsPrev()
        {
            int curr = Int32.Parse(_textRepetitions.text);
            curr--;

            if (curr >= 0)
                _textRepetitions.text = curr.ToString();
        }

        private void OnButtonMotionRangeNext()
        {
            _motionRangesIndex++;

            if (_motionRangesIndex < _motionRanges.Length)
                _textRangeOfMotion.text = _motionRanges[_motionRangesIndex];
            else
                _motionRangesIndex--;
        }

        private void OnButtonMotionRangePrev()
        {
            _motionRangesIndex--;

            if (_motionRangesIndex >= 0)
                _textRangeOfMotion.text = _motionRanges[_motionRangesIndex];
            else
                _motionRangesIndex++;
        }

        private void OnButtonBottlesNext()
        {
            double curr = Convert.ToDouble(_textBottles.text);
            curr += 0.5;

            if (curr <= 5.0)
                _textBottles.text = curr.ToString("N1");
        }

        private void OnButtonBottlesPrev()
        {
            double curr = Convert.ToDouble(_textBottles.text);
            curr -= 0.5;

            if (curr >= 0.5)
                _textBottles.text = curr.ToString("N1");
        }

        public string GetBottles() => _textBottles.text;
        public string GetRepetitions() => _textRepetitions.text;
        public string GetMotionRange() => _textRangeOfMotion.text;
    }
}