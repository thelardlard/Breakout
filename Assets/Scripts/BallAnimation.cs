using UnityEngine;
using PrimeTween;

public class BallAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [Tooltip("How much the ball should shrink on impact (0-1 range recommended)")]
    [Range(0.1f, 1f)]
    public float shrinkFactor = 0.8f;

    [Tooltip("How long the shrink animation should last in seconds")]
    public float shrinkDuration = 0.1f;

    [Tooltip("How long the expand animation should last in seconds")]
    public float expandDuration = 0.2f;

    [Tooltip("Optional easing for shrink animation")]
    public Ease shrinkEase = Ease.InQuad;

    [Tooltip("Optional easing for expand animation")]
    public Ease expandEase = Ease.OutElastic;

    // Track if animation is in progress
    private bool isAnimating = false;

    // Tween sequence for chaining animations
    private Sequence animationSequence;

    void OnCollisionEnter(Collision collision)
    {
        // Only start the animation if we're not already animating
        if (isAnimating)
        {
            return;
        }

        isAnimating = true;

        // Calculate the target scale
        Vector3 originalScale = transform.localScale;
        Vector3 shrunkScale = originalScale * shrinkFactor;

        // Create a sequence of tweens
        animationSequence = Sequence.Create()
            // First shrink the ball
            .Chain(Tween.Scale(transform, shrunkScale, shrinkDuration, shrinkEase))
            // Then expand it back
            .Chain(Tween.Scale(transform, originalScale, expandDuration, expandEase))
            // Set a callback when the sequence completes
            .OnComplete(() => {
                isAnimating = false;
            });
    }

    void OnDisable()
    {
        // Ensure we clean up the tween if the object is disabled
        if (animationSequence.isAlive)
        {
            animationSequence.Stop();
        }
    }
}
