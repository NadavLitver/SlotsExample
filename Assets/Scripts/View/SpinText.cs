using TMPro;
using UnityEngine;
namespace view
{
    public class SpinText : MonoBehaviour
    {
        [SerializeField] SpinButton spinButton;
        [SerializeField] TextMeshProUGUI m_text;

        public void SwapToAutoText()
        {
            m_text.text = "Auto On";
        }

        public void SwapToStopText()
        {
            m_text.text = "Stop";
        }
        public void SwapToSpinText()
        {
            m_text.text = "Spin";
        }
    }
}