using UnityEngine;
using System.Collections;
public class BulletScript : MonoBehaviour
{
    public bool isActive;
    public bool hasHitInvader;
    public Vector3 thrust;
    public Quaternion heading;
    public GameObject cam;
    Global globalScript;
    

    void OnCollisionEnter(Collision collision) 
    {
        Collider collider = collision.collider;
        if (isActive) {
            if (collider.CompareTag("Ship"))
            {
                Ship ship = collider.gameObject.GetComponent<Ship>();
                ship.getsHit();
                globalScript.loseLife();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Invader"))
            {
                Die();
                Invader invader = collider.gameObject.GetComponent<Invader>();
                invader.Die();
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                Destroy(gameObject);
            }
            else if (collider.CompareTag("BarricadeCube"))
            {
                BarricadeCube cube = collider.gameObject.GetComponent<BarricadeCube>();
                cube.Die();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Floor")) {
                Die();
            }
            else if (collider.CompareTag("Ceiling")) {
                Die();
            }
            else
            {
                // if we collided with something else, print to console
                // what the other thing was
                Debug.Log("Collided with " + collider.tag);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        isActive = true;
        hasHitInvader = false;
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

    void Die() {
        isActive = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}