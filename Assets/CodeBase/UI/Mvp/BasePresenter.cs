using System;

namespace CodeBase.UI.Mvp
{
  public abstract class PresenterPayload {}

  public abstract class BasePresenter<TView> : IDisposable
      where TView : BaseView
  {
    protected readonly TView View;

    protected BasePresenter(TView view)
    {
      View = view != null ? view : throw new ArgumentNullException(nameof(view));
    }

    public abstract void Initialize();
    public abstract void Dispose();
  }
}