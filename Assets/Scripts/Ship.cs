using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float thrustSpeed;
    public float rotation;
    // Use this for initialization
    void Start()
    {
        thrustSpeed = 0.03f;
    }

    public GameObject bullet; // the GameObject to spawn
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.Translate(thrustSpeed, 0, 0);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.Translate(-thrustSpeed, 0, 0);
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Fire! " + rotation);
            /* we don�t want to spawn a Bullet inside our ship, so some
            Simple trigonometry is done here to spawn the bullet
            at the tip of where the ship is pointed.
            */
            Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, 1f);
            // instantiate the Bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();
            // set the direction the Bullet will travel in
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }

    }
}
