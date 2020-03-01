using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleJauge : MonoBehaviour
{
    private float _maxJaugeValue;
    
    [SerializeField][Tooltip("Slider Image")]
    private Image _slidebar;
 
    void Awake(){
        _maxJaugeValue = _slidebar.fillAmount;
    }

    public void UpdateValue(float value, float maxValue)
    {
        float amout = value * _maxJaugeValue / maxValue;
        _slidebar.fillAmount = amout;
    }
}
