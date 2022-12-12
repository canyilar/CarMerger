using UnityEngine;
using UnityEngine.EventSystems;

namespace CarMerger
{
    public class SpeedModifierButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.IncreaseSpeedMultiplier();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameManager.Instance.NormalizeSpeedMultiplier();
        }
    }
}
