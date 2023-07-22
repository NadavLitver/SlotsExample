using UnityEngine;
using controller;
namespace model
{
    public abstract class DownloadStrategyDataContainer : ScriptableObject, IDownloadStrategyContainer
    {
        public abstract ISlotDownloader SlotDownloader { get; }

    }
}