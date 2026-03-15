using System;
using UnityEngine;

namespace CodeBase.UI.Mvp
{
  public abstract class BaseView : MonoBehaviour
  {
    public event Action OnShown;
    public event Action OnHidden;

    public virtual void Show()
    {
      gameObject.SetActive(true);
      OnShown?.Invoke();
    }

    public virtual void Hide()
    {
      gameObject.SetActive(false);
      OnHidden?.Invoke();
    }
  }
}