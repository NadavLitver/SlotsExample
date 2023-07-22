using UnityEngine;

public class FPSSetter : MonoBehaviour
{
    /// <summary>
    /// no need to go over 120 on mobile devices, the best screens today on mobile are 120hz, for the editor we can use unlimited frames
    /// </summary>
    private void Awake()
    {
#if UNITY_EDITOR
    Application.targetFrameRate = -1;
#else
    Application.targetFrameRate = 120;
#endif
    }
}
