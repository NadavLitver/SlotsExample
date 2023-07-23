using model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using view;

namespace controller
{
    /// <summary>
    /// win condition checker is responsible for checking the win, giving the points and notifing the popup
    /// </summary>
    public class WinConditionChecker
    {
        public event Action<int, bool> OnWin;
        private int basePrize;
        private BigWinPopupModel bigWinPopupModel;
        private BigWinPopupViewHandler bigWinPopupView;
        private const int minSymbolDuplicationForWin = 3;

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
                    if (kvp.Value >= minSymbolDuplicationForWin)//check for duplicates
                    {
                        if(kvp.Value == symbolsInRow.Length)//dont look for count consecutive symbols if the whole row is filled with same value anyway
                        {
                            anyRowFullyFilled = true;
                            allWinValues.Add(kvp.Value);
                            break;
                        }
                        consecutiveCounts.Clear(); // Clear the list before checking consecutive occurrences
                        int consecutiveCount = 0;

                        for (int i = 0; i < symbolsInRow.Length; i++)//go over all symbols in row 
                        {
                            if (symbolsInRow[i].GetID() == kvp.Key)//
                            {
                                consecutiveCount++;
                            }
                            else
                            {
                                if (consecutiveCount >= minSymbolDuplicationForWin)
                                {

                                    Debug.Log($"Duplicates and their counts:\n Symbol ID {kvp.Key} has {kvp.Value} occurrences.");
                                    for (int j = i - consecutiveCount; j <= i - 1; j++)
                                    {
                                        symbolsInRow[j].DrawCircleSetup();
                                    }
                                    for (int j = i; j < symbolsInRow.Length; j++)
                                    {
                                        symbolsInRow[j].CallGrayOutImage();
                                    }


                                    allWinValues.Add(kvp.Value); // Store the winning value
                                    break;
                                }
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
        public void PrizeOfWin(int _winValue, bool _anyRowFullyFilled)//give the user his points and notify the "popup" if needed
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