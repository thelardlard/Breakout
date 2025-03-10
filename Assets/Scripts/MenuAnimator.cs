using PrimeTween;
using UnityEngine;


public class MenuAnimator : MonoBehaviour
{
    public RectTransform menuPanel; // Assign your menu panel in the inspector
    private const float animationDuration = 0.5f;

    void Start()
    {
        menuPanel.localScale = Vector3.zero; // Start hidden
    }

    public void ShowMenu()
    {
        menuPanel.gameObject.SetActive(true);
        Tween.Scale(menuPanel, Vector3.one, animationDuration, Ease.OutBack);
    }

    public void HideMenu()
    {
        Tween.Scale(menuPanel, Vector3.zero, 0.3f, Ease.InBack)
            .OnComplete(() => menuPanel.gameObject.SetActive(false));
    }
}

