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

    public Vector3 originInScreenCoords;

    // Start is called before the first frame update
    void Start()
    {
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
                    Instantiate(
                        highInvader, 
                        new Vector3(xPos, spawnStart.y, zPos),
                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                }

                if (row == 1 || row == 2) {
                    Instantiate(
                        midInvader, 
                        new Vector3(xPos, spawnStart.y, zPos),
                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                }

                if (row == 3 || row == 4) {
                    Instantiate(
                        lowInvader, 
                        new Vector3(xPos, spawnStart.y, zPos),
                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                }
            }
               
        }
        
        // float xSpace = (SpawnEnd.transform.position.x - SpawnStart.transform.position.x) / numEachInvader;
        // float zSpace = (SpawnStart.transform.position.z - SpawnEnd.transform.position.z) / 3;

        // for (int row = 0; row < 1; row++) {
        //     for (int col = 0; col < numEachInvader; col++) {
                
        //         Vector3 invaderPos = new Vector3(col * xSpace, SpawnStart.transform.position.y, row * zSpace);
        //         Debug.Log("x coordinate " + invaderPos.x);
        //         Debug.Log("z coordinate " + invaderPos.z);

        //         Instantiate(
        //             highInvader,
        //             Camera.main.ScreenToWorldPoint(invaderPos),
        //             Quaternion.identity);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
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

        var startPos_box = this.boundingBox.transform.position;
        var endPos_box = this.boundingBox.transform.position;
        var topRightCorner_box = new Vector3(endPos_box.x, endPos_box.y, startPos_box.z);
        var bottomLeftCorner_box = new Vector3(startPos_box.x, endPos_box.y, endPos_box.z);
        var originalColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos_box, topRightCorner_box);
        Gizmos.DrawLine(topRightCorner_box, endPos_box);
        Gizmos.DrawLine(bottomLeftCorner_box, endPos_box);
        Gizmos.DrawLine(startPos_box, bottomLeftCorner_box);
        Gizmos.color = originalColor;
    }
}
