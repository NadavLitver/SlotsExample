using Cysharp.Threading.Tasks;
using UnityEngine;
namespace controller
{
    /// <summary>
    /// an interface for creating downloaders implementations
    /// </summary>
    public interface ISlotDownloader
    {
        UniTask DownloadSlotFromGoogleDrive();
        GameObject GetDownloadedPrefab();
        AssetBundle GetAssetBundle();
        public void UnloadBundle();
    }
}