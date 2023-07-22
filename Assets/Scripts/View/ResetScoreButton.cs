using UnityEngine;
using UnityEngine.UI;
using controller;
namespace view
{
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