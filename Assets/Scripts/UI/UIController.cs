using System;
using System.Collections.Generic;
using System.Linq;
using EdCon.MiniGameTemplate.Saves;
using UnityEngine;

namespace EdCon.MiniGameTemplate.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _uiElementContainer;

        private HashSet<UIElement> _uiElements = new HashSet<UIElement>();

        public Canvas Canvas => _canvas;
        public HashSet<UIElement> UIElements => _uiElements;
        
        public event Action<UIElement> OnElementAdded;
        public event Action<UIElement> OnElementRemoved;
        
        private void OnEnable()
        {
            var uiControls = _uiElementContainer.GetComponentsInChildren<UIElement>();
            foreach (var uiControl in uiControls)
            {
                AddElement(uiControl);
            }
        }

        private void OnDisable()
        {
            foreach (var uiElement in _uiElements.ToList())
            {
                RemoveElement(uiElement);
            }
            _uiElements.Clear();
        }

        public void AddElement(UIElement element)
        {
            if (element == null) return;

            if (_uiElements.Add(element))
            {
                if (element is ISaveable saveleable)
                {
                    LoadSave(saveleable);
                }
                
                OnElementAdded?.Invoke(element);
            }
        }

        public void RemoveElement(UIElement element)
        {
            if (element == null) return;
            
            if (_uiElements.Remove(element))
            {
                OnElementRemoved?.Invoke(element);
            }
        }

        public void LoadSave(ISaveable saveable)
        {
            if (!saveable.IsSaveable) return;
            
            var saveData = SaveManager.GetData(saveable.SaveID, saveable.GetSaveData());
            
            saveable.SetSaveData(saveData);
        }
        
        public void Save()
        {
            foreach (var uiElement in _uiElements)
            {
                if (uiElement is ISaveable saveable && saveable.IsSaveable)
                {
                    var saveData = saveable.GetSaveData();
                    SaveManager.SetData(saveable.SaveID, saveData);
                }
            }
        }

        public void ResetSave()
        {
            foreach (var uiElement in _uiElements)
            {
                if (uiElement is ISaveable saveable && saveable.IsSaveable)
                {
                    saveable.SetSaveData(saveable.DefaultSaveData);
                }
            }
        }
    }
}