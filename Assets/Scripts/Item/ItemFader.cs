using DG.Tweening;
using UnityEngine;

public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetColor, Settings.fadeIn);
    }

    public void FadeOut()
    {
        Color targetColar = new(1,1,1,Settings.targetAlpha);
        spriteRenderer.DOColor(targetColar, Settings.fadeIn);
    }
}
