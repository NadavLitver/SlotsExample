using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ResetScoreButton : MonoBehaviour
{
    [SerializeField] Button resetScoreButtonComponent;
    void Start()
    {
        resetScoreButtonComponent.onClick.AddListener(CallResetScore);
    }
    public void CallResetScore() => ScoreHandler.ResetScore();
}
