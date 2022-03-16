using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class GotoAgent : Agent
{
    [SerializeField] Rigidbody[] motors;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);
    }

    public override async void OnActionReceived(ActionBuffers actions)
    {
        float[] torque = actions.ContinuousActions.Array;

        for(int i = 0; i<4; i++)
        
            motors[i].AddForce(motors[i].transform.up*torque[i]);
        }
    }
}
