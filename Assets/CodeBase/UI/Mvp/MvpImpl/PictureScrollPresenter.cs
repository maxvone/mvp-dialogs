using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.StaticData;
using CodeBase.UI.Mvp;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.MvpImpl
{
  public class PictureScrollPresenterPayload : PresenterPayload
  {
    public IUiFactory UiFactory { get; }
    public IAssetProvider AssetProvider { get; }

    public PictureScrollPresenterPayload(IUiFactory uiFactory, IAssetProvider assetProvider)
    {
      UiFactory = uiFactory;
      AssetProvider = assetProvider;
    }
  }

  public class PictureScrollPresenter : BasePresenter<PictureScrollView>
  {
    private readonly List<PictureCellView> _cellViews = new();
    private readonly IUiFactory _uiFactory;
    private readonly IAssetProvider _assetProvider;

    public PictureScrollPresenter(PictureScrollView view, PictureScrollPresenterPayload payload) : base(view)
    {
      _uiFactory = payload.UiFactory;
      _assetProvider = payload.AssetProvider;

    }

    public override void Initialize()
    {
      InitializeScrollView();
    }

    public override void Dispose()
    {
    }

    private void InitializeScrollView()
    {
      var puzzleData = _assetProvider.Load<PuzzlesStaticData>(AssetAddress.PuzzlesStaticDataPath)
        .ContinueWith(data =>
      {
        foreach (PuzzleData puzzleData in data.Puzzles)
        {
          UniTask op = _uiFactory.CreatePictureCell().ContinueWith(async cellGo =>
          {
            PictureCellView cellView = cellGo.GetComponent<PictureCellView>();
            cellGo.transform.SetParent(View.transform, false);

            await _assetProvider.Load<Texture2D>(puzzleData.Path).ContinueWith(texture =>
             {
               cellView.SetImage(texture);
             });

            _cellViews.Add(cellView);
          });

        }
      });
    }
  }
}