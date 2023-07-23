using System;
using System.Collections.Generic;
using UnityEngine;
using model;
using view;
using System.Linq;

namespace controller
{
    /// <summary>
    /// win condition checker is responsible for checking the win, giving the points and notifing the popup
    /// </summary>
    public class WinConditionChecker
    {
        public event Action<int,bool> OnWin;
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
        public void OnSpinEndCheckRows(SymbolView[][] symbolRows)
        {
            List<int> allWinValues = new List<int>(); // Store all winning values for each row
            bool anyRowFullyFilled = false;

            foreach (SymbolView[] symbolsInRow in symbolRows)
            {
                Dictionary<int, int> symbolCount = new Dictionary<int, int>();
                List<int> consecutiveCounts = new List<int>(); // Store the consecutive counts for each symbol ID

                // Count occurrences of each symbol ID in the row
                foreach (SymbolView symbol in symbolsInRow)
                {
                    int symbolId = symbol.GetID();
                    if (symbolCount.ContainsKey(symbolId))
                        symbolCount[symbolId]++;
                    else
                        symbolCount[symbolId] = 1;
                }

                // Find duplicates and their counts
                foreach (KeyValuePair<int, int> kvp in symbolCount)
                {
                    if (kvp.Value >= 3)
                    {
                        consecutiveCounts.Clear(); // Clear the list before checking consecutive occurrences
                        int consecutiveCount = 0;

                        for (int i = 0; i < symbolsInRow.Length; i++)
                        {
                            if (symbolsInRow[i].GetID() == kvp.Key)
                            {
                                consecutiveCount++;
                                if (consecutiveCount >= 3)
                                {
                                    if (kvp.Value < symbolsInRow.Length)
                                    {
                                        Debug.Log($"Duplicates and their counts:\n Symbol ID {kvp.Key} has {kvp.Value} occurrences.");
                                        for (int j = i - consecutiveCount + 1; j <= i; j++)
                                        {
                                            symbolsInRow[j].DrawCircleSetup();
                                        }
                                        for (int j = i + 1; j < symbolsInRow.Length && j <= i + 2; j++)
                                        {
                                            symbolsInRow[j].CallGrayOutImage();
                                        }
                                    }
                                    else
                                    {
                                        anyRowFullyFilled = true;
                                    }
                                    allWinValues.Add(kvp.Value); // Store the winning value
                                    break;
                                }
                            }
                            else
                            {
                                consecutiveCount = 0; // Reset consecutive count if symbols are not the same
                            }
                        }
                    }
                }
            }

            // Calculate the total win value
            int totalWinValue = allWinValues.Sum();

            // Invoke OnWin once with the total win value
            OnWin.Invoke(totalWinValue, anyRowFullyFilled);
        }
        public void PrizeOfWin(int _winValue,bool _anyRowFullyFilled)//give the user his points and notify the "popup" if needed
        {
            int scoreToAdd = _winValue * basePrize;
            ScoreHandler.AddToScore(scoreToAdd);
            Debug.Log($"Added to score! {scoreToAdd}");
            if (_anyRowFullyFilled)
            {
                bigWinPopupView.gameObject.SetActive(true);
                bigWinPopupView.Popup(bigWinPopupModel.ScaleGoal, bigWinPopupModel.ScaleToStart, bigWinPopupModel.DurationToScale, scoreToAdd);
            }
        }


    }
}