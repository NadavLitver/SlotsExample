using UnityEngine;
using UnityEngine.UI;

public class SymbolView : MonoBehaviour
{
    private int m_ID;
    [SerializeField] RectTransform m_RectTransform;
    [SerializeField] Image m_Image;
    public RectTransform RectTransformRef { get => m_RectTransform; }
    public Image Image { get => m_Image; }

    public void SetID(int _ID)
    {
        m_ID = _ID;
    }
    public int GetID()
    {
        return m_ID;
    }

}
