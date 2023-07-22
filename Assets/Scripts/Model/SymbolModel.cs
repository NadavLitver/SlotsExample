using UnityEngine;
namespace model
{

    [CreateAssetMenu(fileName = "New Symbol", menuName = "Slots/SymbolModel")]
    public class SymbolModel : ScriptableObject
    {
        [SerializeField] int symbolID;
        [SerializeField] Sprite symbolSprite;
        [SerializeField] string symbolName;

        public int SymbolID { get => symbolID; }
        public Sprite SymbolSprite { get => symbolSprite; }
        public string SymbolName { get => symbolName; }
    }
}