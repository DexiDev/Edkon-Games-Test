using UnityEngine;

namespace EdCon.MiniGameTemplate.UI
{
    public class UIElement : MonoBehaviour
    {
        protected RectTransform _rectTransform;
        
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
     
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}