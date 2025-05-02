using EdCon.MiniGameTemplate.UI;

namespace EdCon.MiniGameTemplate.Saves
{
    public interface ISaveable
    {
        public bool IsSaveable { get; }
        
        public string SaveID { get; }
        
        public ISaveData DefaultSaveData { get; }
        
        public ISaveData GetSaveData();
        
        public void SetSaveData(ISaveData saveData);
    }
}