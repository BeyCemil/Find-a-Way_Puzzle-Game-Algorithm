using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class LineRoadAlgorithm : MonoBehaviour
{
    [SerializeField] private GameObject[] lineRoads;
    private int[] randomRotations = { 0, 90, 180, 270 };
    private bool success = false;
    private Dictionary<GameObject, Quaternion> initialRotations = new Dictionary<GameObject, Quaternion>();

    private void Awake()
    {
        StoreInitialRotations();
    }

    private void Start()
    {


        RandomRotate();

        CheckRotations(); //Debugging.

    }

    private void Update()
    {
        CheckWinCondition();
    }

    //Stores initial rotations at first frame. Before RandomRotate()
    private void StoreInitialRotations()
    {
        foreach (GameObject lineRoad in lineRoads)
        {
            initialRotations[lineRoad] = lineRoad.transform.rotation;
        }
    }

    //Rotates all array objects at first frame.
    private void RandomRotate()
    {
        foreach (GameObject lineRoad in lineRoads)
        {
            int randomIndex = UnityEngine.Random.Range(0, randomRotations.Length);
            int randomRotation = randomRotations[randomIndex];

            Quaternion initialRotation = initialRotations[lineRoad];
            lineRoad.transform.rotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }

    //Checks the rotations one by one.
    private void CheckRotations()
    {
        foreach (GameObject lineRoad in lineRoads)
        {
            if (Quaternion.Angle(lineRoad.transform.rotation, initialRotations[lineRoad]) <= 0f
            || Quaternion.Angle(lineRoad.transform.rotation, initialRotations[lineRoad]) == 180f)
            {
                Debug.Log("Rotation is close to the initial rotation for " + lineRoad.name);
            }
        }
    }

    //Checks if all rotations is correct.
    private void CheckWinCondition()
    {
        success = true;

        foreach (GameObject lineRoad in lineRoads)
        {
            if (!Quaternion.Equals(lineRoad.transform.rotation, initialRotations[lineRoad])
            && !Mathf.Approximately(lineRoad.transform.rotation.eulerAngles.y, 180f)
            && !Mathf.Approximately(lineRoad.transform.rotation.eulerAngles.y, -0)
            && !Mathf.Approximately(lineRoad.transform.rotation.eulerAngles.y, -180f))
            {
                success = false;
                Debug.Log("Waiting for correct pattern");
            }
        }

        if (success)
        {
            Debug.Log("Correct! All rotations match the initial rotations.");
        }
    }
}