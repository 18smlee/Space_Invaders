using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
public class InvaderController : MonoBehaviour
{
    // Controls where player and invaders are spawned and can move
    public GameObject boundingBox;
    public GameObject SpawnStart;
    public GameObject SpawnEnd;

    Vector3 spawnStart;
    Vector3 spawnEnd;
    public int numRows;
    public int numCols;
    float rowSpace;
    float colSpace;
    
    // Invaders
    public GameObject highInvader;
    public GameObject midInvader;
    public GameObject lowInvader;
    public GameObject UFOInvader;

    public List<List<GameObject>> invaderRows;
    public float invaderSpeed;
    public float shootTimer;
    public float ufoTimer;
    public float shootInterval;
    public float ufoInterval;
    public float ufoSpeed;
    public bool isGoingRight;
    
    // Win lose state
    Global globalScript;
    
    // Start is called before the first frame update
    void Start()
    {
        invaderSpeed = 0.01f;
        shootTimer = 0;
        shootInterval = 3.0f;
        ufoTimer = 0;
        ufoInterval = 5.0f;
        ufoSpeed = 0.4f;
        isGoingRight = true;
        numRows = 5;
        numCols = 11;
        spawnStart = SpawnStart.transform.position;
        spawnEnd = SpawnEnd.transform.position;
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();

        // initialize array of rows, with each row containing an invader
        invaderRows = new List<List<GameObject>>();

        
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
                                                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
                    myNewHighInvader.GetComponent<HighInvader>().setRow(row);
                    myNewHighInvader.transform.parent = gameObject.transform;
                    invadersInRow.Add(myNewHighInvader);
                }

                if (row == 1 || row == 2) {
                    var myNewMidInvader =  Instantiate(midInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
                    myNewMidInvader.transform.parent = gameObject.transform;
                    myNewMidInvader.GetComponent<MidInvader>().setRow(row);
                    invadersInRow.Add(myNewMidInvader);
                }

                if (row == 3 || row == 4) {
                    var myNewLowInvader = Instantiate( lowInvader,
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
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
        UnityEngine.Bounds boxBounds = boundingBox.GetComponent<BoxCollider>().bounds;
        float spawnHalfWidth = Math.Abs(spawnEnd.x - spawnStart.x) / 2.0f;
        Debug.Log(invaderSpeed);

        if (invaderControllerPos.x + spawnHalfWidth > boxBounds.max.x || invaderControllerPos.x - spawnHalfWidth < boxBounds.min.x) {
            invaderSpeed = -invaderSpeed;
            gameObject.transform.position = new Vector3(invaderControllerPos.x, invaderControllerPos.y, invaderControllerPos.z - 0.5f * colSpace);
        }
        gameObject.transform.Translate(invaderSpeed, 0, 0);

        // Remove invaders that have been destroyed
        for (int i = 0; i < invaderRows.Count(); i++) {
            for (int j = 0; j < invaderRows[i].Count(); j++) {
                if (invaderRows[i][j] == null) {
                    invaderRows[i].Remove(invaderRows[i][j]);
                }
            }
        }

        // Invaders in the bottom row shoot at a specified interval
        shootTimer += Time.deltaTime;
        var rand = new System.Random();
        if (shootTimer > shootInterval) {
            shootTimer = 0;
            int lastRow = invaderRows.Count() - 1;
            // Randomly choose one invader from the last row to shoot
            int randomInvader = rand.Next(invaderRows[lastRow].Count);
            invaderRows[lastRow][randomInvader].GetComponent<Invader>().Shoot();
        }

        // Every frame, there is a small probability that the UFO will appear
        double ufoProb = UFOInvader.GetComponent<UFOInvader>().prob;

        ufoTimer += Time.deltaTime;
        if (ufoTimer > ufoInterval) {
            ufoTimer = 0;
            double random = rand.NextDouble();
            if (random <= ufoProb) {
                if (isGoingRight) {
                    GameObject newUFO = Instantiate( UFOInvader,
                                                new Vector3(boxBounds.min.x, boxBounds.min.y, boxBounds.max.z + rowSpace),
                                                Quaternion.AngleAxis(0, new Vector3(1, 0, 0)));
                    newUFO.GetComponent<UFOInvader>().setSpeed(ufoSpeed);
                    isGoingRight = false;
                } else {
                    GameObject newUFO = Instantiate( UFOInvader,
                                                new Vector3(boxBounds.max.x, boxBounds.min.y, boxBounds.max.z + rowSpace),
                                                Quaternion.AngleAxis(180, new Vector3(0, 0, 1)));
                    newUFO.GetComponent<UFOInvader>().setSpeed(ufoSpeed);
                    isGoingRight = true;
                }
                
            }
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
