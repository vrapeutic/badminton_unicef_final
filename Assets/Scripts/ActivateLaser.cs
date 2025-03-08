using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLaser : MonoBehaviour
{
    [SerializeField] GameObject Anchor_0;
    [SerializeField] GameObject Anchor_1;
    [SerializeField] GameObject b_Beam;

    [SerializeField] GameObject actualBeam;

    public bool first;

    void Start()
    {
        actualBeam.SetActive(false);
    }

    
    void Update()
    {
        /*if(Anchor_0.activeInHierarchy && Anchor_1.activeInHierarchy&& b_Beam.activeInHierarchy)
        {
            actualBeam.SetActive(true);
        }
        else
        {
            actualBeam.SetActive(false);

        }
*/
        if(first)
        {
            if (Anchor_0.activeInHierarchy)
            {
                actualBeam.SetActive(true);
            }
            else
            {
                actualBeam.SetActive(false);

            }
        }else
        {
            if (Anchor_0.activeInHierarchy && Anchor_1.activeInHierarchy && b_Beam.activeInHierarchy)
            {
                actualBeam.SetActive(true);
            }
            else
            {
                actualBeam.SetActive(false);

            }

            if (b_Beam.GetComponent<MeshRenderer>().isVisible)
                actualBeam.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
