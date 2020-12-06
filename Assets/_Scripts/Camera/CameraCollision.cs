using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script checks if there are anything obstructing the camera's
/// view towards the player. If there is, it will move towards the player
/// until minDistance.
/// 
/// @author ShifatKhan
/// </summary>
public class CameraCollision : MonoBehaviour
{
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 4f;
    [SerializeField] private float smooth = 3f;
    private Vector3 camDirection;
    [SerializeField] private float distance;

    // TODO: Might not need.
    private CameraMovement cameraMovement;

    // Layer to enable the raycast on.
    // This fixes the issue of random zooming when looking from the top.
    [SerializeField] private LayerMask collisionMask;

    void Start()
    {
        camDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

        cameraMovement = GetComponentInParent<CameraMovement>();
    }

    void Update()
    {
        // Set the default camera position (or "desired" camera position)
        Vector3 desiredCameraPos = transform.parent.TransformPoint(camDirection * maxDistance);

        RaycastHit hit;

        // Check if there;s an obstacle between default cam pos and target.
        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, layerMask: collisionMask))
        {
            // Quick fix: take 90% of raycast hit to fix clipping.
            distance = Mathf.Clamp((hit.distance * 0.87f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, camDirection * distance, smooth * Time.deltaTime);
    }
}
