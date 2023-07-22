using UnityEngine;

public class FPSSetter : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = -1;
#else
    Application.targetFrameRate = 120;
#endif
    }
}
