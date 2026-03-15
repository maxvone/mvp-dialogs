using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IState
  {
    private const string mainMenuSceneName = "MainMenuScene"; //TODO: Move to config

    private readonly GameStateMachine _stateMachine;

    public LoadLevelState(GameStateMachine gameStateMachine)
    {
      _stateMachine = gameStateMachine;
    }

    public async void Enter()
    {
      UniTask loadingMainMenuOperation = LoadMainMenuSceneAsync();
      await UniTask.WaitUntil(() => loadingMainMenuOperation.Status == UniTaskStatus.Succeeded);
      _stateMachine.Enter<MainMenuLoopState>();
    }

    public void Exit() { }

    private async UniTask LoadMainMenuSceneAsync()
    {
      AsyncOperation loadingSceneOperation = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);

      await loadingSceneOperation;

      Debug.Log($"Boot + Gameplay loaded! Active scene = {SceneManager.GetActiveScene().name}");

      Scene mainMenu = SceneManager.GetSceneByName(mainMenuSceneName);
      if (mainMenu.IsValid())
        SceneManager.SetActiveScene(mainMenu);
    }
  }
}