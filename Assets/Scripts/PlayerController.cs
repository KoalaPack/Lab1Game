using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 4.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public GameObject targetDestination;

    private void Start()
    {

    }

    void Update()
    {

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (agent.enabled == true)
            {
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetDestination.transform.position = transform.position + move;

                agent.SetDestination(targetDestination.transform.position);

                // Changes the height position of the player..
                if (Input.GetButtonDown("Jump") && groundedPlayer)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }

                playerVelocity.y += gravityValue * Time.deltaTime;
                //controller.Move(playerVelocity * Time.deltaTime);
            }


    }
}

