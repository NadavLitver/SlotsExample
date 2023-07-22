using System.Collections.Generic;
using UnityEngine;
using view;
using model;
namespace controller
{
    /// <summary>
    /// symbol controllers is responsible for getting symbol models and updating the view
    /// </summary>
    public class SymbolController
    {
        private List<SymbolView> m_SymbolViews;
        public List<SymbolView> SymbolViews { get => m_SymbolViews; }


        public SymbolController()
        {
            m_SymbolViews = new List<SymbolView>();
        }
        public void DisplayAndPositionSymbols(SymbolModel[] symbolsData, float distanceBetweenSymbols, RectTransform reelCenterPos, SymbolView symbolPrefab)
        {
            // Clear any existing symbols (if needed).
            ClearSymbols();

            float currentYOffsetNonEven = 0f;
            float currentYOffestEven = 0f;

            //initilize symbols view
            for (int i = 0; i < symbolsData.Length; i++)
            {
                // Create a new instance of the SymbolView.
                SymbolView symbolViewInstance = Object.Instantiate(symbolPrefab, reelCenterPos.parent);
                // Set the sprite for the symbol image instance using the SymbolData's sprite.
                symbolViewInstance.Image.sprite = symbolsData[i].SymbolSprite;
                // Set Identifier Based on Symbol Data
                SetSymbolIdentifier(symbolsData[i], symbolViewInstance);
                //Add to SymbolViewsList
                SymbolViews.Add(symbolViewInstance);
                // Set the position of the symbol image instance vertically based on the currentYOffset.
                if (i % 2 == 0)
                {
                    symbolViewInstance.transform.localPosition = new Vector2(reelCenterPos.localPosition.x, reelCenterPos.localPosition.y + currentYOffestEven);
                    currentYOffestEven += distanceBetweenSymbols;
                }
                else
                {
                    currentYOffsetNonEven -= distanceBetweenSymbols;
                    symbolViewInstance.transform.localPosition = new Vector2(reelCenterPos.localPosition.x, reelCenterPos.localPosition.y + currentYOffsetNonEven);
                }

            }
        }
      
        private static void SetSymbolIdentifier(SymbolModel symbolData, SymbolView SymbolViewInstance)
        {
            SymbolViewInstance.SetID(symbolData.SymbolID);
        }

        private void ClearSymbols()
        {
            if (SymbolViews.Count < 0)
                return;

            // Destroy all child objects (symbol images) in the ReelView before displaying new symbols.
            foreach (SymbolView symbolView in SymbolViews)
            {
                Object.Destroy(symbolView.gameObject);
            }
        }
        public void TurnOfCircles()
        {
            foreach (var symbol in SymbolViews)
            {
                symbol.TurnOffLineRendererCircle();
            }
        }
    }
}