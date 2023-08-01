using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAnimation : MonoBehaviour
{
    public Animator anim;

    public GameObject leftAttack;
    public GameObject rightAttack;

    public bool cooldownTimer = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cooldownTimer = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer == true)
        {
            anim.Play("PushAttack");
            StartCoroutine(WaitAndContinue());
            cooldownTimer = false;
        }
    }
    private System.Collections.IEnumerator WaitAndContinue()
    {
        // Wait for .5 seconds before continuing
        yield return new WaitForSeconds(0.5f);
        AbleToAttack();
    }

    private void AbleToAttack()
    {
        cooldownTimer = true;
    }
}
