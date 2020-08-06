using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{

    public Transform prefab; // the first cube 
    public Transform crystal; // the first crystal
    public List<Transform> list = new List<Transform>();
    private int genCount = 0;
    private int delCount = 0;
    private Vector3 cubeLocation; // location of the most recently generated cube
    private GameManager gameManager;
    private bool flag = false; // used to only call Invokes Once

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); // only one game manager
        CreateRoadStart();
    }

    private void Update()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            onGameStart();
        }
    }

    void onGameStart()
    {
        if (!flag)
        {
            InvokeRepeating("CreateRoadContinuous", 0.5f, 0.5f);
            InvokeRepeating("DeleteRoadContinuous", 5f, 0.5f);
            flag = true;
        }
    }

    void CreateRoadStart()
    {
        cubeLocation = new Vector3(0, 0, 0);

        while (genCount < 15) // generate first 15 cubes
        {
           list.Add(Instantiate(prefab, nextCube(), Quaternion.Euler(0, 45, 0)));
           genCount++;
        }
    }

    public Vector3 nextCube()
    {
        int cubeGen = Random.Range(0, 9);

        if (cubeGen < 6 || genCount < 3) // (0,1,2,3,4,5) 60% chance or first 4 blocks
        {
            cubeLocation = new Vector3(cubeLocation.x + 0.707f, 0, cubeLocation.z + 0.707f); // straight
        }
        else // (6,7,8,9) 40% chance
        {
            cubeLocation = new Vector3(cubeLocation.x - 0.707f, 0, cubeLocation.z + 0.707f); // left shift
        }

        if(cubeGen == 1) //10% chance to  spawn crystal
        {
            Instantiate(crystal, new Vector3(cubeLocation.x, cubeLocation.y + 0.5416f, cubeLocation.z), Quaternion.Euler(0, 220, 0));
        }

        return cubeLocation;
    }

    public void CreateRoadContinuous()
    {
        list.Add(Instantiate(prefab, nextCube(), Quaternion.Euler(0, 45, 0)));
    }

    public void DeleteRoadContinuous()
    {
        Destroy(list[delCount].gameObject);
        delCount++;
    }
}
