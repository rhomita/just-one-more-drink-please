using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Transform player;

    private float zoom;
    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    void Update()
    {
        zoom += Input.mouseScrollDelta.y / 2;
        zoom = Mathf.Clamp(zoom, -2, 2);
        
        
        Vector3 newPosition = player.position + offset + transform.forward * zoom;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
    }
}
