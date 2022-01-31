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
    public GameObject invaderBullet;
    public GameObject sprinkle;

    public List<List<GameObject>> invaderGrid;
    public float invaderSpeed;
    public float shootTimer;
    public float ufoTimer;
    public float sprinkleTimer;
    public float sprinkleInterval;
    public List<int> emptyCols = new List<int>();
    public float shootInterval;
    public float ufoInterval;
    public float ufoSpeed;
    public bool isGoingRight;
    
    // Win lose state
    Global globalScript;
    
    // Start is called before the first frame update
    void Start()
    {
        invaderSpeed = 0.001f;
        shootTimer = 0;
        shootInterval = 3.0f;
        ufoTimer = 0;
        ufoInterval = 5.0f;
        ufoSpeed = 0.04f;
        sprinkleTimer = 0;
        sprinkleInterval = 5.0f;
        isGoingRight = true;
        numRows = 5;
        numCols = 11;
        spawnStart = SpawnStart.transform.position;
        spawnEnd = SpawnEnd.transform.position;
        globalScript = GameObject.Find("GlobalObject").GetComponent<Global>();

        // initialize array of rows, with each row containing an invader
        invaderGrid = new List<List<GameObject>>();

        
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
                    myNewHighInvader.GetComponent<HighInvader>().setCol(col);
                    myNewHighInvader.transform.parent = gameObject.transform;
                    invadersInRow.Add(myNewHighInvader);
                }

                if (row == 1 || row == 2) {
                    var myNewMidInvader =  Instantiate(midInvader, 
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
                    myNewMidInvader.transform.parent = gameObject.transform;
                    myNewMidInvader.GetComponent<MidInvader>().setRow(row);
                    myNewMidInvader.GetComponent<MidInvader>().setCol(col);
                    invadersInRow.Add(myNewMidInvader);
                }

                if (row == 3 || row == 4) {
                    var myNewLowInvader = Instantiate( lowInvader,
                                                        new Vector3(xPos, spawnStart.y, zPos),
                                                        Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
                    myNewLowInvader.transform.parent = gameObject.transform;
                    myNewLowInvader.GetComponent<LowInvader>().setRow(row);
                    myNewLowInvader.GetComponent<LowInvader>().setCol(col);
                    invadersInRow.Add(myNewLowInvader);
                }
            }
            invaderGrid.Add(invadersInRow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the block of invaders
        Vector3 invaderControllerPos = gameObject.transform.position;
        UnityEngine.Bounds boxBounds = boundingBox.GetComponent<BoxCollider>().bounds;
        float spawnHalfWidth = Math.Abs(spawnEnd.x - spawnStart.x) / 2.0f;

        if (invaderControllerPos.x + spawnHalfWidth > boxBounds.max.x || invaderControllerPos.x - spawnHalfWidth < boxBounds.min.x) {
            invaderSpeed = -invaderSpeed;
            gameObject.transform.position = new Vector3(invaderControllerPos.x, invaderControllerPos.y, invaderControllerPos.z - 0.5f * colSpace);
        }
        gameObject.transform.Translate(invaderSpeed, 0, 0);

        // Remove invaders that have been destroyed
        // for (int i = 0; i < invaderGrid.Count(); i++) {
        //     for (int j = 0; j < invaderGrid[i].Count(); j++) {
        //         if (invaderGrid[i][j] == null) {
        //             invaderGrid[i].Remove(invaderGrid[i][j]);
        //         }
        //     }
        // }

        // Invaders in the bottom row shoot at a specified interval
        shootTimer += Time.deltaTime;
        var rand = new System.Random();
        if (shootTimer > shootInterval) {
            shootTimer = 0;
            int lastRow = invaderGrid.Count() - 1;
            // Randomly choose one invader from the last row to shoot
            int randomInvader = rand.Next(invaderGrid[lastRow].Count);
            invaderGrid[lastRow][randomInvader].GetComponent<Invader>().Shoot(invaderBullet);
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
        // Player wins if all invaders are dead

        if (isGridEmpty()) {
            globalScript.win();
        }

        // If column is empty, then spawn sprinkles
        for (int col = 0; col < numCols; col++) {
            if (isColumnEmpty(col) && !emptyCols.Contains(col)) {
                spawnSprinkles();
                emptyCols.Add(col);
            }
        }
        
    }

    void spawnSprinkles() {
        Vector3 invaderControlPos = gameObject.transform.position;
        float spawnHalfWidth = Math.Abs(spawnEnd.x - spawnStart.x) / 2.0f;
        for (int i = 0; i < 100; i++) {
            Vector3 sprinklePos = new Vector3(  UnityEngine.Random.Range(invaderControlPos.x - spawnHalfWidth, invaderControlPos.x + spawnHalfWidth) * 1.3f,
                                                invaderControlPos.y,
                                                UnityEngine.Random.Range(15f - 2f, 15f + 2f));
            Instantiate( sprinkle,
                    sprinklePos,
                    Quaternion.AngleAxis(0, new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f))));
        }
    }

    bool isColumnEmpty(int col) {
        for (int i = 0; i < invaderGrid.Count(); i++) {
            if (invaderGrid[i][col].GetComponent<Invader>().isAlive) {
                return false;
            }
        }
        return true;
    }

    bool isGridEmpty() {
        for (int row = 0; row < numRows; row++) {
            for (int col = 0; col < numCols; col++) {
                if (invaderGrid[row][col].GetComponent<Invader>().isAlive) {
                    return false;
                }
            }
        }
        return true;
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
