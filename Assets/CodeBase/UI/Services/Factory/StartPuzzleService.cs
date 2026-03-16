using CodeBase.StaticData;

namespace CodeBase.UI.Services.Factory
{
  public class StartPuzzleService : IStartPuzzleService
  {
    private IStartPuzzlePaymentStrategy _paymentStrategy = new StartPuzzleFreePaymentStrategy();

    public bool TryStartPuzzle(PuzzleData puzzleData) =>
      _paymentStrategy.TryPay(puzzleData);

    public void ChangePaymentStrategy(IStartPuzzlePaymentStrategy paymentStrategy) =>
      _paymentStrategy = paymentStrategy;
  }
}