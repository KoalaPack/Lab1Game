using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisableNavMesh : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == ("Player"))
        {
            agent.enabled = false;
        }
    }
}
