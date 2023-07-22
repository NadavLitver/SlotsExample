using System.Collections.Generic;
using UnityEngine;
namespace controller
{
    public class LosingStrategy : ISpinningStrategy
    {
        public void SpinReels(List<ReelController> reels)
        {
            int[] idArray = new int[reels[0].ReelModel.SymbolsData.Length];// I could make this a 2d array placing the for inside the for and being more dynamic if different reels have different id's, but because they don't for now I can save on performence and do less loops
            for (int i = 0; i < reels[0].ReelModel.SymbolsData.Length; i++)
            {
                idArray[i] = reels[0].ReelModel.SymbolsData[i].SymbolID;
            }
            int[] randomUniqueIDs = GetRandomUniqueNumbers(idArray, reels.Count);

            for (int i = 0; i < reels.Count; i++)
            {
                reels[i].SpinWithGoal(randomUniqueIDs[i]);
            }
        }
        int[] GetRandomUniqueNumbers(int[] inputArray, int count)
        {
            if (count >= inputArray.Length)
            {
                Debug.LogWarning("Requested count is greater than or equal to the array length. Returning the whole array.");
                return inputArray;
            }

            List<int> tempList = new List<int>(inputArray);
            int[] resultArray = new int[count];

            // Fisher-Yates Shuffle Algorithm
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, tempList.Count);
                resultArray[i] = tempList[randomIndex];
                tempList[randomIndex] = tempList[i];
            }

            return resultArray;
        }
    }
}