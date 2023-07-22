using System;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionChecker
{
    public event Action<int> OnWin;
    private int basePrize;
    private BigWinPopupModel bigWinPopupModel;
    private BigWinPopupViewHandler bigWinPopupView;

    public WinConditionChecker(BigWinPopupModel _bigWinPopupModel, BigWinPopupViewHandler _bigWinPopupView)
    {
        basePrize = 5000;
        OnWin += PrizeOfWin;
        bigWinPopupModel = _bigWinPopupModel;
        bigWinPopupView = _bigWinPopupView;
    }
    public void OnSpinEnd(SymbolView[] finishingSymbols)
    {

        Dictionary<int, int> symbolCount = new Dictionary<int, int>();
        // Count occurrences of each symbol ID in the finishingSymbols array
        foreach (SymbolView symbol in finishingSymbols)
        {
            int symbolId = symbol.GetID();
            if (symbolCount.ContainsKey(symbolId))
                symbolCount[symbolId]++;
            else
                symbolCount[symbolId] = 1;
        }

        // Find duplicates and their counts

        foreach (var kvp in symbolCount)
        {
            if (kvp.Value >= 3)
            {
                Debug.Log($"Duplicates and their counts:\n Symbol ID {kvp.Key} has {kvp.Value} occurrences.");
                foreach (SymbolView symbol in finishingSymbols)
                {
                    if (symbol.GetID() == kvp.Key)
                    {
                        symbol.DrawCircleSetup();
                    }
                    else
                    {
                        symbol.CallGrayOutImage();
                    }
                }

                OnWin.Invoke(kvp.Value);

            }

        }



    }
    public void PrizeOfWin(int valueMultiplier)
    {
        int scoreToAdd = valueMultiplier * basePrize;
        ScoreHandler.AddToScore(scoreToAdd);
        Debug.Log($"Added to score! {scoreToAdd}");
        if (valueMultiplier == 5)
        {
            bigWinPopupView.gameObject.SetActive(true);
            bigWinPopupView.Popup(bigWinPopupModel.ScaleGoal, bigWinPopupModel.ScaleToStart, bigWinPopupModel.DurationToScale,scoreToAdd);
        }
    }


}
