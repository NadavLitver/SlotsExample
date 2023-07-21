using UnityEngine;
[CreateAssetMenu(fileName = "New ReelModel", menuName = "Slots/ReelData")]
public class ReelModel : ScriptableObject
{
   [SerializeField] float m_SpinningIntervals;
   [SerializeField] float m_SpinningSpeed;
   [SerializeField] SymbolModel[] m_SymbolsData;
   
   [SerializeField] float m_DistanceBetweenSymbols;

   public float SpinningSpeed { get => m_SpinningSpeed;}
   public float DefaultSpinCount { get => m_SpinningIntervals; }
   public float DistanceBetweenSymbols { get => m_DistanceBetweenSymbols; }
   public SymbolModel[] SymbolsData { get => m_SymbolsData;}
}
