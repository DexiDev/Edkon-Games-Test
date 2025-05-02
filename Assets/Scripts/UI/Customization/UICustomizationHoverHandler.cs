using UnityEngine;

namespace EdCon.MiniGameTemplate.UI.Customization
{
    public class UICustomizationHoverHandler : MonoBehaviour
    {
        [SerializeField] private CustomizationController _customizationController;
        [SerializeField] private RectTransform _hoverContract;

        private RectTransform _hoverInstance;
        
        private void OnEnable()
        {
            ControlSelectedHandle(_customizationController.SelectedElement);
            _customizationController.OnControlSelected += ControlSelectedHandle;
        }

        private void OnDisable()
        {
            _customizationController.OnControlSelected -= ControlSelectedHandle;
            ControlSelectedHandle(null);

            if (_hoverInstance != null && _hoverInstance.gameObject != null)
            {
                Destroy(_hoverInstance.gameObject);
                _hoverInstance = null;
            }
        }

        private void ControlSelectedHandle(ICustomizationElement customizationElement)
        {
            if (_hoverInstance != null && _hoverInstance.gameObject != null && gameObject.activeInHierarchy)
            {
                _hoverInstance.gameObject.SetActive(false);
                _hoverInstance.SetParent(transform, true);
            }

            if (customizationElement != null)
            {
                if (_hoverInstance == null)
                {
                    _hoverInstance = Instantiate(_hoverContract, transform);
                }
                
                _hoverInstance.SetParent(customizationElement.UIElement.transform, true);
               
                _hoverInstance.anchorMin = Vector2.zero;
                _hoverInstance.anchorMax = Vector2.one;
                _hoverInstance.offsetMin = Vector2.zero;
                _hoverInstance.offsetMax = Vector2.zero;
                _hoverInstance.localScale = Vector3.one;
                
                _hoverInstance.gameObject.SetActive(true);
            }
        }
    }
}