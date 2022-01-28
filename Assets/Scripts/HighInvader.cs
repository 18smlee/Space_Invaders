using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighInvader : Invader
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pointValue = 30;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
