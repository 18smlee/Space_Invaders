using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointValue;
    public int row;
    public float rotation;
    public bool isAlive;
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public GameObject bullet;

    public Global globalScript;
    public InvaderController invaderControllerScript;

    // Start is called before the first frame update
    public virtual void Start()
    {
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();
        invaderControllerScript = GameObject.Find("InvaderController").GetComponent<InvaderController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {}

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        globalScript.score += pointValue;
        Debug.Log(invaderControllerScript.invaderRows.Count());
        
        // If there's only 1 invader left, remove row from list
        if (invaderControllerScript.invaderRows[row].Count() == 1) {
           invaderControllerScript.invaderRows.RemoveAt(row);
           Debug.Log(invaderControllerScript.invaderRows.Count());
        }

        isAlive = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        transform.parent = null;
        // Destroy(gameObject);
    }

    // If an invader collides with the ship, player loses
    public virtual void OnCollision(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship")) {
            if (isAlive) {
                globalScript.lose();
            }
        }
    }

    public void Shoot()
    {
        Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, -1f);
        GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
        BulletScript b = obj.GetComponent<BulletScript>();
        Quaternion rot = Quaternion.Euler(new Vector3(0, -180, 0));
        b.heading = rot;
    }

    public virtual void setRow(int rowIn) {
        row = rowIn;
    }
}
