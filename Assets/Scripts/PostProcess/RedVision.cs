using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcess
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class RedVision : MonoBehaviour
    {
        private PostProcessVolume postProcessVolume;

        [SerializeField] [ColorUsage(false, true)]
        private Color _targetColor;

        #region RBG color fields & ranges
        private float R = 0;
        private float G = 0;
        private float B = 0;
        
        // Use to calculate the difference
        private float rangeForR = 0;
        private float rangeForG = 0;
        private float rangeForB = 0;

        #endregion

        void Start()
        {
            postProcessVolume = GetComponent<PostProcessVolume>();
            R = _targetColor.r;
            G = _targetColor.g;
            B = _targetColor.b;

            rangeForR = Color.white.r - _targetColor.r;
            rangeForB = Color.white.g - _targetColor.b;
            rangeForG = Color.white.b - _targetColor.g;
        }

        public void UpdatePostProcess(float currentFrenzyValue, float maxFrenzyValue = float.NaN)
        {
            if (float.IsNaN(maxFrenzyValue))
            {
                maxFrenzyValue = currentFrenzyValue;
            }

            ColorGrading colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            int colorGradingIndex = postProcessVolume.profile.settings.IndexOf(colorGrading);

            if (currentFrenzyValue < maxFrenzyValue)
            {
                Color newColor = colorGrading.colorFilter.value;
                newColor.r = Color.white.r -(currentFrenzyValue * rangeForR) / maxFrenzyValue;
                newColor.g = Color.white.g - (currentFrenzyValue * rangeForG) / maxFrenzyValue;
                newColor.b = Color.white.b - (currentFrenzyValue * rangeForB) / maxFrenzyValue;

                colorGrading.colorFilter.value = newColor;
            }
            else if (Math.Abs(currentFrenzyValue - maxFrenzyValue) < 0.1)
            {
                colorGrading.colorFilter.value = _targetColor;
            }

            postProcessVolume.profile.settings[colorGradingIndex] = colorGrading;
        }

    }
}