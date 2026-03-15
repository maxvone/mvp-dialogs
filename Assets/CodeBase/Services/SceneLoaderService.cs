namespace CodeBase.Services
{
  public class SceneLoaderService : ISceneLoaderService
  {
    public void Load(string sceneName) =>
      UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
  }
}