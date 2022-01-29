using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOInvader : Invader
{
    public float prob = 0.2f;
    public float speed = 0.08f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pointValue = 50;
    }

    // Update is called once per frame
    public override void Update()
    {}
}
