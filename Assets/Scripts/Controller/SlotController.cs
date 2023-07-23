using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using model;
using view;
using static UnityEngine.Rendering.DebugUI.Table;

namespace controller
{
    /// <summary>
    /// slot controller is probably the biggest script in this project
    /// it is responsible for the state of the slot
    /// initializing the reels
    /// calling when the reels should spin
    /// invoking events when reels/slot has started spinning and when its stopped
    /// it can recieve different spinning stratagies
    /// 
    /// </summary>
    public class SlotController : MonoBehaviour
    {
        [SerializeField] SlotModel slotModel;
        [SerializeField] ReelController[] allReelControllers;
        [SerializeField] SpinButton spinButton;
        [SerializeField] SpinText spinButtonText;
        [SerializeField] BigWinPopupModel bigWinPopupModel;
        [SerializeField] BigWinPopupViewHandler bigWinPopupViewHandler;

        private ISpinningStrategy spinningStrategy;
        private List<ReelController> activeReels;
        private WinConditionChecker winConditionChecker;
        private bool reelsSpinning;
        private bool autoActivated;

        public event Action OnSpinStarted;
        public event Action OnSpinEnded;

        private void Awake()
        {
            activeReels = new List<ReelController>();
            winConditionChecker = new WinConditionChecker(bigWinPopupModel, bigWinPopupViewHandler);
            SetSpinningStrategy(new RandomSpinningStrategy());
            InitReels();

            spinButton.OnShortPressEvent += OnShortPres;
            spinButton.OnLongPressEvent += SetAutoTrue;
            OnSpinStarted += DeductCostFromScore;
            OnSpinEnded += CallCheckWinConditionMet;
            OnSpinEnded += UpdateSpinButtonText;
        }

        public void SetSpinningStrategy(ISpinningStrategy strategy)
        {
            spinningStrategy = strategy;
        }

        private void InitReels()
        {
            for (int i = 0; i < slotModel.ReelModels.Length; i++)
            {
                allReelControllers[i].InitReel(slotModel.ReelModels[i]);
                activeReels.Add(allReelControllers[i]);
            }
        }

        private void OnShortPres()
        {
            if (!reelsSpinning && !autoActivated)
            {
                SpinAllReels();
                spinButtonText.SwapToStopText();
            }
            else if (autoActivated || reelsSpinning)
            {
                StopAllReels();
            }
        }

        public void SpinAllReels()
        {
            if (!slotModel.HasEnoughPoints())
                return;

            spinningStrategy?.SpinReels(activeReels);
            reelsSpinning = true;
            _ = OnReelsSpinningTask();
        }
        public async UniTask OnReelsSpinningTask()
        {
            OnSpinStarted?.Invoke();
            while (reelsSpinning)
            {
                bool someReelSpinning = false;// bool to store if any reel is spinning
                foreach (var reelController in activeReels)//loop of active reels
                {
                    if (reelController.IsSpinning)// of one reel is spinning set bool to true (should happen on first loops)
                    {
                        someReelSpinning = true;
                    }
                }
                await UniTask.NextFrame();
                if (!someReelSpinning)// if no reel came out spinning set the reels spinning bools false and exit while loop
                {
                    reelsSpinning = false;
                }
            }
            OnSpinEnded?.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            if (!reelsSpinning && autoActivated)//check after 2 seconds if auto is on and player didn't manually spin the reels
            {
                if (isBigWinPopupAlive())//wait till popup is closed in case auto spinning is activated
                {
                    await UniTask.WaitUntil(() => !isBigWinPopupAlive());
                }

                SpinAllReels();
            }
        }
        public void StopAllReels()
        {
            foreach (var reelController in activeReels)
            {
                reelController.Stop();
            }
            spinButtonText.SwapToSpinText();
            SetAutoFalse();
        }

        private void SetAutoTrue()
        {
            autoActivated = true;
            if (!reelsSpinning)
            {
                SpinAllReels();
            }
            spinButtonText.SwapToAutoText();
        }
        private void SetAutoFalse() => autoActivated = false;

        void DeductCostFromScore() => ScoreHandler.DeductFromScore(slotModel.SlotSpinCost);
        void CallCheckWinConditionMet()
        {
            SymbolView[][] rowsToCheck = new SymbolView[][] { GetRowAboveMiddle(), GetMidRow(), GetRowBelowMiddle() };

            winConditionChecker.OnSpinEndCheckRows(rowsToCheck);
        }

        private SymbolView[] GetMidRow()
        {
            SymbolView[] symbolViews = new SymbolView[activeReels.Count];
            for (int i = 0; i < activeReels.Count; i++)
            {
                symbolViews[i] = activeReels[i].GetSymbolInMiddle();
            }
           return symbolViews;
        }
        private SymbolView[] GetRowAboveMiddle()
        {
            SymbolView[] symbolViews = new SymbolView[activeReels.Count];
            for (int i = 0; i < activeReels.Count; i++)
            {
                symbolViews[i] = activeReels[i].GetSymbolAboveMiddle();
            }
            return symbolViews;
        }
        private SymbolView[] GetRowBelowMiddle()
        {
            SymbolView[] symbolViews = new SymbolView[activeReels.Count];
            for (int i = 0; i < activeReels.Count; i++)
            {
                symbolViews[i] = activeReels[i].GetSymbolBelowMiddle();
            }
            return symbolViews;
        }
        private void UpdateSpinButtonText()
        {
            if (!autoActivated)
            {
                spinButtonText.SwapToSpinText();
            }
        }
        private bool isBigWinPopupAlive() => bigWinPopupViewHandler.gameObject.activeInHierarchy;
    }
}