using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UI.MvpImpl;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUiFactory : IService
  {
    public Transform UiRoot { get; }

    UniTask CreateUIRoot();
    UniTask<PictureScrollPresenter> CreatePictureScroll();
    UniTask<GameObject> CreatePictureCell();
    UniTask<PlayDialogPresenter> CreatePlayDialog(PuzzleData data);
  }
}