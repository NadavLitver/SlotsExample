using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace controller
{
    public static class SceneLoader
    {
        public static bool IsLoading { get; private set; }
        // Call this method to load a scene asynchronously using a Scene object
        public static async void LoadScene(int sceneBuildIndex)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
                while (!asyncOperation.isDone)
                { 
                    await Task.Yield(); // Wait for the next frame
                }
                IsLoading = false;
            }
        }

        // Call this method to load a scene asynchronously using a Scene object And an image set to filled to show progress
        public static async void LoadScene(int sceneBuildIndex, UnityEngine.UI.Slider progressSlider)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
                while (!asyncOperation.isDone)
                {
                    float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Progress is from 0 to 0.9
                    progressSlider.value = progress;
                    await Task.Yield(); // Wait for the next frame
                }
                progressSlider.value = 1f; // Ensure the slider is at the max value (1) after loading is complete.
                IsLoading = false;
            }
        }
    }
}