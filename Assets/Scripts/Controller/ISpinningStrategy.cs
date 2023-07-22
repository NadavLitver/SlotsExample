using System.Collections.Generic;
namespace controller
{
    public interface ISpinningStrategy
    {
        void SpinReels(List<ReelController> reels);
    }
}