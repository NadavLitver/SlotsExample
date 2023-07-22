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
        public override ISlotDownloader SlotDownloader
        {
            get
            {
                slotDownloader ??= new AlladinSlotDownloader();
                return slotDownloader;
            }
        }
        private ISlotDownloader slotDownloader;


    }
}