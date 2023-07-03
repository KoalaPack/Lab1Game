using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    public GameObject player;
    public Camera spinCamera;
    public Camera playerCamera;

    void OnTriggerEnter(Collider player)
    {
        if (player.tag ==("Player"))
        {
            playerCamera.gameObject.SetActive(false);
            spinCamera.gameObject.SetActive(true);
            Destroy(player.gameObject);
        }
    }
}
