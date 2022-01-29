using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    public GameObject barricade;
    public GameObject boundingBox;
    public int numBarricades;
    public float barricadeSpace;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Bounds boxBounds = boundingBox.GetComponent<BoxCollider>().bounds;

        numBarricades = 2;
        barricadeSpace = Mathf.Abs(boxBounds.max.x - boxBounds.min.x) / numBarricades;
        
        for (float x = boxBounds.min.x + barricadeSpace / 2.0f; x < boxBounds.max.x; x += barricadeSpace) {
            Instantiate(
                barricade,
                new Vector3(x, gameObject.transform.position.y, gameObject.transform.position.z),
                Quaternion.AngleAxis(180, new Vector3(0, 1, 0))
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
