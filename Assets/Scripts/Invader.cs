using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointValue;
    
    public GameObject globalObject;

    // Start is called before the first frame update
    public virtual void Start()
    {}

    // Update is called once per frame
    public virtual void Update()
    {}
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public void Die()
    { 
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }

    // If an invader collides with the ship, player loses
    public virtual void OnCollisionEnter(Collision collision)
    {
        
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship")) {
            Debug.Log("Invader has collided with ship");
            Global globalScript = globalObject.GetComponent<Global>();
            globalScript.lose();
        }
    }
}
