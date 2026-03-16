using System;
using UnityEngine;

namespace CodeBase.UI.Mvp
{
  public abstract class BaseView : MonoBehaviour
  {
    public event Action OnShown;
    public event Action OnHidden;

    private void Awake()
    {
      gameObject.SetActive(false);
    }

    public virtual void Show()
    {
      gameObject.SetActive(true);
      OnShown?.Invoke();
    }

    public virtual void Hide()
    {
      Destroy(gameObject); //TODO: for the sake of simplicity I leave Destroy here, but in real project we should use object pool for views & presenters
      OnHidden?.Invoke();
    }
  }
}