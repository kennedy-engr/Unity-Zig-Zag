using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this to a camera to allow for camera to follow a target

public class FollowCam : MonoBehaviour
{

    public Transform target; // the traget that the camera follows
    private Vector3 offset; // distance between camera and target

    void Awake()
    {
        offset = transform.position - target.position; // camera pos - target pos (static)
    }

    void LateUpdate()
    {
        transform.position = target.position + offset; // camera follows target

    }
}
