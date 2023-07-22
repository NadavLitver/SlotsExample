using System.Collections.Generic;
namespace controller
{
    /// <summary>
    /// an interface for creating different "Spinning" concrete implementations
    /// </summary>
    public interface ISpinningStrategy
    {
        void SpinReels(List<ReelController> reels);
    }
}