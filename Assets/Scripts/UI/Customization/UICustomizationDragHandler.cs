using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.UI.Customization
{
    public class UICustomizationDragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ICustomizationElement _customizationElement;
        private Vector2 _offset;
        private Canvas _canvas;

        public void Initialize(ICustomizationElement customizationElement, Canvas canvas)
        {
            _customizationElement = customizationElement;
            _canvas = canvas;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_customizationElement == null || _canvas == null) return;

            var elementRect = _customizationElement.UIElement.RectTransform;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint))
            {
                _offset = elementRect.anchoredPosition - localPoint;
            }
        }

        public void OnBeginDrag(PointerEventData eventData) { }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canvas == null || _customizationElement == null) return;

            var canvasRect = _canvas.transform as RectTransform;
            var elementRect = _customizationElement.UIElement.RectTransform;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out var localPoint))
            {
                Vector2 targetPosition = localPoint + _offset;

                targetPosition = GetClampScreen(targetPosition, canvasRect, elementRect);

                _customizationElement.SetPosition(targetPosition);
            }
        }

        private Vector2 GetClampScreen(Vector2 targetPosition, RectTransform canvasRect, RectTransform elementRect)
        {
            var position = targetPosition;
            
            Vector2 canvasSize = canvasRect.rect.size;
            Vector2 elementSize = elementRect.rect.size;

            float halfWidth = elementSize.x * 0.5f;
            float halfHeight = elementSize.y * 0.5f;

            float minX = -canvasSize.x * 0.5f + halfWidth;
            float maxX = canvasSize.x * 0.5f - halfWidth;
            float minY = -canvasSize.y * 0.5f + halfHeight;
            float maxY = canvasSize.y * 0.5f - halfHeight;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);

            return position;
        }


        public void OnEndDrag(PointerEventData eventData) { }
    }
}