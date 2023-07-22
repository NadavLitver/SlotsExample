using UnityEngine;
using controller;
namespace model
{
    [CreateAssetMenu(fileName = "New StratagiesData", menuName = "Slots/StratagiesData")]

    public class StratagiesModel : ScriptableObject
    {
        private RandomSpinningStrategy randomSpinningStrategy = new RandomSpinningStrategy();
        private RandomSymbolSetReelAmountStrategy randomSymbolThreeReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(3);
        private RandomSymbolSetReelAmountStrategy randomSymbolFourReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(4);
        private RandomSymbolSetReelAmountStrategy randomSymbolFiveReelAmountStrategy = new RandomSymbolSetReelAmountStrategy(5);
        private LosingStrategy losingStrategy = new LosingStrategy();


        public RandomSpinningStrategy RandomSpinningStrategy
        {
            get
            {
                randomSpinningStrategy ??= new RandomSpinningStrategy();
                return randomSpinningStrategy;
            }
        }

        public RandomSymbolSetReelAmountStrategy RandomSymbolThreeReelAmountStrategy
        {
            get
            {
                randomSymbolThreeReelAmountStrategy ??= new RandomSymbolSetReelAmountStrategy(3);
                return randomSymbolThreeReelAmountStrategy;
            }
        }

        public RandomSymbolSetReelAmountStrategy RandomSymbolFourReelAmountStrategy
        {
            get
            {
                randomSymbolFourReelAmountStrategy ??= new RandomSymbolSetReelAmountStrategy(4);
                return randomSymbolFourReelAmountStrategy;
            }
        }

        public RandomSymbolSetReelAmountStrategy RandomSymbolFiveReelAmountStrategy
        {
            get
            {
                randomSymbolFiveReelAmountStrategy ??= new RandomSymbolSetReelAmountStrategy(5);
                return randomSymbolFiveReelAmountStrategy;
            }
        }

        public LosingStrategy LosingStrategy
        {
            get
            {
                losingStrategy ??= new LosingStrategy();
                return losingStrategy;
            }
        }
    }
}