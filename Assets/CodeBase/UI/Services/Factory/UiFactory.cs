using System;
using CodeBase.AssetManagement;
using CodeBase.Services;
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
      presenter.Initialize();

      pictureScroll.transform.SetParent(UiRoot, false);
      return pictureScroll;
    }

    public async UniTask<GameObject> CreatePictureCell()
    {
      GameObject cell = await _assetProvider.Instantiate(AssetAddress.PictureCellPath);
      return cell;
    }
  }
}