using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStick : MonoBehaviour
{
    /*code needed in Playerinput script:
     * 
     * Quaternion rotation = Quaternion.identity;
            GameObject axeThrown = Instantiate(axe, axeSpawnPoint.position, rotation);
            axeRB = axeThrown.GetComponent<Rigidbody>();
            axeRB.isKinematic = false;

        Vector3 forceDirection = main.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(main.transform.position, main.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - axeSpawnPoint.position).normalized;
        }
            Vector3 throwVector = forceDirection * axethrowForce * throwUpForce;
            axeRB.AddForce(throwVector, ForceMode.Impulse);
            axeRB.AddTorque(transform.right * 99999999f, ForceMode.Impulse);

        AxeStick axestick = axeThrown.AddComponent<AxeStick>();
        axestick.parentTransform = transform;
        Destroy(axeThrown, 5f);
    */
    
    
    public Transform parentTransform;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the axe has hit something other than the player
        if (collision.gameObject != parentTransform.gameObject)
        {
            // Make the axe stick to the object it hit
            gameObject.transform.parent = collision.transform;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
