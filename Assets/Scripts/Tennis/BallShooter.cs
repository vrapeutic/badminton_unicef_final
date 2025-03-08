using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab; // Prefab of the ball to be instantiated
    [SerializeField] float force; // Force applied to the ball when shot
    [SerializeField] float maxAfterHitDuration; // Maximum duration after hitting the ball
    [SerializeField] Transform[] shootDirections; // Possible directions for shooting the ball
    [SerializeField] int targetPercntage; // Percentage chance to target a specific direction
    [SerializeField] Transform directionTo_1; // Transform for the first direction option
    [SerializeField] Transform directionTo_2; // Transform for the second direction option
    [SerializeField] Transform Player; // Reference to the player (optional, not used in this script)
    [SerializeField] bool Opponent; // Whether the shooter belongs to the opponent

    float afterHitDuration; // Tracks the time after the ball is hit
    [SerializeField] OpponentAIDrriver[] opponent_AIs; // Array of opponent AI drivers

    // Called when the script starts
    void Start()
    {
        // Find all OpponentAIDrriver components in the scene
        opponent_AIs = GameObject.FindObjectsOfType<OpponentAIDrriver>();
    }

    // Shoots the ball in a specific direction
    public void ShootInDirection(Vector3 dir)
    {
        // Play ball hit sound effect
        AudioManager.Singleton.PlaySFX((int)SFX.SFX_BallHit);

        // Destroy the last ball if it exists
        Ball lastBall = GameObject.FindObjectOfType<Ball>();
        if (lastBall != null)
            Destroy(lastBall.gameObject);

        // Instantiate a new ball
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // Add force to the ball in the specified direction
        ball.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

        // Set the after-hit duration and disable ball collision temporarily
        afterHitDuration = maxAfterHitDuration;
        ball.GetComponent<Ball>().SetCollisionAvailability(false);

        // Assign the new ball to all opponent AI drivers
        for (int i = 0; i < opponent_AIs.Length; i++)
        {
            opponent_AIs[i].targetball = ball.transform;
        }
    }

    // Shoots the ball in a random or targeted direction
    public void Shoot()
    {
        // Play ball hit sound effect
        AudioManager.Singleton.PlaySFX((int)SFX.SFX_BallHit);

        // Destroy the last ball if it exists
        Ball lastBall = GameObject.FindObjectOfType<Ball>();
        if (lastBall != null)
            Destroy(lastBall.gameObject);

        Transform dir; // Variable to store the chosen direction

        if (Opponent)
        {
            // Determine the direction based on the percentage chance
            int percenatg = Random.Range(0, 101);
            if (percenatg <= targetPercntage)
            {
                dir = directionTo_1.GetChild(0).transform; // First direction
            }
            else
            {
                dir = directionTo_2.GetChild(0).transform; // Second direction
            }
        }
        else
        {
            // Choose a random direction from the array
            dir = shootDirections[Random.Range(0, shootDirections.Length)];
        }

        // Instantiate a new ball
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // Add force to the ball in the chosen direction
        ball.GetComponent<Rigidbody>().AddForce(dir.forward * force, ForceMode.Impulse);

        // Set the after-hit duration and assign the ball to all opponent AI drivers
        afterHitDuration = maxAfterHitDuration;
        for (int i = 0; i < opponent_AIs.Length; i++)
        {
            opponent_AIs[i].targetball = ball.transform;
        }
    }
}
