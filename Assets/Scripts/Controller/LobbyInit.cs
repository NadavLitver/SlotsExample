using Cysharp.Threading.Tasks;
using model;
using UnityEngine;

public class LobbyInit : MonoBehaviour
{
    [SerializeField] DownloadStrategyDataContainer lobbyDownloaderContainer;
    private void Awake()
    {
        _ = Init();
    }
    private async UniTask Init()
    {
        await lobbyDownloaderContainer.Downloader.DownloadSlotFromGoogleDrive();
        Instantiate(lobbyDownloaderContainer.Downloader.GetDownloadedPrefab());


    }
    private void OnDisable()
    {
        lobbyDownloaderContainer.Downloader.UnloadBundle();
    }
}
