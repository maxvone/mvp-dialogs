using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
    public class StartPuzzleCoinsPaymentStrategy : IStartPuzzlePaymentStrategy
  {
    public bool TryPay(PuzzleData puzzleData)
    {
      //Reduce coins, check if player has enough coins, etc

      UnityEngine.Debug.Log($"Starting puzzle {puzzleData.Title} with coins");
      return true;
    }
  }

}