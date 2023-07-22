using UnityEngine;
using UnityEngine.UI;
namespace view
{
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