using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AnimationClip animationClip; // The animation clip to play
    public KeyCode activationKey = KeyCode.J; // The key to activate the animation
    public float cooldownDuration = 1f; // The cooldown duration between animation activations

    private Animation animationComponent; // Reference to the Animation component
    private bool canPlayAnimation = true; // Flag to indicate if animation can be played

    private void Start()
    {
        animationComponent = GetComponent<Animation>(); // Get the Animation component attached to the GameObject
    }

    private void Update()
    {
        if (canPlayAnimation && Input.GetKeyDown(activationKey))
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        if (animationComponent != null && animationClip != null)
        {
            animationComponent.Play(animationClip.name);

            canPlayAnimation = false;
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetCooldown()
    {
        canPlayAnimation = true;
    }
}