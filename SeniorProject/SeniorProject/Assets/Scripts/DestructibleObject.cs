using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float explosionForce = 1f;
    public float explosionRadius = 50f;
    public int debrisCount = 10000;
    public float debrisSize = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);

            for (int i = 0; i < debrisCount; i++)
            {
                GameObject debris = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debris.transform.position = transform.position;
                debris.transform.localScale = new Vector3(debrisSize, debrisSize, debrisSize);
                Rigidbody rigidbody = debris.AddComponent<Rigidbody>();
                Vector3 explosionDirection = collision.contacts[0].normal;
                debris.GetComponent<Rigidbody>().AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
            }
        }
    }
}
