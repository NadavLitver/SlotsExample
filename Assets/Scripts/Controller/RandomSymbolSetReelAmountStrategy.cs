using System.Collections.Generic;
namespace controller
{
    public class RandomSymbolSetReelAmountStrategy : ISpinningStrategy
    {
        int amountOfWinningReels;
        public RandomSymbolSetReelAmountStrategy(int _amountOfWinningReels)
        {
            amountOfWinningReels = _amountOfWinningReels;
        }
        public void SpinReels(List<ReelController> reels)
        {
            int randomId = UnityEngine.Random.Range(1, reels[0].ReelModel.SymbolsData.Length + 1);
            for (int i = 0; i < reels.Count; i++)
            {
               
                if (i < amountOfWinningReels)
                {
                    reels[i].SpinWithGoal(randomId);
                }
                else
                {
                    int otherRandomID = UnityEngine.Random.Range(1, reels[i].ReelModel.SymbolsData.Length + 1);
                    if (otherRandomID == randomId)
                    {
                        int tries = 10;
                        for (int j = 0; j < tries; j++)
                        {
                            otherRandomID = UnityEngine.Random.Range(1, reels[i].ReelModel.SymbolsData.Length + 1);
                            if (otherRandomID != randomId)
                            {
                                break;
                            }
                        }
                    }

                    reels[i].SpinWithGoal(otherRandomID);
                }
            }

        }
    }
}