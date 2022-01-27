using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidInvader : Invader
{
    // Start is called before the first frame update
    void Start()
    {
        pointValue = 20;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(base.speed, 0, 0);
    }
}
