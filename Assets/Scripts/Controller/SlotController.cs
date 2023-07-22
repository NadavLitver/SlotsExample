using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using model;
using view;
namespace controller
{
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
        bool reelsSpinning;
        bool autoActivated;
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
            OnSpinEnded += CallWinConditionChecker;
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
                if (isBigWinPopupAlive())
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
        void CallWinConditionChecker()
        {
            SymbolView[] symbolViews = new SymbolView[activeReels.Count];
            for (int i = 0; i < activeReels.Count; i++)
            {
                symbolViews[i] = activeReels[i].GetSymbolInMiddle();
            }
            winConditionChecker.OnSpinEnd(symbolViews);
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