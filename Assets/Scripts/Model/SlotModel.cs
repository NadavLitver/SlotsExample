using UnityEngine;
using controller;
namespace model
{
    /// <summary>
    /// holds data for single slot (which holds multiple reels which holds multiple symbols)
    /// slot model -> reel model -> symbol model
    /// </summary>
    [CreateAssetMenu(fileName = "New Symbol", menuName = "Slots/SlotModel")]

    public class SlotModel : ScriptableObject
    {
        [SerializeField] ReelModel[] reelModels;
        [SerializeField] int slotSpinCost;
        public ReelModel[] ReelModels { get => reelModels; }
        public int SlotSpinCost { get => slotSpinCost; }

        public bool HasEnoughPoints() => ScoreHandler.GetScore() >= SlotSpinCost;
    }
}