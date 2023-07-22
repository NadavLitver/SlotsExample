using TMPro;
using UnityEngine;
using controller;
namespace view
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreTextHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Text;
        void Start()
        {
            ScoreHandler.OnScoreChanged += SetScoreText;
            SetScoreText(ScoreHandler.GetScore());
        }

        private void SetScoreText(int newScore)
        {
            m_Text.text = $"Score: {newScore}";
        }

        private void OnDestroy()
        {
            ScoreHandler.OnScoreChanged -= SetScoreText;
        }
    }
}