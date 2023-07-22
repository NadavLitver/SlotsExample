using UnityEngine;
using controller;
namespace model
{
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