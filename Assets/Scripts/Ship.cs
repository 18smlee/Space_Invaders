using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float thrustSpeed;
    public float rotation;
    public GameObject bullet;
    public GameObject boundingBox;
    // Use this for initialization
    void Start()
    {
        thrustSpeed = 0.03f;
    }
    
    void Update()
    {
        BoxCollider boxCollider = boundingBox.GetComponent<BoxCollider>();
        Vector3 shipOffset = new Vector3(boxCollider.bounds.min.x * 0.1f, 0.0f, 0.0f);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            // Only move if ship is in bounding box
            if (boxCollider.bounds.Contains(gameObject.transform.position - shipOffset)) {
                gameObject.transform.Translate(thrustSpeed, 0, 0);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            // Only move if ship is in bounding box
            if (boxCollider.bounds.Contains(gameObject.transform.position + shipOffset)) {
                gameObject.transform.Translate(-thrustSpeed, 0, 0);
            }
        }

        if (Input.GetKeyDown("space"))
        {
            Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, 1f);
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            BulletScript b = obj.GetComponent<BulletScript>();
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }
    }
}
