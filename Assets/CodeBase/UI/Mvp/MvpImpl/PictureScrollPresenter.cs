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
    private readonly Dictionary<PuzzleData, PictureCellView> _cellViews = new();
    private readonly IAssetProvider _assetProvider;
    private readonly IUiFactory _uiFactory;

    public PictureScrollPresenter(PictureScrollView view, PictureScrollPresenterPayload payload) : base(view)
    {
      _uiFactory = payload.UiFactory;
      _assetProvider = payload.AssetProvider;

    }

    public override async UniTask InitializeAsync()
    {
      await InitializeScrollView();
      foreach (var dataByView in _cellViews)
      {
        PuzzleData data = dataByView.Key;
        PictureCellView cellView = dataByView.Value;

        cellView.Button.onClick.AddListener(() =>
        {
          OpenPlayDialog(data);
        });
      }
    }

    private void OpenPlayDialog(PuzzleData data)
    {
      _uiFactory.CreatePlayDialog(data).Forget();
    }

    public override void Dispose()
    {
      foreach (PictureCellView cellView in _cellViews.Values)
        cellView.Button.onClick.RemoveAllListeners();
    }

    private async UniTask InitializeScrollView()
    {
      await _assetProvider.Load<PuzzlesStaticData>(AssetAddress.PuzzlesStaticDataPath)
        .ContinueWith(async data =>
      {
        foreach (PuzzleData puzzleData in data.Puzzles)
        {
          await _uiFactory.CreatePictureCell().ContinueWith(async cellGo =>
          {
            PictureCellView cellView = cellGo.GetComponent<PictureCellView>();
            cellGo.transform.SetParent(View.transform, false);

            await _assetProvider.Load<Texture2D>(puzzleData.Path).ContinueWith(texture =>
             {
               cellView.SetImage(texture);
             });

            _cellViews.Add(puzzleData, cellView);
          });
        }
      });
    }
  }
}