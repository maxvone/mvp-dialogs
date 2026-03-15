namespace CodeBase.Services
{
  public interface ISceneLoaderService : IService
  {
    void Load(string sceneName);
  }
}