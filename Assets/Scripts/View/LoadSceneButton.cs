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
    /// if a Slot downloader was set it will pass it along to the scene loader in order to download the asset bundle when before moving a scene
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private int m_SceneIndex;
        [SerializeField] private Button m_Button;
        [SerializeField] private Slider m_PrecentSlider;
        private IAssetBundleDownloader m_downloaderStrategy;

        private event Action OnSlotClicked;
        private void Start()
        {
            CheckRelatedSceneIndex();
            m_Button.onClick.AddListener(SlotClicked);

            if(m_downloaderStrategy != null)
            {
                OnSlotClicked += OnLoadSceneWithDownloader;
            }
            else
            {
                OnSlotClicked += OnLoadScene;
            }
         
        }
        public void SetDownloaderStrategy(IAssetBundleDownloader downloader)
        {
            m_downloaderStrategy = downloader;
        }

        private void CheckRelatedSceneIndex()
        {
            if (SceneManager.GetSceneByBuildIndex(m_SceneIndex) == null)
            {
                Debug.LogError($"LoadSceneButton {this.gameObject.name} build index not found ");
            }

        }
        public void OnLoadScene()
        {
            _ = SceneLoader.LoadScene(m_SceneIndex, m_PrecentSlider);

        }
        public void OnLoadSceneWithDownloader()
        {
            _ = SceneLoader.LoadScene(m_SceneIndex, m_PrecentSlider, m_downloaderStrategy);

        }
        private void SlotClicked()
        {
            m_PrecentSlider.gameObject.SetActive(true);
            OnSlotClicked?.Invoke();
        }
        private void OnDestroy()
        {
            if (m_downloaderStrategy != null)
            {
                OnSlotClicked -= OnLoadSceneWithDownloader;
            }
            else
            {
                OnSlotClicked -= OnLoadScene;
            }

        }
    }
}