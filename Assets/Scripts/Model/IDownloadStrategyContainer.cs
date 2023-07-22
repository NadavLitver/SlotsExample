
using controller;
namespace model
{
    public interface IDownloadStrategyContainer
    {
        abstract ISlotDownloader SlotDownloader { get; }
    }
}