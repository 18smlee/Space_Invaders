using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float thrustSpeed;
    public float rotation;
    public float shootInterval;
    public float timer;
    public GameObject bullet;
    public GameObject boundingBox;
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public AudioClip shootFX;
    // Use this for initialization
    void Start()
    {
        thrustSpeed = 0.05f;
        shootInterval = 0.5f;
        timer = 0.0f;
        boundingBox = GameObject.Find("BoundingBox");
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

        timer += Time.deltaTime;
        if (Input.GetKey("space"))
        {
            if (timer > shootInterval) {
                timer = 0;
                AudioSource.PlayClipAtPoint(shootFX, gameObject.transform.position);
                Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, 1.1f);
                GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
                Bullet b = obj.GetComponent<Bullet>();
                Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
                b.heading = rot;
            }
        }
    }

    public void getsHit() {
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
    }
}
