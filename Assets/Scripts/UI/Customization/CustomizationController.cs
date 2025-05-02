using System;
using System.Collections.Generic;
using System.Linq;
using EdCon.MiniGameTemplate.UI.Specifics;
using UnityEngine;

namespace EdCon.MiniGameTemplate.UI.Customization
{
    public class CustomizationController : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        [SerializeField] private GameObject _propertyPanel;
        [SerializeField] private UISlider _scaleBar;
        [SerializeField] private UISlider _opacityBar;
        [SerializeField] private UIControl _backgroundControl;
        [SerializeField] private UIAlert _uiAlert;
        
        private ICustomizationElement _selectedElement;
        private UICustomizationDragHandler _dragHandler;
        
        private HashSet<ICustomizationElement> _customizationElements = new HashSet<ICustomizationElement>();
        
        public ICustomizationElement SelectedElement => _selectedElement;
        
        public event Action<ICustomizationElement> OnControlSelected;
        
        private void OnEnable()
        {
            _propertyPanel.gameObject.SetActive(false);
            
            foreach (var uiElement in _uiController.UIElements) ElementAddedHandle(uiElement);
            
            _uiController.OnElementAdded += ElementAddedHandle;
            _uiController.OnElementRemoved += ElementRemovedHandle;
            
            _backgroundControl.OnClick += ElementClickHandle;
        }

        private void OnDisable()
        {
            ElementClickHandle(null);
            
            _customizationElements.ToList().ForEach(RemoveControl);
            _customizationElements.Clear();
            
            _uiController.OnElementAdded -= ElementAddedHandle;
            _uiController.OnElementRemoved -= ElementRemovedHandle;
            
            _backgroundControl.OnClick -= ElementClickHandle;
        }

        private void ElementAddedHandle(UIElement uiElement)
        {
            if (uiElement is ICustomizationElement customizationElement)
            {
                AddControl(customizationElement);
            }
        }

        private void ElementRemovedHandle(UIElement uiElement)
        {
            if (uiElement is ICustomizationElement customizationElement)
            {
                RemoveControl(customizationElement);
            }
        }

        public void AddControl(ICustomizationElement element)
        {
            if (element == null) return;
            
            if(_customizationElements.Add(element))
            {
                element.OnClick += ElementClickHandle;
            }
        }

        public void RemoveControl(ICustomizationElement element)
        {
            if (element == null) return;

            if (_customizationElements.Remove(element))
            {
                element.OnClick -= ElementClickHandle;

                if (element == _selectedElement)
                {
                    ElementClickHandle(null);
                }
            }
        }

        private void ElementClickHandle(ICustomizationElement element)
        {
            if(_selectedElement == element) return;
            
            if (_selectedElement != null)
            {
                _propertyPanel.gameObject.SetActive(false);
                _scaleBar.OnValueChanged -= ScaleValueChangeHandle;
                _opacityBar.OnValueChanged -= OpacityValueChangeHandle;
                
                Destroy(_dragHandler);
            }
            
            _selectedElement = ReferenceEquals(element, _backgroundControl) ? null : element;

            if (_selectedElement != null)
            {
                _scaleBar.SetValue(_selectedElement.Scale.x);
                _opacityBar.SetValue(_selectedElement.Opacity);
                
                _propertyPanel.gameObject.SetActive(true);
                
                _scaleBar.OnValueChanged += ScaleValueChangeHandle;
                _opacityBar.OnValueChanged += OpacityValueChangeHandle;

                _dragHandler = _selectedElement.UIElement.gameObject.AddComponent<UICustomizationDragHandler>();
                
                _dragHandler.Initialize(_selectedElement, _uiController.Canvas);
            }

            OnControlSelected?.Invoke(_selectedElement);
        }

        private void ScaleValueChangeHandle(float value)
        {
            _selectedElement?.SetScale(Vector3.one * value);
        }

        private void OpacityValueChangeHandle(float value)
        {
            _selectedElement?.SetOpacity(value);
        }
        
        public void Save()
        {
            ElementClickHandle(null);
            _uiController.Save();
            _uiAlert.ShowMessage("Layout Scheme Saved");
        }

        public void ResetSave()
        {
            ElementClickHandle(null);
            _uiController.ResetSave();
            _uiAlert.ShowMessage("Layout Scheme Reset");
        }
    }
}