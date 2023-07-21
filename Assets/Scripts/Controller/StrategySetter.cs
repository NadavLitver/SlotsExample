
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrategySetter : MonoBehaviour
{
    [SerializeField] SlotController slotController;
    [SerializeField] TMP_Dropdown strategyDropdown;
    [SerializeField] StratagiesModel stratagiesModel;
    private List<ISpinningStrategy> strategies;
    private void Awake()
    {
        strategyDropdown.onValueChanged.AddListener(OnDropdownChoice);
        strategies.Add(stratagiesModel.RandomSpinningStrategy);
        strategies.Add(stratagiesModel.RandomSymbolThreeReelAmountStrategy);
        strategies.Add(stratagiesModel.RandomSymbolFourReelAmountStrategy);
        strategies.Add(stratagiesModel.RandomSymbolFiveReelAmountStrategy);
        strategies.Add(stratagiesModel.LosingStrategy);


    }

    private void OnDropdownChoice(int strategy)
    {
        slotController.SetSpinningStrategy(strategies[strategy]);
    }
}
