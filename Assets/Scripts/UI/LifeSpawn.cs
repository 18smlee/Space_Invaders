using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawn : MonoBehaviour
{
    Global global;
    public GameObject heart;
    float xSpace;
    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();
        xSpace = 0.75f *  heart.GetComponent<RectTransform>().rect.width;
        
        for (int i = 0; i < global.numLives; i++) {
            Instantiate( heart,
                        new Vector3(transform.position.x + i * xSpace, transform.position.y, transform.position.z),
                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0))
                        );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
