using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowInvader : Invader
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pointValue = 10;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

     public override void setRow(int rowIn)
    {
        base.setRow(rowIn);
    }
    
}
