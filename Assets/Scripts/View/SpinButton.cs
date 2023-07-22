using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace view
{
    /// <summary>
    /// spin button is responsible for detecting inputs for the "spin","stop" and "auto" funcionalities, only invokes events.
    /// </summary>
    public class SpinButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] float longPressDuration = 1.0f; // Duration in seconds to consider a press as long press
        private bool isLongPress = false;
        private bool isPointerDown = false;
        private float pressTime = 0f;
        public event Action OnPointerDownEvent;
        public event Action OnPointerUpEvent;
        public event Action OnShortPressEvent;
        public event Action OnLongPressEvent;



        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            pressTime = Time.time;
            OnPointerDownEvent?.Invoke();
            _ = OnPressedTask();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            isLongPress = false;
            OnPointerUpEvent?.Invoke();
            if (Time.time - pressTime < longPressDuration)
            {
                // Short press action
                OnShortPressEvent?.Invoke();
            }
        }
        private async UniTask OnPressedTask()// I like this solution a bit more then using the update, even though its abit lengthier it won't check any condition if pointer is not down
        {
            while (isPointerDown)
            {
                if (!isLongPress && Time.time - pressTime >= longPressDuration)
                {
                    isLongPress = true;
                    OnLongPressEvent?.Invoke();
                }
                await UniTask.NextFrame();
            }
        }
    }
}