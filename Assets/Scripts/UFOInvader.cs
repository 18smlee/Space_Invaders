using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOInvader : Invader
{
    public GameObject boundingBox;
    public double prob;
    public float speed;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        boundingBox = GameObject.Find("BoundingBox");
        pointValue = 50;
        prob = 0.5;
    }

    // Update is called once per frame
    public override void Update()
    {
        Bounds boxBounds = boundingBox.GetComponent<BoxCollider>().bounds;
        if (transform.position.x <= boxBounds.max.x && transform.position.x >= boxBounds.min.x) {
            gameObject.transform.Translate(speed, 0, 0);
        } else {
            Destroy(gameObject);
        }
    }

    public void setSpeed(float speedIn) {
        speed = speedIn;
    }
}
