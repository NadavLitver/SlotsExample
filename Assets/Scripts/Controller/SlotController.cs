using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] private SlotModel slotModel;
    [SerializeField] private ReelController[] allReelControllers;
    private List<ReelController> activeReels;
    [SerializeField] SpinButton spinButton;
    [SerializeField] SpinText spinButtonText;

    bool reelsSpinning;
    bool autoActivated;
    private void Awake()
    {
        activeReels = new List<ReelController>();
        for (int i = 0; i < slotModel.ReelModels.Length; i++)
        {
            allReelControllers[i].InitReel(slotModel.ReelModels[i]);
            activeReels.Add(allReelControllers[i]);
        }
        spinButton.OnShortPressEvent += OnShortPres;
        spinButton.OnLongPressEvent += SetAutoTrue;
    }

    private void OnShortPres()
    {
        if(!reelsSpinning && !autoActivated) 
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
        
        foreach (var reelController in activeReels)
        {
            reelController.SpinRandom();
        }
        reelsSpinning = true;
        _ = OnReelsSpinningTask();
    }
    public async UniTask OnReelsSpinningTask()
    {
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
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        if(!reelsSpinning && autoActivated)//check after 2 seconds if auto is on and player didn't manually spin the reels
        {
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


}
