using UnityEngine;
[CreateAssetMenu(fileName = "New Symbol", menuName = "Slots/StratagiesData")]

public class StratagiesModel : ScriptableObject
{
    private RandomSpinningStrategy randomSpinningStrategy = new RandomSpinningStrategy();
    private RandomSymbolSetReelAmountStrategy randomSymbolThreeReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(3);
    private RandomSymbolSetReelAmountStrategy randomSymbolFourReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(4);
    private RandomSymbolSetReelAmountStrategy randomSymbolFiveReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(5);
    private LosingStrategy losingStrategy = new LosingStrategy();

    public RandomSpinningStrategy RandomSpinningStrategy { get => randomSpinningStrategy;}
    public RandomSymbolSetReelAmountStrategy RandomSymbolThreeReelAmountStrategy { get => randomSymbolThreeReelAmountStrategy;  }
    public RandomSymbolSetReelAmountStrategy RandomSymbolFourReelAmountStrategy { get => randomSymbolFourReelAmountStrategy; }
    public RandomSymbolSetReelAmountStrategy RandomSymbolFiveReelAmountStrategy { get => randomSymbolFiveReelAmountStrategy;}
    public LosingStrategy LosingStrategy { get => losingStrategy;}
}
