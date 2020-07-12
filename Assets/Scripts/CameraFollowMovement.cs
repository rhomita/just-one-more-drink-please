using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMovement : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    
    private Transform player;
    
    private float mouseSensitivity = 50f;
    private float zoom;
    private float xRotation = 0f;
    
    
    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
        player = GameManager.instance.Player;
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            xRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation.x, xRotation, 0);
        }

        zoom -= Input.mouseScrollDelta.y / 2;
        zoom = Mathf.Clamp(zoom, 2, 5);
        
        Vector3 position = player.position - transform.forward * zoom;
        transform.position = Vector3.Slerp(position, transform.position, Time.deltaTime * 1);
        
        return;
    }
}
