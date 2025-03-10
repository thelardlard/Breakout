using UnityEngine;
using PrimeTween;
using UnityEngine.EventSystems;
public class ButtonGrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float targetSize = 1.04f;
    private const float animationDuration = 0.2f;

    public void OnEnable()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.Scale(transform, targetSize, animationDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Scale(transform, 1, animationDuration);
    }
}
