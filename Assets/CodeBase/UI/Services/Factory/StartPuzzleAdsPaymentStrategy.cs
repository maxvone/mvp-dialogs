using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
    public class StartPuzzleAdsPaymentStrategy : IStartPuzzlePaymentStrategy
  {
    public bool TryPay(PuzzleData puzzleData)
    {
      //Show ad, wait for it to finish, etc

      UnityEngine.Debug.Log($"Starting puzzle {puzzleData.Title} with ads");
      return true;
    }
  }

}