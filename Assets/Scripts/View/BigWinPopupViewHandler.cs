using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace view
{
    public class BigWinPopupViewHandler : MonoBehaviour
    {
        private Vector3 startingVector;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button mainButton;
        [SerializeField] private SpinButton SpinButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        private void Awake()
        {
            closeButton.onClick.AddListener(DisableSelf);
            mainButton.onClick.AddListener(DisableSelf);
        }

        public void Popup(Vector3 goal, Vector3 _startingVector, float duration, int scoreGain)
        {
            SpinButton.enabled = false;
            startingVector = _startingVector;
            transform.DOScale(goal, duration).SetEase(Ease.OutElastic);
            scoreText.text = scoreGain.ToString();
        }
        private void OnDisable()
        {
            SpinButton.enabled = true;
            transform.localScale = startingVector;
            scoreText.text = string.Empty;
        }
        private void DisableSelf() => this.gameObject.SetActive(false);
    }
}