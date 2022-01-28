using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InvaderController : MonoBehaviour
{
    public GameObject boundingBox;
    public GameObject SpawnStart;
    public GameObject SpawnEnd;
    public GameObject highInvader;
    public GameObject midInvader;
    public GameObject lowInvader;
    public float invaderSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        invaderSpeed = 0.01f;
        int numRows = 5;
        int numCols = 11;
        Vector3 spawnStart = SpawnStart.transform.position;
        Vector3 spawnEnd = SpawnEnd.transform.position;

        float rowSpace = Math.Abs(spawnEnd.z - spawnStart.z) / numRows;
        float colSpace = Math.Abs(spawnEnd.x - spawnStart.x) / numCols;

        for (int row = 0; row < numRows; row++) {
            for (int col = 0; col < numCols; col++) {
                 
                float xPos = spawnStart.x + col * colSpace + colSpace / 2.0f;
                float zPos = spawnStart.z - row * rowSpace - rowSpace / 2.0f;

                if (row == 0) {
                    var myNewHighInvader = Instantiate( highInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewHighInvader.transform.parent = gameObject.transform;
                }

                if (row == 1 || row == 2) {
                    var myNewMidInvader =  Instantiate(midInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewMidInvader.transform.parent = gameObject.transform;
                }

                if (row == 3 || row == 4) {
                    var myNewLowInvader = Instantiate( lowInvader,
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewLowInvader.transform.parent = gameObject.transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Had to hardcode positions because bounding box not working here
        BoxCollider boxCollider = boundingBox.GetComponent<BoxCollider>();
        if (gameObject.transform.position.x  > 10.0f || gameObject.transform.position.x < -10.0f) {
            invaderSpeed = -invaderSpeed;
        }
        gameObject.transform.Translate(invaderSpeed, 0, 0);
        
    }

    private void OnDrawGizmos()
    {
        // draw red box around spawn box
        var startPos = this.SpawnStart.transform.position;
        var endPos = this.SpawnEnd.transform.position;
        var topRightCorner = new Vector3(endPos.x, endPos.y, startPos.z);
        var bottomLeftCorner = new Vector3(startPos.x, endPos.y, endPos.z);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPos, topRightCorner);
        Gizmos.DrawLine(topRightCorner, endPos);
        Gizmos.DrawLine(bottomLeftCorner, endPos);
        Gizmos.DrawLine(startPos, bottomLeftCorner);

    }
}
