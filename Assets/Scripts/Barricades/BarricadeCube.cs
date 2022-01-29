using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeCube : MonoBehaviour
{
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        Destroy(gameObject);
    }
}
