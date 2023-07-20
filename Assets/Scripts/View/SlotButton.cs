using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using controller;

namespace view
{
    /// <summary>
    /// This script is responsible for detecting the player pressing on the slot on invoking an event
    /// This slot button implementation is dependent on the "SceneLoader" in order to load scene.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SlotButton : MonoBehaviour
    {
        [SerializeField] private int m_RelatedSceneIndex;
        [SerializeField] private Button m_Button;
        [SerializeField] private Slider m_PrecentSlider;
        private event Action<int, Slider> OnSlotClicked;
        private void Start()
        {
            CheckRelatedSceneIndex();
            m_Button.onClick.AddListener(SlotClicked);
            OnSlotClicked += SceneLoader.LoadScene;
        }

        private void CheckRelatedSceneIndex()
        {
            if (SceneManager.GetSceneByBuildIndex(m_RelatedSceneIndex) == null)
            {
                Debug.LogError($"SlotButton {this.gameObject.name} build index not found ");
            }
         
        }

        private void SlotClicked()
        {
            m_PrecentSlider.gameObject.SetActive(true);
            OnSlotClicked?.Invoke(m_RelatedSceneIndex, m_PrecentSlider);
        }
        private void OnDestroy()
        {
            OnSlotClicked -= SceneLoader.LoadScene;

        }
    }
}