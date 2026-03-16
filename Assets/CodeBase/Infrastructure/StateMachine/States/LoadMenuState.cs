using CodeBase.UI.MvpImpl;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadMenuState : IState
  {
    private const string MainMenuSceneName = "MainMenuScene"; //TODO: Move to config

    private readonly GameStateMachine _stateMachine;
    private readonly IUiFactory _uiFactory;

    public LoadMenuState(GameStateMachine gameStateMachine, IUiFactory uiFactory)
    {
      _stateMachine = gameStateMachine;
      _uiFactory = uiFactory;
    }

    public async void Enter()
    {
      UniTask loadingMainMenuOperation = LoadMainMenuSceneAsync();
      await UniTask.WaitUntil(() => loadingMainMenuOperation.Status == UniTaskStatus.Succeeded);

      await InitUIRoot();
      await InitPictureScroll();

      _stateMachine.Enter<MainMenuLoopState>();
    }

    public void Exit() { }

    private async UniTask InitUIRoot() =>
      await _uiFactory.CreateUIRoot();

    private async UniTask InitPictureScroll()
    {
      PictureScrollPresenter presenter = await _uiFactory.CreatePictureScroll();
      presenter.Show();
    }

    private async UniTask LoadMainMenuSceneAsync()
    {
      AsyncOperation loadingSceneOperation = SceneManager.LoadSceneAsync(MainMenuSceneName, LoadSceneMode.Additive);

      await loadingSceneOperation;

      Debug.Log($"Boot + Menu loaded! Active scene = {SceneManager.GetActiveScene().name}");

      Scene mainMenu = SceneManager.GetSceneByName(MainMenuSceneName);
      if (mainMenu.IsValid())
        SceneManager.SetActiveScene(mainMenu);
    }
  }
}