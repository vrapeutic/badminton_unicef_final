using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollower : MonoBehaviour
{
    public Transform targetToFollow; // The object that this GameObject will follow
    public Transform targetWall; // A reference to the wall for raycasting
    public Transform targetPoint; // The point adjusted based on raycast collisions

    Vector3 startPos; // The initial position of the target point

    private void Start()
    {
        startPos = targetPoint.position; // Store the initial position of the target point
    }

    void Update()
    {
        // Check if there is a target to follow
        if (targetToFollow != null)
        {
            transform.position = targetToFollow.position; // Update this GameObject's position to match the target's position
        }
        else
        {
            // Reset the target point's position to the start position if there's no target
            targetPoint.position = startPos;
        }

        // Draw a debug ray between this GameObject and the target wall
        Debug.DrawRay(transform.position, targetWall.position, Color.blue);

        RaycastHit hit; // Variable to store raycast hit information
        // Cast a ray towards the target wall
        if (Physics.Raycast(transform.position, targetWall.position, out hit))
        {
            // Update the target point's position based on the point of collision
            targetPoint.position = new Vector3(hit.point.x, targetPoint.position.y, targetPoint.position.z);
        }
    }
}