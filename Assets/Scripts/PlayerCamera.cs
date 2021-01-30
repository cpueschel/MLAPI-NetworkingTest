using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerCamera : NetworkedBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            offset = transform.position - player.transform.position;
        } else {
            transform.GetComponent<AudioListener>().enabled = false;
            transform.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (IsLocalPlayer)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
