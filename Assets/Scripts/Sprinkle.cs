using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkle : MonoBehaviour
{
     public Global global;
     public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        global.score += pointValue;
        Destroy(gameObject);
    }
    
    public void OnCollisionEnter(Collision collision) {
        Debug.Log("sprinkle hit something");
    }
}
