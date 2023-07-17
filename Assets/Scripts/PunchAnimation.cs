using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAnimation : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            anim.Play("PunchAni");
        }
    }
}
