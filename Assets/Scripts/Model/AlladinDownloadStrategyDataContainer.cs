using UnityEngine;
using controller;
namespace model
{
    [CreateAssetMenu(fileName = "Alladin Downloader Strategy", menuName = "Slots/Alladin Downloader Strategy", order = 1)]

    public class AlladinDownloadStrategyDataContainer : DownloadStrategyDataContainer
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