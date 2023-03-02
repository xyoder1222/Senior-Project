using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumpback : MonoBehaviour
{
    public Rigidbody player;
    private Vector3 playerVelocity;
    public float jumpheight = 2f;
    public float backwardsforce = -2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onAbility1()
    {
        Debug.Log("Jumpback");
        player.AddForce(backwardsforce, jumpheight, 0, ForceMode.Impulse);
    }
}
