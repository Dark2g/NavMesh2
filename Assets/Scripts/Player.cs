using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private const float MoveSpeed = 5f;

    public NavMeshAgent agent;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            Vector3 targetPosition = transform.position + direction;
            agent.SetDestination(targetPosition);
            agent.speed = MoveSpeed;
        }
        else
        {
            // Detiene el agente cuando no hay input
            agent.SetDestination(transform.position);
        }
    }
}
