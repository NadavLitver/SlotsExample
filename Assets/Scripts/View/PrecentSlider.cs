using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace view
{
    /// <summary>
    /// This script sole responsability is to update the text component on slider value change
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class PrecentSlider : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_PrecentText;
        [SerializeField] Slider m_Slider;
        private void Awake()
        {
            m_Slider.onValueChanged.AddListener(UpdateText);
        }
        private void UpdateText(float value)//value goes from 0 to 1 so we multiply it by 100
        {
            m_PrecentText.text = value * 100 + "%";
        }
    }
}