using System;
using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.UI.Specifics
{
    public class UISlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _textField;
        
        public Slider Slider => _slider;
        public float Value => _slider.value;
        
        public event Action<float> OnValueChanged;
        
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ValueChangedHandle);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ValueChangedHandle);
        }

        public void SetValue(float value)
        {
            SetViewValue(value);
            _slider.value = value;
        }

        private void ValueChangedHandle(float value)
        {
            SetViewValue(value);
            OnValueChanged?.Invoke(value);
        }

        private void SetViewValue(float value)
        {
            if (_textField != null)
            {
                int viewValue = (int)(value * 100);
                _textField.text = $"{viewValue}%";
            }
        }
    }
}