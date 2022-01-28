using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointValue;
    public float baseSpeed; 
    public GameObject boundingBox;

    // Start is called before the first frame update
    public virtual void Start()
    {
        baseSpeed = 0.01f;
    }

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
}
