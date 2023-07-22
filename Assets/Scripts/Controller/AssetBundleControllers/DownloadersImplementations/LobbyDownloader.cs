using controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyDownloader : IAssetBundleDownloader
{
    private const string AndroidAssetBundleGoogleDriveLink = "https://drive.google.com/uc?export=download&id=1naKGwOdR4F2wzrOB_qCnBfR4hwEYUjGu";
    private const string WindowsAssetBundleGoogleDriveLink = "https://drive.google.com/uc?export=download&id=1D1RdtKJ-trA_ln_cTUKlI_DZ_fOPqKJl";
    private const string prefabName = "Lobby";

    GameObject downloadedPrefab;
    AssetBundle downloadedBundle;
  
    public async UniTask DownloadSlotFromGoogleDrive()
    {
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
    public void UnloadBundle(bool RemoveRelatedObjects)
    {
        if (downloadedBundle != null)
        {
            downloadedBundle.Unload(false);
            downloadedBundle = null;
        }
    }
}
