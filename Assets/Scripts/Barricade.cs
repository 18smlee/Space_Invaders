using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject barricadeCube;
    // Start is called before the first frame update
    void Start()
    {
        Bounds meshBounds = gameObject.GetComponent<MeshCollider>().bounds;
        Bounds boxBounds = gameObject.GetComponent<BoxCollider>().bounds;
        Debug.Log(meshBounds);
        Debug.Log("x goes from " + boxBounds.min.x + " to " + boxBounds.max.x);
        Debug.Log("z goes from " + boxBounds.min.z + " to " + boxBounds.max.z);
        Debug.Log("x incr: " +  barricadeCube.transform.localScale.x);


        for (float x = boxBounds.min.x; x < boxBounds.max.x; x += barricadeCube.transform.localScale.x) {
            for (float z = boxBounds.min.z; z < boxBounds.max.z; z += barricadeCube.transform.localScale.z) {
                Vector3 currPos = new Vector3(x, gameObject.transform.position.y, z);
                Debug.Log(currPos);
                // if (meshBounds.Contains(currPos)) {
                    Debug.Log("hihihi");
                    Instantiate( barricadeCube,
                                 new Vector3(x, gameObject.transform.position.y, z),
                                 Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
                // }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
