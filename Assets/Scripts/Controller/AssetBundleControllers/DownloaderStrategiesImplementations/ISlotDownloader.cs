using Cysharp.Threading.Tasks;
using UnityEngine;
namespace controller
{
    public interface ISlotDownloader
    {
        UniTask DownloadSlotFromGoogleDrive();
        GameObject GetDownloadedPrefab();
    }
}