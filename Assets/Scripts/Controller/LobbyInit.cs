using controller;
using Cysharp.Threading.Tasks;
using model;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInit : MonoBehaviour
{
    [SerializeField] DownloadStrategyDataContainer lobbyDownloaderContainer;
    [SerializeField] Slider ProgressBar;
    private void Awake()
    {
        _ = Init();
    }
    private async UniTask Init()
    {
        await SceneLoader.LoadScene(1, ProgressBar, lobbyDownloaderContainer.Downloader);

    }
  
}
