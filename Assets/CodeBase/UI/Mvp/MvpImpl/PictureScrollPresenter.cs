using CodeBase.UI.Mvp;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.MvpImpl
{
  public class PictureScrollPresenterPayload : PresenterPayload
  {
    public IUiFactory UiFactory { get; }

    public PictureScrollPresenterPayload(IUiFactory uiFactory)
    {
      UiFactory = uiFactory;
    }
  }

  public class PictureScrollPresenter : BasePresenter<PictureScrollView>
  {
    private readonly IUiFactory _uiFactory;

    public PictureScrollPresenter(PictureScrollView view, PictureScrollPresenterPayload payload) : base(view)
    {
      _uiFactory = payload.UiFactory;
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
      UniTask op = _uiFactory.CreatePictureCell().ContinueWith(cellGo =>
      {
        cellGo.transform.SetParent(View.transform, false);
      });
    }
  }
}