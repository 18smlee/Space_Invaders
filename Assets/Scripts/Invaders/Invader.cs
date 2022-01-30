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
        isAlive = true;
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();
        invaderControllerScript = GameObject.Find("InvaderController").GetComponent<InvaderController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {}

    public void Die()
    {
        Debug.Log("Invader dies");
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        globalScript.score += pointValue;

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        isAlive = false;
        transform.parent = null;

        invaderControllerScript.invaderRows[row].Remove(gameObject);
        // If there's only 1 invader left, remove row from list
        if (invaderControllerScript.invaderRows[row].Count() == 0) {
           invaderControllerScript.invaderRows.RemoveAt(row);
        }
    }

    // If an invader collides with the ship, player loses
    public virtual void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship") || collider.CompareTag("BarricadeCube")) {
            if (isAlive) {
                globalScript.lose();
            }
        }
        if (collider.CompareTag("Invader")) {
            Invader other = collider.gameObject.GetComponent<Invader>();
        }
    }

    public void Shoot()
    {
        Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, -1f);
        GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
        Bullet b = obj.GetComponent<Bullet>();
        Quaternion rot = Quaternion.Euler(new Vector3(0, -180, 0));
        b.heading = rot;
    }

    public virtual void setRow(int rowIn) {
        row = rowIn;
    }
}
