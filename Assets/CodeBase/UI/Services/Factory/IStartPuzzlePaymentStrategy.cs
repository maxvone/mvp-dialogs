using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
    public interface IStartPuzzlePaymentStrategy
  {
    bool TryPay(PuzzleData puzzleData);
  }

}