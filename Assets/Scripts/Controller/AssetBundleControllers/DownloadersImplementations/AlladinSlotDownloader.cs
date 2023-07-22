using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
namespace controller
{
    /// <summary>
    /// the concrete implementation for downloading the alladin slot scene(prefab)
    /// </summary>
    public class AlladinSlotDownloader : IAssetBundleDownloader
    {
        private const string AndroidAssetBundleGoogleDriveLink = "https://drive.google.com/uc?export=download&id=1MgsxGbhqDf_HNT5mBIZiiVOOyjdJpaeo";
        private const string WindowsAssetBundleGoogleDriveLink = "https://drive.google.com/uc?export=download&id=18XsuUgSO3dTp_86YHE_JDTbAJy6TSluw";
        private const string prefabName = "AladdinSlot_Bundle";

        GameObject downloadedPrefab;
        AssetBundle downloadedBundle;
        public async UniTask DownloadSlotFromGoogleDrive()
        {
            UnloadBundle();
#if UNITY_EDITOR
            UnityWebRequest webRequest = UnityWebRequest.Get(WindowsAssetBundleGoogleDriveLink);
#else
            UnityWebRequest webRequest = UnityWebRequest.Get(AndroidAssetBundleGoogleDriveLink);
#endif
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // File has been downloaded successfully
                byte[] downloadedData = webRequest.downloadHandler.data;
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(downloadedData);
                if (assetBundle != null)
                {
                    downloadedBundle = assetBundle;
                    downloadedPrefab = assetBundle.LoadAsset<GameObject>(prefabName);
                }

                // Process the downloaded data as needed (e.g., save to disk or load as asset bundle)
            }
            else
            {
                // Handle download error
                Debug.LogError("Error downloading file: " + webRequest.error);
            }
        }
        public GameObject GetDownloadedPrefab()
        {
            return downloadedPrefab;
        }
        public AssetBundle GetAssetBundle()
        {
            return downloadedBundle;
        }
        public void UnloadBundle()
        {
            if (downloadedBundle != null)
            {
                downloadedBundle.Unload(true);
                downloadedBundle=null;
            }
        }
    }
}