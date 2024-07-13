using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private Vector3 offset;
    public float smoothSpeed = 0.25f; // how quickly the camera follows
    public Vector3 desiredPosition;

    // Update is called once per frame
    void LateUpdate()
    {
       // Calculate the desired position based on the current position of the goal and the offset
        Vector3 desiredPosition = goal.transform.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the goal object
        transform.LookAt(goal.transform.position);
    }
}
