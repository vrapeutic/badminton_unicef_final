using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitBall : MonoBehaviour
{
    [SerializeField] OpponentAIDrriver aIDrriver; // Reference to the opponent AI driver controlling actions on ball interaction

    // Triggered when another object enters the trigger collider attached to this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a Ball component and if the AI driver has not yet struck
        if (other.gameObject.GetComponent<Ball>() != null && !aIDrriver.Strike)
        {
            // Call the action method on the opponent AI driver with the ball GameObject as an argument
            aIDrriver.ActionOnBallTouch(other.gameObject);
        }
    }
}