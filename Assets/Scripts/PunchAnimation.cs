using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PunchAnimation : MonoBehaviour
{
    public Animator anim;

    public GameObject leftAttack;
    public GameObject rightAttack;

    public bool cooldownTimer = true;

    public GameObject particleObject;
    public ParticleSystem attackParticles;

    public AudioSource audioSource;  // Reference to the Audio Source
    public AudioClip Sound;       // Sound to be played when hit
    private void Start()
    {
        anim = GetComponent<Animator>();
        cooldownTimer = true;
        particleObject.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer == true)
        {
            anim.Play("PushAttack");
            StartCoroutine(WaitAndContinue());
            //StartCoroutine(WaitForParticles());
            cooldownTimer = false;
            particleObject.SetActive(true);
            attackParticles.Play();
            // Play the hit sound
            audioSource.PlayOneShot(Sound);
        }
    }
    private System.Collections.IEnumerator WaitAndContinue()
    {
        // Wait for .5 seconds before continuing
        yield return new WaitForSeconds(0.5f);
        AbleToAttack();
    }

    //private IEnumerator WaitForParticles()
    //{
    //    yield return new WaitForSeconds(1f);
    //    particleObject.SetActive(true);
    //    attackParticles.Pause();
    //}

    private void AbleToAttack()
    {
        cooldownTimer = true;
    }
}
