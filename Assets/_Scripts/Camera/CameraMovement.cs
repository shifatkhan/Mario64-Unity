using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script takes care of the camera's follow movement + rotation
/// based on mouse movement.
/// 
/// @author ShifatKhan
/// @special thanks to https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 120f;
    
    public GameObject followTarget;

    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float sensitivity = 150f;

    private Vector2 rotation;

    private void Start()
    {
        if (followTarget == null)
            followTarget = GameObject.FindGameObjectWithTag("Player");

        rotation = transform.localRotation.eulerAngles;

        // Lock the curser in the middle and hide it.
        // Fixes issue where mouse goes out of window.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        rotation.y += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotation.x += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Restrict to min and max angle (AKA no 360 movement vertically)
        rotation.x = Mathf.Clamp(rotation.x, -clampAngle, clampAngle);

        // Rotate camera
        Quaternion localRotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        // Translate camera towards target.
        transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position, cameraSpeed * Time.deltaTime);
    }
}
