using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdsColliderController : MonoBehaviour
{
    [SerializeField] GameObject _colliderHolder; // Holder object containing the collider that will move
    [SerializeField] Collider _collider; // Reference to the specific collider that will be enabled/disabled
    [SerializeField] Transform startPoint; // The starting position of the collider
    [SerializeField] Transform endPoint; // The ending position to which the collider will move

    // Method to start moving the collider from startPoint to endPoint
    public void MoveCollider()
    {
        _colliderHolder.SetActive(true); // Activate the collider holder object
        _collider.enabled = true; // Enable the collider for interactions

        // Use LeanTween to smoothly move the collider holder along the X-axis to the end point
        LeanTween.moveX(_colliderHolder, endPoint.position.x, 6).setOnComplete(() => 
        {
            // Reset the collider holder's position back to the start point after reaching the end
            _colliderHolder.transform.position = new Vector3(startPoint.position.x, _colliderHolder.transform.position.y, _colliderHolder.transform.position.z);
            
            _collider.enabled = false; // Disable the collider to prevent further interactions
            _colliderHolder.SetActive(false); // Deactivate the collider holder object

            // Invoke the end of distraction events from the DistractionManager and EventsManager
            DistractionManager.Singleton.OnDistarctionEnd?.Invoke();
            EventsManager.OnEndDistraction?.Invoke();
        });
    }
}