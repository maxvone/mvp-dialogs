using System;
using CodeBase.AssetManagement;
using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UI.Mvp;
using CodeBase.UI.MvpImpl;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UiFactory : IUiFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly AllServices _services;

    public Transform UiRoot { get; private set; }

    public UiFactory(AllServices services)
    {
      _services = services;
      _assetProvider = _services.Single<IAssetProvider>();
    }

    public async UniTask CreateUIRoot()
    {
      GameObject root = await _assetProvider.Instantiate(AssetAddress.UiRootPath);
      UiRoot = root.transform;
    }

    public async UniTask<GameObject> CreatePictureScroll()
    {
      GameObject pictureScroll = await _assetProvider.Instantiate(AssetAddress.PictureScrollPath);

      PictureScrollView view = pictureScroll.GetComponent<PictureScrollView>();
      PictureScrollPresenter presenter = new(view, new PictureScrollPresenterPayload(this, _services.Single<IAssetProvider>()));
      presenter.InitializeAsync().Forget();

      pictureScroll.transform.SetParent(UiRoot, false);
      return pictureScroll;
    }

    public async UniTask<GameObject> CreatePictureCell()
    {
      GameObject cell = await _assetProvider.Instantiate(AssetAddress.PictureCellPath);
      return cell;
    }

    public async UniTask<GameObject> CreatePlayDialog(PuzzleData data)
    {
      GameObject dialog = await _assetProvider.Instantiate(AssetAddress.PlayDialogPath);

      PlayDialogView view = dialog.GetComponent<PlayDialogView>();
      PlayDialogPresenter presenter = new(view, new PlayDialogPresenterPayload(data,
        _services.Single<IAssetProvider>(),
        _services.Single<IStartPuzzleService>()
      ));
      presenter.InitializeAsync().Forget();

      dialog.transform.SetParent(UiRoot, false);
      return dialog;
    }
  }
}