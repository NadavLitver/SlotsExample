using UnityEngine;
using view;
using model;
namespace controller
{
    public class LoadSceneButtonController : MonoBehaviour
    {
        [SerializeField] LoadSceneButton m_LoadSceneButton;
        [SerializeField] DownloadStrategyDataContainer downloadStrategyDataContainer;
        private void Awake()
        {
            m_LoadSceneButton.SetDownloaderStrategy(downloadStrategyDataContainer.SlotDownloader);
        }
    }
}