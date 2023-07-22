using System.Collections.Generic;
namespace controller
{
    /// <summary>
    /// default spinning strategy, a concrete implementation of "Spinning" logic for random spins
    /// </summary>
    public class RandomSpinningStrategy : ISpinningStrategy
    {
        public void SpinReels(List<ReelController> reels)
        {
            foreach (var reelController in reels)
            {
                reelController.SpinRandom();
            }
        }
    }
}