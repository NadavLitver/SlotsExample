using System;
using System.Collections.Generic;
using UnityEngine;
using model;
using view;
namespace controller
{
    /// <summary>
    /// win condition checker is responsible for checking the win, giving the points and notifing the popup
    /// </summary>
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

            foreach (KeyValuePair<int,int> kvp in symbolCount)//kvp key is the "symbol" identified using an int id and the value is the amount it has repeated
            {
                if (kvp.Value >= 3)
                {
                    if(kvp.Value < 5)//the pop up screen anyways his hiding the symbols so we can draw the circles only on 3 or 4
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
                    }
                   

                    OnWin.Invoke(kvp.Value);

                }

            }



        }
        public void PrizeOfWin(int winValue)//give the user his points and notify the "popup" if needed
        {
            int scoreToAdd = winValue * basePrize;
            ScoreHandler.AddToScore(scoreToAdd);
            Debug.Log($"Added to score! {scoreToAdd}");
            if (winValue == 5)
            {
                bigWinPopupView.gameObject.SetActive(true);
                bigWinPopupView.Popup(bigWinPopupModel.ScaleGoal, bigWinPopupModel.ScaleToStart, bigWinPopupModel.DurationToScale, scoreToAdd);
            }
        }


    }
}