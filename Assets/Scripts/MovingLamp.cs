using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLamp : MonoBehaviour
{
    [SerializeField] float speed = 1;

    [SerializeField] Transform[] points;
    //[SerializeField] Transform point_1;

    Transform currentNode;
    int i = 0;
    void Start()
    {
        currentNode = points[i];
    }

    
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, point_0.position, speed * Time.deltaTime);
        
        if(!(Vector3.Distance(transform.position,currentNode.position) <= 1f))
        {
            transform.position = Vector3.Lerp(transform.position, points[i].position, speed * Time.deltaTime);
            if ((Vector3.Distance(transform.position, currentNode.position) <= 1f))
            {
                i++;
                if (i >= points.Length)
                    i = 0;
            }
        }

        currentNode = points[i];

        /*if (Vector3.Distance(transform.position, point_1.position) <= 1f)
        {
            transform.position = Vector3.Lerp(transform.position, point_0.position, speed * Time.deltaTime);
        }*/
    }
}
