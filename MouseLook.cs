using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MouseLook : NetworkBehaviour
{
    float xRotation;
    float lookSensitivity = 60;
    public Camera cameraPlayer;

    public Transform playerBody;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (IsOwner && cameraPlayer.gameObject.activeInHierarchy == true) {


        float x = Input.GetAxis("Mouse X")*lookSensitivity*Time.deltaTime;
        float y = Input.GetAxis("Mouse Y")*lookSensitivity*Time.deltaTime;

        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
            cameraPlayer.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * x);
        }
    }
}
