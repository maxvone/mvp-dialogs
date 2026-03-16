using CodeBase.UI.Mvp;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.MvpImpl
{
  public class PictureCellView : BaseView
  {
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Button Button => _button;

    public void SetImage(Texture2D texture)
    {
      _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
  }
}