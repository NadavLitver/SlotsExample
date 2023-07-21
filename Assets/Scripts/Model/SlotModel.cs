using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Symbol", menuName = "Slots/SlotModel")]

public class SlotModel : ScriptableObject
{
    [SerializeField] ReelModel[] reelModels;

    public ReelModel[] ReelModels { get => reelModels;}
}
