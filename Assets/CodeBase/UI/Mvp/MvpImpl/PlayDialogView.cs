using CodeBase.UI.Mvp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.MvpImpl
{
  public class PlayDialogView : BaseView
  {
    [SerializeField] private Image _puzzleImage;
    [SerializeField] private TMP_Text _puzzleName;
    [SerializeField] private Button _playButtonFree;
    [SerializeField] private Button _playButtonCoins;
    [SerializeField] private Button _playButtonAds;

    public Button PlayButtonFree => _playButtonFree;
    public Button PlayButtonCoins => _playButtonCoins;
    public Button PlayButtonAds => _playButtonAds;

    public void SetPuzzleData(Sprite image, string name)
    {
      _puzzleImage.sprite = image;
      _puzzleName.text = name;
    }
  }
}