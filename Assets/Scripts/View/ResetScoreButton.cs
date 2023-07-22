using UnityEngine;
using UnityEngine.UI;
using controller;
namespace view
{
    /// <summary>
    /// responsible on calling the score handler "Reset Score" when button is pressed
    /// </summary>
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
}