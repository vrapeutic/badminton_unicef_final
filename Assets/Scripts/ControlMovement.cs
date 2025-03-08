using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 targetPos;

    void Start()
    {
        
    }

    public void TeleportPlayer(Transform pos)
    {
        targetPos = pos.position; 
        player.transform.position = targetPos;
    }
}
