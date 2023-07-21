using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] private SlotModel slotModel;
    [SerializeField] private List<ReelController> reelControllers;
    
    private void Awake()
    {
        for (int i = 0; i < slotModel.ReelModels.Length; i++)
        {
            reelControllers[i].InitReel(slotModel.ReelModels[i]);
        }
       
    }
    //public void SpinAllReels()
    //{
    //    foreach (var reelController in reelControllers)
    //    {
    //        reelController.CallSpinReel();
    //    }
    //}

}
