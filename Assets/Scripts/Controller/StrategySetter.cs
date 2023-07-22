
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using model;
namespace controller
{
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