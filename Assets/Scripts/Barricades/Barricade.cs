using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject barricadeCube;
    // Start is called before the first frame update
    void Start()
    {
        // Bounds meshBounds = gameObject.GetComponent<MeshCollider>().bounds;
        UnityEngine.Bounds boxBounds = gameObject.GetComponent<BoxCollider>().bounds;

        for (float x = boxBounds.min.x; x < boxBounds.max.x; x += barricadeCube.transform.localScale.x) {
            for (float z = boxBounds.min.z; z < boxBounds.max.z; z += barricadeCube.transform.localScale.z) {
                Vector3 currPos = new Vector3(x, gameObject.transform.position.y, z);
                // if (meshBounds.Contains(currPos)) {
                    Instantiate( barricadeCube,
                                 new Vector3(x, gameObject.transform.position.y, z),
                                 Quaternion.AngleAxis(-90, new Vector3(1, 0, 0)));
                // }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
