namespace Assets.CodeBase.Infostructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void LoadAll();
        void SaveAll();
    }
}