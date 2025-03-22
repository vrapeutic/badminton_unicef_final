//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 targetPos;

    public void TeleportPlayer(Transform pos)
    {
        targetPos = pos.position; 
        player.transform.position = targetPos;
    }
}
