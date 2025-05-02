using System;
using EdCon.MiniGameTemplate.Saves;
using EdCon.MiniGameTemplate.UI.Customization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.UI.Specifics
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIControl : UIElement, ICustomizationElement
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private string _saveID;
        [SerializeField] private bool _isSaveable = true;
        
        private ISaveData _defaultSaveData;
        
        public UIElement UIElement => this;
        public Vector3 Scale => transform.localScale;
        public float Opacity => _canvasGroup.alpha;
        
        public bool IsSaveable => _isSaveable;
        public string SaveID => _saveID;
        
        public ISaveData DefaultSaveData => _defaultSaveData;

        public event Action<ICustomizationElement> OnClick;

        protected override void Awake()
        {
            base.Awake();
            _canvasGroup ??= GetComponent<CanvasGroup>();

            _defaultSaveData ??= GetSaveData();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetOpacity(float opacity)
        {
            _canvasGroup.alpha = opacity;
        }

        public void SetPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
        }
#endif

        public ISaveData GetSaveData()
        {
            return new CustomizationSaveData()
            {
                SaveID = SaveID,
                Position = RectTransform.anchoredPosition,
                Scale = Scale,
                Opacity = Opacity
            };
        }

        public void SetSaveData(ISaveData saveData)
        {
            _defaultSaveData ??= GetSaveData();
            
            if (saveData is CustomizationSaveData customizationSaveData)
            {
                SetPosition(customizationSaveData.Position);
                SetScale(customizationSaveData.Scale);
                SetOpacity(customizationSaveData.Opacity);
            }
        }
    }
}