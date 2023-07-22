using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PopupModel", menuName = "Slots/BigWinPopupModel")]

public class BigWinPopupModel : ScriptableObject
{
    [SerializeField] Vector3 scaleGoal;
    [SerializeField] Vector3 scaleToStart;
    [SerializeField] float durationToScale;

    public Vector3 ScaleGoal { get => scaleGoal; }
    public Vector3 ScaleToStart { get => scaleToStart;}
    public float DurationToScale { get => durationToScale;}
}
