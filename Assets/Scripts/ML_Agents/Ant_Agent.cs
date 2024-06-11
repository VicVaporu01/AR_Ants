using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class Ant_Agent : Agent
{
    private Rigidbody antRB;
    [SerializeField] private Transform foodTarget;
    [SerializeField] private FoodSpawner foodSpawner;

    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;

    private float xRange = 6.3f;
    private float zRange = 6.3f;

    private void Start()
    {
        antRB = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // foodTarget = foodSpawner.activeFood;
        // Reset the position of the agent
        transform.localPosition = new Vector3(0f, 0f, 0f);

        // Ask for new food
        // foodSpawner.SpawnFood();
        foodTarget.transform.localPosition = foodSpawner.RequestPosition();
    }

    // Info that the agent can use to make decisions
    public override void CollectObservations(VectorSensor sensor)
    {
        // First, lets add the position of the agent
        sensor.AddObservation(transform.localPosition);

        // Now, lets add the position of the food target
        sensor.AddObservation(foodTarget.transform.localPosition);
    }

    // To test by myself the project
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    // This method is to take actions based on the decision of the agent
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5f;
        antRB.velocity = new Vector3(moveX, 0, moveZ) * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food))
        {
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}