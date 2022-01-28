using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointValue;
    int row;
    Global globalScript;
    InvaderController invaderControllerScript;
    public GameObject deathExplosion;
    public AudioClip deathKnell;

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
        Destroy(gameObject);

        // If there's only 1 invader left, remove row from dictionary, otherwise subtract the num of invaders by 1
        if (invaderControllerScript.rowStates[row] == 1) {
            invaderControllerScript.rowStates.Remove(row);
        } else {
            invaderControllerScript.rowStates[row] -= 1;
        }
        

    }

    // If an invader collides with the ship, player loses
    public virtual void OnCollisionEnter(Collision collision)
    {
        
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship")) {
            Debug.Log("Invader has collided with ship");
            globalScript.lose();
        }
    }

    public virtual void setRow(int rowIn) {
        row = rowIn;
    }
}
