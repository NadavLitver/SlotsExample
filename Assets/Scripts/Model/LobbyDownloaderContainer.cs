using controller;
using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "Lobby Downloader Strategy", menuName = "Slots/Lobby Downloader Strategy", order = 1)]

    public class LobbyDownloaderContainer : DownloadStrategyDataContainer
    {
        public override IAssetBundleDownloader Downloader
        {
            get
            {
                downloader ??= new LobbyDownloader();
                return downloader;
            }
        }
        private IAssetBundleDownloader downloader;


    }
}