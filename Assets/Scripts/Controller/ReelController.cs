using Cysharp.Threading.Tasks;
using DG.Tweening;
using model;
using System;
using UnityEngine;
using view;
namespace controller
{
    /// <summary>
    /// Reel controller is responsible for initializing reel models, moving the symbols and stopping them
    /// </summary>
    public class ReelController : MonoBehaviour
    {
        private SymbolController m_SymbolController;
        private ReelModel m_ReelModel;
        private bool isSpinning;

        [SerializeField] RectTransform reelCenter;
        [SerializeField] SymbolView m_SymbolViewPrefab;
        public RectTransform ReelCenter { get => reelCenter; }
        public bool IsSpinning { get => isSpinning; }
        public ReelModel ReelModel { get => m_ReelModel; }

        bool spinStopped;
        private Vector3 middleRowCenterPos;
        private Vector3 rowAboveMidCenterPos;
        private Vector3 rowBelowMidCenterPos;
        private const float yPosTolerance = 0.2f;
        public void InitReel(ReelModel reelModel)
        {
            m_ReelModel = reelModel;
            m_SymbolController = new SymbolController();
            m_SymbolController.DisplayAndPositionSymbols(m_ReelModel.SymbolsData, m_ReelModel.DistanceBetweenSymbols, reelCenter, m_SymbolViewPrefab);
            // SpinRandom();
            middleRowCenterPos = reelCenter.localPosition;
            rowAboveMidCenterPos = middleRowCenterPos + (reelModel.DistanceBetweenSymbols * Vector3.up);
            rowBelowMidCenterPos = middleRowCenterPos - (reelModel.DistanceBetweenSymbols * Vector3.up);


        }
        public void SpinRandom()
        {
            int goalID = UnityEngine.Random.Range(1, m_ReelModel.SymbolsData.Length + 1);//+1 because max exclusive,start from 1 because I started from 1 in SO's
            CallSpinReel(goalID);
            DebugSpin(goalID);
        }
       
        public void SpinWithGoal(int goalID)
        {
            CallSpinReel(goalID);
            DebugSpin(goalID);
        }
        private void DebugSpin(int goalID)
        {
            int indexForDebug = goalID - 1;
            if (indexForDebug < 0)
            {
                indexForDebug = 0;
            }
            Debug.Log($"Reel{gameObject.name} is going to end on{m_ReelModel.SymbolsData[indexForDebug].SymbolName}");
        }
        public void CallSpinReel(int goalID)
        {
            if (!isSpinning)
            {
                _ = SpinReelTask(goalID);
            }
        }

        private async UniTask SpinReelTask(int goalID)
        {

            isSpinning = true;
            //init spinning variables
            float spinDistance = m_ReelModel.DistanceBetweenSymbols;
            float spinSpeed = m_ReelModel.SpinningSpeed;
            float OneSpinTime = spinDistance / spinSpeed;
            Vector2 topPosition = GetSymbolInHighestPosition();
            int SpinCounter = 0;
            //turn of winning circles
            m_SymbolController.TurnOfCircles();
            while (isSpinning)
            {
                //init spin variables
                bool spinComplete = false;

                //move each symbol downwards
                foreach (SymbolView symbol in m_SymbolController.SymbolViews)
                {
                    //get destination for one step of spinning proccess (many of these create the "Spinning" effect)
                    float destination = symbol.transform.localPosition.y - spinDistance;
                    //move symbol to destination
                    symbol.transform.DOLocalMoveY(destination, OneSpinTime).SetEase(Ease.Linear).onComplete = () => spinComplete = true;
                    //check stop conditions
                    if (AreStopConditionsAnswered(goalID, SpinCounter, symbol, destination))
                    {
                        Stop();
                    }

                }
                //wait till spin is complete so symbols are positioned correctly
                await UniTask.WaitUntil(() => spinComplete == true);
                //place lowest symbol on top
                GetSymbolWithLowestY().transform.localPosition = topPosition;
                //increase spin counter
                SpinCounter++;


                if (spinStopped)//exit loop incase spin was stopped either from the stop conditions or from an outside "Stop"
                {
                    isSpinning = false;
                    spinStopped = false;
                }
            }


        }
        /// <summary>
        /// AreStopConditionsAnswered checks that 
        /// a. base amount of spins has been reached
        /// b. current symbol is the symbol to be stopped on "the middle"(cuurent symbol symbol is the "Goal")
        /// c. item destination is the middle
        /// </summary>

        private bool AreStopConditionsAnswered(int goalID, int SpinCounter, SymbolView symbol, float destination)
        {
            return SpinCounter > m_ReelModel.DefaultSpinCount && symbol.GetID() == goalID && MathF.Abs(destination - ReelCenter.localPosition.y) < yPosTolerance;/// do tween had a 0.00003 error so I used the "absolute value" of the distance of the y
        }
        public SymbolView GetSymbolInMiddle()
        {
            if (!isSpinning)
            {
                foreach (SymbolView symbolView in m_SymbolController.SymbolViews)
                {
                    if (MathF.Abs(symbolView.transform.localPosition.y - middleRowCenterPos.y) < yPosTolerance)
                    {
                        return symbolView;
                    }
                }
            }
            Debug.LogError($"No Centered Symbol on {this.gameObject.name}.");
            return null;


        }
        public SymbolView GetSymbolAboveMiddle()
        {
            if (!isSpinning)
            {
                foreach (SymbolView symbolView in m_SymbolController.SymbolViews)
                {
                    if (MathF.Abs(symbolView.transform.localPosition.y - rowAboveMidCenterPos.y) < yPosTolerance)
                    {
                        return symbolView;
                    }
                }
            }
            Debug.LogError($"No Centered Symbol on {this.gameObject.name}.");
            return null;


        }
        public SymbolView GetSymbolBelowMiddle()
        {
            if (!isSpinning)
            {
                foreach (SymbolView symbolView in m_SymbolController.SymbolViews)
                {
                    if (MathF.Abs(symbolView.transform.localPosition.y - rowBelowMidCenterPos.y) < yPosTolerance)
                    {
                        return symbolView;
                    }
                }
            }
            Debug.LogError($"No Centered Symbol on {this.gameObject.name}.");
            return null;


        }
        /// <summary>
        /// get lowest symbol on reel
        /// </summary>
        public SymbolView GetSymbolWithLowestY()
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
        public void Stop()
        {
            if (isSpinning)
            {
                spinStopped = true;
            }

        }

    }
    
}