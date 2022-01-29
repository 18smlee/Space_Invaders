using UnityEngine;
using System.Collections;
public class BulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;
    public GameObject cam;
    Global globalScript;
    

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.CompareTag("Ship"))
        {
            Destroy(gameObject);
            Ship ship = collider.gameObject.GetComponent<Ship>();
            ship.getsHit();
            globalScript.loseLife();
        }
        else if (collider.CompareTag("Invader"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("BarricadeCube"))
        {
            BarricadeCube cube = collider.gameObject.GetComponent<BarricadeCube>();
            cube.Die();
            Destroy(gameObject);
        }
        else
        {
            // if we collided with something else, print to console
            // what the other thing was
            // Debug.Log("Collided with " + collider.tag);
        }
    }

    // Use this for initialization
    void Start()
    {
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();
        // travel straight in the z-axis
        thrust.z = 400.0f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }
    // Update is called once per frame
    void Update()
    {}
}