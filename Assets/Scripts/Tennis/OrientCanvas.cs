using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientCanvas : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform (the object the canvas will face)
    public float speed = 5f; // Speed at which the canvas rotates to face the player

    void Update()
    {
        // Calculate the direction vector from the canvas to the player, ignoring the Y-axis (vertical alignment)
        Vector3 direction = player.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z); // Zero out the Y component to maintain horizontal alignment

        // Calculate the target rotation for the canvas to face away from the player
        Quaternion rotation = Quaternion.LookRotation(-1 * direction);

        // Smoothly interpolate the canvas's rotation towards the target rotation using Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}