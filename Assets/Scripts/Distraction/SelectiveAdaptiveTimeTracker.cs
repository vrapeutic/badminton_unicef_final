using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Used Anymore
public class SelectiveAdaptiveTimeTracker : MonoBehaviour
{

    [SerializeField] float collectiveTime;
    [SerializeField] string distractionName;
    [SerializeField] float adaptiveCollectiveTime;

    bool canCalculateAdaptiveTime;

    public float CollectiveTime => collectiveTime;

    public void SelectiveTimeTracker(float timeDelta)
    {
        collectiveTime += timeDelta;
    }

    public void EnableCanCalculateAdaptiveTime()
    {
        adaptiveCollectiveTime = 0;
        canCalculateAdaptiveTime = true;
    }

    void DisableCanCalculateAdaptiveTime()
    {
        adaptiveCollectiveTime = 0;
        canCalculateAdaptiveTime = true;
    }

    private void Update()
    {
        if (canCalculateAdaptiveTime) 
        {
            adaptiveCollectiveTime += Time.deltaTime;
        }
    }

    public void SendToCSV()
    {
        //if(!(CollectiveTime > 0)) { return; }
        CsvReadWrite.Instance.WriteDistarctionData(distractionName, collectiveTime.ToString());
        collectiveTime = 0;
    }

    public void SendAdaptiveToCSV()
    {
        if (!(adaptiveCollectiveTime > 0)) { return; }
        CsvReadWrite.Instance.WriteDistarctionData(distractionName, adaptiveCollectiveTime.ToString());
    }
}
