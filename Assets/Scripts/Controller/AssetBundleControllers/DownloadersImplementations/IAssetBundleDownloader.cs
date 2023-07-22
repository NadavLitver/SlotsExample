using Cysharp.Threading.Tasks;
using UnityEngine;
namespace controller
{
    /// <summary>
    /// an interface for creating downloaders implementations
    /// </summary>
    public interface IAssetBundleDownloader
    {
        UniTask DownloadSlotFromGoogleDrive();
        GameObject GetDownloadedPrefab();
        AssetBundle GetAssetBundle();
        void UnloadBundle(bool removeRelatedObjects);
    }
}