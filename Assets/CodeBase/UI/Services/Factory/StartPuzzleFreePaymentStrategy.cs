using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
    public class StartPuzzleFreePaymentStrategy : IStartPuzzlePaymentStrategy
  {
    public bool TryPay(PuzzleData puzzleData)
    {
      UnityEngine.Debug.Log($"Starting puzzle {puzzleData.Title} for free");
      return true;
    }
  }

}