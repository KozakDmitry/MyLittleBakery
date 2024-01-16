using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infostructure.Services.ProgressService
{
    public interface IProgressService : IService
    {
        PlayerData playerData { get; set; }
    }
}