using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace view
{
    /// <summary>
    /// symbol view is responsible for for single symbol view funcionality such as visuals and detection getting the Image/ID, graying out the symbol and drawing a circle.
    /// </summary>
    public class SymbolView : MonoBehaviour
    {
        private int m_ID;
        [SerializeField] RectTransform m_RectTransform;
        [SerializeField] Image m_Image;
        [SerializeField] LineRenderer m_LineRenderer;
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
        [ContextMenu("DrawCircleSetup")]
        public void DrawCircleSetup()
        {
            // init variables for drawing circle
            int numPoints = 25;
            float radius = 80f;
            m_LineRenderer.positionCount = numPoints + 1;
            m_LineRenderer.useWorldSpace = false;

            float deltaAngle = 360f / numPoints; // Calculate the angle between each point
            _ = DrawCircleTask(numPoints, radius, deltaAngle);
        }

        private async UniTask DrawCircleTask(int numPoints, float radius, float deltaAngle)
        {
            for (int i = 0; i < numPoints + 1; i++)
            {
                float angle = i * deltaAngle; // Calculate the current angle

                // Convert the angle from degrees to radians
                float radians = angle * Mathf.Deg2Rad;

                // Calculate x and z based on the angle (radians) and radius
                float x = radius * Mathf.Cos(radians);
                float y = radius * Mathf.Sin(radians);

                // Set the position of the Line Renderer to the calculated point
                Vector3 pos = new Vector3(x, y, 0f);
                m_LineRenderer.SetPosition(i, pos);
                await UniTask.WaitForFixedUpdate();//make sure the "for" loop is creating the circle frame independently
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            int currentNumberOfPoints = numPoints + 1;

            for (int i = 0; i < numPoints + 1; i++)//remove circle
            {
                m_LineRenderer.positionCount = currentNumberOfPoints--;
                await UniTask.WaitForFixedUpdate();
            }

        }
        public void CallGrayOutImage() => _ = GrayOutImage();
        private async UniTask GrayOutImage()
        {
            Color startingColor = Image.color;
            Image.color = Color.gray;
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            Image.color = startingColor;


        }
    }
}