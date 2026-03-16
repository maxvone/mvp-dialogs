using CodeBase.AssetManagement;
using CodeBase.StaticData;
using CodeBase.UI.Mvp;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.MvpImpl
{
  public class PlayDialogPresenterPayload : PresenterPayload
  {
    public PuzzleData PuzzleData { get; private set; }
    public IAssetProvider AssetProvider { get; private set; }
    public IStartPuzzleService StartPuzzleService { get; private set; }

    public PlayDialogPresenterPayload(PuzzleData puzzleData, IAssetProvider assetProvider, IStartPuzzleService startPuzzleService)
    {
      PuzzleData = puzzleData;
      AssetProvider = assetProvider;
      StartPuzzleService = startPuzzleService;
    }
  }

  public class PlayDialogPresenter : BasePresenter<PlayDialogView>
  {
    private readonly PlayDialogPresenterPayload _payload;
    private IAssetProvider _assetProvider;
    private IStartPuzzleService _startPuzzleService;

    public PlayDialogPresenter(PlayDialogView view, PlayDialogPresenterPayload payload) : base(view)
    {
      _payload = payload;
      _assetProvider = payload.AssetProvider;
      _startPuzzleService = payload.StartPuzzleService;
    }

    public override async UniTask InitializeAsync()
    {
      LoadPuzzleImage();
      View.PlayButtonFree.onClick.AddListener(HandlePlayFreeButtonClicked);
      View.PlayButtonCoins.onClick.AddListener(HandlePlayCoinsButtonClicked);
      View.PlayButtonAds.onClick.AddListener(HandlePlayAdsButtonClicked);
    }

    public override void Dispose()
    {
      View.PlayButtonFree.onClick.RemoveListener(HandlePlayFreeButtonClicked);
      View.PlayButtonCoins.onClick.RemoveListener(HandlePlayCoinsButtonClicked);
      View.PlayButtonAds.onClick.RemoveListener(HandlePlayAdsButtonClicked);
    }

    private void LoadPuzzleImage()
    {
      _assetProvider.Load<Texture2D>(_payload.PuzzleData.Path)
        .ContinueWith(texture =>
        {
          Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
          View.SetPuzzleData(sprite, _payload.PuzzleData.Title);
        }).Forget();
    }

    private void HandlePlayFreeButtonClicked()
    {
      _startPuzzleService.ChangePaymentStrategy(new StartPuzzleFreePaymentStrategy());
      _startPuzzleService.TryStartPuzzle(_payload.PuzzleData);
    }

    private void HandlePlayCoinsButtonClicked()
    {
      _startPuzzleService.ChangePaymentStrategy(new StartPuzzleCoinsPaymentStrategy());
      _startPuzzleService.TryStartPuzzle(_payload.PuzzleData);
    }

    private void HandlePlayAdsButtonClicked()
    {
      _startPuzzleService.ChangePaymentStrategy(new StartPuzzleAdsPaymentStrategy());
      _startPuzzleService.TryStartPuzzle(_payload.PuzzleData);
    }

  }
}