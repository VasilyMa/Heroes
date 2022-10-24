using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{

    public class RewardMB : MonoBehaviour
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color currentColor;
        [SerializeField] private Image _background;
        [SerializeField] private Image _image;
        [SerializeField] private Text _textAmount;
        [SerializeField] private Text _day;

        public void SetReward(int day, int currentStreak, Sprite sprite, string textAmount)
        {
            _image.sprite = sprite;
            _textAmount.text = textAmount.ToString();
            _day.text = $"DAY {day + 1}";
            _background.color = day == currentStreak ? currentColor : defaultColor;
        }
    }
}
    