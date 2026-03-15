using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUiFactory : IService
  {
    public Transform UiRoot { get; }

    UniTask CreateUIRoot();
    UniTask<GameObject> CreatePictureScroll();
    UniTask<GameObject> CreatePictureCell();
  }
}