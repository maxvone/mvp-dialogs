using CodeBase.Services;
using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
  public interface IStartPuzzleService : IService
  {
    bool TryStartPuzzle(PuzzleData puzzleData);
    void ChangePaymentStrategy(IStartPuzzlePaymentStrategy paymentStrategy);
  }
}