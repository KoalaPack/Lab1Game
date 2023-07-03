using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        //Set the offset of the camera based in the player
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Get the cameras transfrom position to be that of the players transform position
        transform.position = player.transform.position + offset;
    }


}
