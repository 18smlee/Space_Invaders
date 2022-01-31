using System.Collections;
using System.Linq;
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
    {}

     public override void setRow(int rowIn)
    {
        base.setRow(rowIn);
    }
    public override void setCol(int colIn)
    {
        base.setCol(colIn);
    }
    
}
