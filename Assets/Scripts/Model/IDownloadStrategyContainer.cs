
using controller;
public interface IDownloadStrategyContainer
{
    abstract ISlotDownloader SlotDownloader { get; }
}
