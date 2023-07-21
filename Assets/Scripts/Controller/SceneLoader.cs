using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace controller
{
    public static class SceneLoader
    {
        public static bool IsLoading { get; private set; }
        // load a scene asynchronously using a Scene index
        public static async UniTask LoadScene(int sceneBuildIndex)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
                while (!asyncOperation.isDone)
                { 
                    await UniTask.Yield(); // no need to be frame perfect so i chose yield over next frame
                }
                IsLoading = false;
            }
        }

        // load a scene asynchronously using a Scene object And an slider to to show progress
        public static async UniTask LoadScene(int sceneBuildIndex, UnityEngine.UI.Slider progressSlider)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
                while (!asyncOperation.isDone)
                {
                    float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Progress is from 0 to 0.9
                    progressSlider.value = progress;
                    await UniTask.NextFrame(); // Wait for the next frame
                }
                progressSlider.value = 1f; // Ensure the slider is at the max value (1) after loading is complete.
                IsLoading = false;
            }
        }
    }
}