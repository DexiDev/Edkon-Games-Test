using System;
using EdCon.MiniGameTemplate.Saves;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.UI.Customization
{
    public interface ICustomizationElement : IPointerDownHandler, ISaveable
    {
        public UIElement UIElement { get; }
        public Vector3 Scale { get; }
        public float Opacity { get; }
        
        public event Action<ICustomizationElement> OnClick;
        
        public void SetScale(Vector3 scale);

        public void SetOpacity(float opacity);

        public void SetPosition(Vector2 position);
    }
}