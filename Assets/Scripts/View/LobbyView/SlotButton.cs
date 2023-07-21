using controller;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace view
{
    /// <summary>
    /// This script is responsible for detecting the player pressing on the slot in lobby and invoking an event
    /// This slot button implementation is dependent on the "SceneLoader" in order to load scene.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SlotButton : MonoBehaviour
    {
        [SerializeField] private int m_SceneIndex;
        [SerializeField] private Button m_Button;
        [SerializeField] private Slider m_PrecentSlider;
        private event Action OnSlotClicked;
        private void Start()
        {
            CheckRelatedSceneIndex();
            m_Button.onClick.AddListener(SlotClicked);
            OnSlotClicked += OnLoadSlotScene;
        }

        private void CheckRelatedSceneIndex()
        {
            if (SceneManager.GetSceneByBuildIndex(m_SceneIndex) == null)
            {
                Debug.LogError($"SlotButton {this.gameObject.name} build index not found ");
            }

        }
        void OnLoadSlotScene()
        {
            _ = SceneLoader.LoadScene(m_SceneIndex, m_PrecentSlider);
        }
        private void SlotClicked()
        {
            m_PrecentSlider.gameObject.SetActive(true);
            OnSlotClicked?.Invoke();
        }
        private void OnDestroy()
        {
            OnSlotClicked -= OnLoadSlotScene;

        }
    }
}