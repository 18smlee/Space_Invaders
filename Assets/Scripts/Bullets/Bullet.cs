using UnityEngine;
using System.Collections;
public class Bullet : MonoBehaviour
{
    public bool isActive;
    public bool hasHitInvader;
    public Vector3 thrust;
    public Quaternion heading;
    public GameObject cam;
    public Global globalScript;
    public Ship ship;

    public virtual void OnCollisionEnter(Collision collision) 
    {
        Collider collider = collision.collider;
        if (isActive) 
        {
            if (collider.CompareTag("Ship"))
            {
                Ship ship = collider.gameObject.GetComponent<Ship>();
                ship.getsHit();
                globalScript.loseLife();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Invader"))
            {
                Debug.Log("Bullet hits an invader");
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
        
        // if the bullet is dead
        } else {
            if (collider.CompareTag("Ship"))
            {
                Debug.Log("ship has collected a bullet " + gameObject.name);
                ship.bulletQueue.Add(gameObject.name);
                Destroy(gameObject);
            }
        }
    }

    // Use this for initialization
    public virtual void Start()
    {
        isActive = true;
        hasHitInvader = false;
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();
        ship = GameObject.Find("vehicle_playerShip").GetComponent<Ship>();
        // travel straight in the z-axis
        thrust.z = 1500.0f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }
    // Update is called once per frame
    public virtual void Update()
    {}

    public virtual void Die() {
        isActive = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}