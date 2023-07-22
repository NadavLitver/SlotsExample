
using model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace controller
{
    /// <summary>
    /// StrategySetter is responsible for setting the spinning strategy for the slot controller (the dropdown)
    /// </summary>
    public class StrategySetter : MonoBehaviour
    {
        [SerializeField] SlotController slotController;
        [SerializeField] TMP_Dropdown strategyDropdown;
        [SerializeField] StratagiesModel stratagiesModel;
        private List<ISpinningStrategy> strategies;
        private void Awake()
        {
            strategyDropdown.onValueChanged.AddListener(OnDropdownChoice);
            strategies = new List<ISpinningStrategy>
        {
            stratagiesModel.RandomSpinningStrategy,
            stratagiesModel.RandomSymbolThreeReelAmountStrategy,
            stratagiesModel.RandomSymbolFourReelAmountStrategy,
            stratagiesModel.RandomSymbolFiveReelAmountStrategy,
            stratagiesModel.LosingStrategy
        };


        }

        private void OnDropdownChoice(int strategy)
        {
            slotController.SetSpinningStrategy(strategies[strategy]);
        }
    }
}