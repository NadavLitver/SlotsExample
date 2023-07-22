using UnityEngine;
using view;
using model;
namespace controller
{
    /// <summary>
    /// used to set the downloader on a "load scene button" depending the scene we want it to download
    /// </summary>
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