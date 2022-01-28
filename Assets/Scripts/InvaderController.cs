using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using UnityEngine;
public class InvaderController : MonoBehaviour
{
    // Controls where player and invaders are spawned and can move
    public GameObject boundingBox;
    public GameObject SpawnStart;
    public GameObject SpawnEnd;
    public int numRows;
    public int numCols;
    float rowSpace;
    float colSpace;
    
    // Invaders
    public GameObject highInvader;
    public GameObject midInvader;
    public GameObject lowInvader;

    public List<List<GameObject>> invaderRows;
    public float invaderSpeed;
    public float timer;
    public float shootInterval;
    
    // Win lose state
    Global globalScript;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        shootInterval = 3.0f;
        numRows = 5;
        numCols = 11;
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();

        // initialize array of rows, with each row containing an invader
        invaderRows = new List<List<GameObject>>();

        invaderSpeed = 0.01f;

        Vector3 spawnStart = SpawnStart.transform.position;
        Vector3 spawnEnd = SpawnEnd.transform.position;

        rowSpace = Math.Abs(spawnEnd.z - spawnStart.z) / numRows;
        colSpace = Math.Abs(spawnEnd.x - spawnStart.x) / numCols;

        for (int row = 0; row < numRows; row++) {

            List<GameObject> invadersInRow =  new List<GameObject>();

            for (int col = 0; col < numCols; col++) {
                 
                float xPos = spawnStart.x + col * colSpace + colSpace / 2.0f;
                float zPos = spawnStart.z - row * rowSpace - rowSpace / 2.0f;

                if (row == 0) {
                    var myNewHighInvader = Instantiate( highInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewHighInvader.transform.parent = gameObject.transform;
                    invadersInRow.Add(myNewHighInvader);
                }

                if (row == 1 || row == 2) {
                    var myNewMidInvader =  Instantiate(midInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewMidInvader.transform.parent = gameObject.transform;
                    myNewMidInvader.GetComponent<MidInvader>().setRow(row);
                    invadersInRow.Add(myNewMidInvader);
                }

                if (row == 3 || row == 4) {
                    var myNewLowInvader = Instantiate( lowInvader,
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) );
                    myNewLowInvader.transform.parent = gameObject.transform;
                    myNewLowInvader.GetComponent<LowInvader>().setRow(row);
                    invadersInRow.Add(myNewLowInvader);
                }
            }
            invaderRows.Add(invadersInRow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Moves the block of invaders
        Vector3 invaderControllerPos = gameObject.transform.position;
        // Had to hardcode positions because bounding box not working here
        // BoxCollider boxCollider = boundingBox.GetComponent<BoxCollider>();
        if (invaderControllerPos.x  > 10.0f || invaderControllerPos.x < -10.0f) {
            invaderSpeed = -invaderSpeed;
            gameObject.transform.position = new Vector3(invaderControllerPos.x, invaderControllerPos.y, invaderControllerPos.z - 0.5f * colSpace);
        }
        gameObject.transform.Translate(invaderSpeed, 0, 0);

        // Remove invaders that have been destroyed
        for (int i = 0; i < invaderRows.Count(); i++) {
            for (int j = 0; j < invaderRows[i].Count(); j++) {
                if (invaderRows[i][j] == null) {
                    invaderRows[i].Remove(invaderRows[i][j]);
                    Debug.Log("row " + i + " has " + invaderRows[i].Count + " invaders left");
                }
            }
        }

        timer += Time.deltaTime;

        var rand = new System.Random();

        if (timer > shootInterval) {
            timer = 0;
            int lastRow = invaderRows.Count() - 1;
            // Randomly choose one invader from the last row to shoot
            int randomInvader = rand.Next(invaderRows[lastRow].Count);
            invaderRows[lastRow][randomInvader].GetComponent<Invader>().Shoot();
        }
        
        // Player wins if all invaders are eliminated
        if (invaderRows.Count() == 0) {
            globalScript.win();
        }
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
