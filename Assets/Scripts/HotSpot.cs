using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Collider collider;

    public void TranslatePlayer()
    {
        player.position = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= 1f)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }
}
