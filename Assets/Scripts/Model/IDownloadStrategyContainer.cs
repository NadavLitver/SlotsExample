
using controller;
namespace model
{
    /// <summary>
    /// interface used to abstract access to download containers see "DownloadStrategyDataContainer"
    /// </summary>
    public interface IDownloadStrategyContainer
    {
        abstract ISlotDownloader SlotDownloader { get; }
    }
}