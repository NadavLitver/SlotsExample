using UnityEngine;
using controller;
namespace model
{
    /// <summary>
    /// abstract class that containers will inherit from in order to hold a concrete implementation of ISlotDownloader (see AlladinDownloaderContainer for example)
    /// </summary>
    public abstract class DownloadStrategyDataContainer : ScriptableObject, IDownloadStrategyContainer
    {
        public abstract IAssetBundleDownloader Downloader { get; }

    }
}