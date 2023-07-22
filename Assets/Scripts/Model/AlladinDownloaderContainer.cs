using UnityEngine;
using controller;
namespace model
{
    /// <summary>
    /// responsible for holding the alladin downloader concrete implmentation
    /// is an scriptable object refrenced through serialization
    /// </summary>
    [CreateAssetMenu(fileName = "Alladin Downloader Strategy", menuName = "Slots/Alladin Downloader Strategy", order = 1)]
    
    public class AlladinDownloaderContainer : DownloadStrategyDataContainer
    {
        public override IAssetBundleDownloader Downloader
        {
            get
            {
                downloader ??= new AlladinSlotDownloader();
                return downloader;
            }
        }
        private IAssetBundleDownloader downloader;


    }
}