using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
namespace controller
{
    public class AlladinSlotDownloader : ISlotDownloader
    {
        private const string googleDriveLink = "https://drive.google.com/uc?export=download&id=1MgsxGbhqDf_HNT5mBIZiiVOOyjdJpaeo";
        private const string prefabName = "AladdinSlot_Bundle";

        GameObject downloadedPrefab;

        public async UniTask DownloadSlotFromGoogleDrive()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(googleDriveLink);
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // File has been downloaded successfully
                byte[] downloadedData = webRequest.downloadHandler.data;
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(downloadedData);
                if (assetBundle != null)
                {
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
    }
}