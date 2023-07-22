using UnityEngine;
using UnityEngine.UI;
namespace view
{
    /// <summary>
    /// responsible on unloading all asset bundles on button pressed 
    /// </summary>
    public class SceneUnloaderButton : MonoBehaviour
    {
        [SerializeField] Button m_Buttom;
        void Start()
        {
            m_Buttom.onClick.AddListener(OnHomeButtonPressed);
        }

        private void OnHomeButtonPressed()
        {
            AssetBundle.UnloadAllAssetBundles(false);
        }
    }
}