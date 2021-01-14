using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VehicleBehaviour;

public class AutoControls : MonoBehaviour
{
    public Transform trackCheckpointsContainer;
    public Transform[] trackCheckpoints;
    Transform currentCheckpoint;
    int currentCheckpointIndex = 0;
    public float reachingDistance = 4f;
    public float throttle;
    public float turn;
    WheelVehicle wheelVehicle;
    public float coeficient = 10f;
    // Start is called before the first frame update
    void Start()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
        trackCheckpoints = new Transform[trackCheckpointsContainer.childCount];
        for (int i = 0; i < trackCheckpointsContainer.childCount; i++)
            trackCheckpoints[i] = trackCheckpointsContainer.GetChild(i);

        currentCheckpoint = trackCheckpoints[currentCheckpointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = currentCheckpoint.position - transform.position;
        if (moveDir.magnitude < reachingDistance)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % trackCheckpoints.Length; //next checkpoint
            currentCheckpoint = trackCheckpoints[currentCheckpointIndex];
            moveDir = currentCheckpoint.position - transform.position;
        }

        Vector3 carSpaceDir = transform.InverseTransformVector(moveDir);
        throttle= wheelVehicle.autoThrottle = Mathf.Sign(carSpaceDir.z);
        turn= wheelVehicle.autoTurn = carSpaceDir.x* coeficient;
    }
}
