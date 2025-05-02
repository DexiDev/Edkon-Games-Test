using EdCon.MiniGameTemplate.Saves;
using UnityEngine;

namespace EdCon.MiniGameTemplate.UI.Customization
{
    public class CustomizationSaveData : ISaveData
    {
        public string SaveID { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public float Opacity { get; set; }
    }
}