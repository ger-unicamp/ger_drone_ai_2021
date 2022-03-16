using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

public class GotoAgent : Agent
{
    [SerializeField] Rigidbody[] motors;
    [SerializeField] Vector3 rangeMin;
    [SerializeField] Vector3 rangeMax;
    [SerializeField] FloatParameter xPosition;
    [SerializeField] FloatParameter yPosition;
    [SerializeField] FloatParameter zPosition;

    Vector3 target;

    public override void OnEpisodeBegin()
    {
        target = new Vector3(xPosition.Sample(), yPosition.Sample(), zPosition.Sample());
        
        target.x *= (rangeMax.x-rangeMin.x);
        target.x += rangeMin.x;

        target.y *= (rangeMax.y-rangeMin.y);
        target.y += rangeMin.y;

        target.z *= (rangeMax.z-rangeMin.z);
        target.z += rangeMin.z; 

        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(target);
    }   

    public override async void OnActionReceived(ActionBuffers actions)
    {
        float[] force = actions.ContinuousActions.Array;

        for(int i = 0; i<4; i++)
        {
            motors[i].AddForce(motors[i].transform.up*force[i]);
        }

        float error = (transform.position-target).magnitude;
        AddReward(-error);
    }

    void OnTriggerEnter(Collider other)
    {
        AddReward(-500);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = 1;
        continuousActionsOut[1] = 1;
        continuousActionsOut[2] = 1;
        continuousActionsOut[3] = 1;
    }
}
