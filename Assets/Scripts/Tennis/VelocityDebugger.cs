using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDebugger : MonoBehaviour
{
    [SerializeField]
    float maxVelocity = 20f;

    private void Update()
    {
        GetComponent<Renderer>().material.color = ColorForVelocity();
    }

    Color ColorForVelocity()
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;

        return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
    }
}
