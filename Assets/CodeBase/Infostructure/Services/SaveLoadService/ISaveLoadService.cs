using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infostructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        PlayerData LoadAll();
        void SaveAll();
    }
}