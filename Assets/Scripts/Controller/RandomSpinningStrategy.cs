using System.Collections.Generic;
namespace controller
{
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