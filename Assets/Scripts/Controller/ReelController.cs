using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    private SymbolController m_SymbolController;
    private ReelModel m_ReelModel;
    private bool isSpinning;
    public RectTransform ReelCenter { get => reelCenter; }
    [SerializeField] RectTransform reelCenter;
    [SerializeField] SymbolView m_SymbolViewPrefab;
    public void InitReel(ReelModel reelModel)
    {
        m_ReelModel = reelModel;
        m_SymbolController = new SymbolController();
        m_SymbolController.DisplayAndPositionSymbols(m_ReelModel.SymbolsData, m_ReelModel.DistanceBetweenSymbols, reelCenter, m_SymbolViewPrefab);
        SpinRandom();


    }
    public void SpinRandom()
    {
        int goalID = UnityEngine.Random.Range(1, 10);
        CallSpinReel(goalID);
        Debug.Log($"Reel{gameObject.name} is going to end on{m_ReelModel.SymbolsData[goalID - 1].SymbolName}");
    }
    public void CallSpinReel(int goalID)
    {
        if (!isSpinning)
        {
            _ = SpinReelRoutine(goalID);
        }
    }

    private async UniTask SpinReelRoutine(int goalID)
    {

        isSpinning = true;
        //init spinning variables
        float spinDistance = m_ReelModel.DistanceBetweenSymbols;
        float spinSpeed = m_ReelModel.SpinningSpeed;
        float OneSpinTime = spinDistance / spinSpeed;
        Vector2 topPosition = GetSymbolInHighestPosition();
        int SpinCounter = 0;

        while (isSpinning)
        {
            //init spin variables
            bool spinComplete = false;
            bool spinDone = false;
            //move each symbol downwards
            foreach (SymbolView item in m_SymbolController.SymbolViews)
            {

                float destination = item.transform.localPosition.y - spinDistance;
                item.transform.DOLocalMoveY(destination, OneSpinTime).SetEase(Ease.Linear).onComplete = () => spinComplete = true;
                if (AreStopConditionsAnswered(goalID, SpinCounter, item, destination))
                {
                    spinDone = true;
                }

            }

            await UniTask.WaitUntil(() => spinComplete == true);
            GetRectTransformWithLowestY().transform.localPosition = topPosition;
            SpinCounter++;
            await UniTask.NextFrame();

            if (spinDone)
            {
                isSpinning = false;
            }
        }


    }
    /// <summary>
    /// AreStopConditionsAnswered checks that 
    /// a. base amount of spins has been reached
    /// b. current item is the item to be stopped in the middle(item is the "Goal"
    /// c. item destination is the middle
    /// </summary>

    private bool AreStopConditionsAnswered(int goalID, int SpinCounter, SymbolView item, float destination)
    {
        return SpinCounter > m_ReelModel.DefaultSpinCount && item.GetID() == goalID && MathF.Abs(destination - ReelCenter.localPosition.y) < 0.1f;/// do tween had a 0.00003 error so I used the "absolute value" of the distance
    }
    /// <summary>
    /// get lowest symbol on reel
    /// </summary>
    public SymbolView GetRectTransformWithLowestY()
    {
        // Start by assuming the first element has the lowest Y value
        SymbolView lowestYSymbol = m_SymbolController.SymbolViews[0];
        float lowestY = lowestYSymbol.transform.position.y;

        for (int i = 1; i < m_SymbolController.SymbolViews.Count; i++)
        {
            float currentY = m_SymbolController.SymbolViews[i].transform.position.y;
            if (currentY < lowestY)
            {
                // Found a RectTransform with a lower Y value, update the lowestYRectTransform
                lowestYSymbol = m_SymbolController.SymbolViews[i];
                lowestY = currentY;
            }
        }

        return lowestYSymbol;
    }
    /// <summary>
    /// get highest position of highest symbol on reel
    /// </summary>
    Vector2 GetSymbolInHighestPosition()
    {
        float currentHighestY = -1000;
        foreach (var symbol in m_SymbolController.SymbolViews)
        {
            if (currentHighestY < symbol.transform.localPosition.y)
            {
                currentHighestY = symbol.transform.localPosition.y;
            }
        }
        return new Vector2(ReelCenter.transform.localPosition.x, currentHighestY);
    }

}