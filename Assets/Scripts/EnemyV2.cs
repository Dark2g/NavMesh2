using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyV2 : MonoBehaviour
{
    [SerializeField] private Transform _route;
    [SerializeField] private bool randomRoute, randomPoint;
    int index = 0;
    private Vector3 destination;
    public Vector3 minPoint, maxPoint;

    void Start()
    {
        //Va al primer punto de ruta al inicio y luego con cada nuevo index
        index = Random.Range(0, _route.childCount);
        GoToPointOfRoute();
    }

    void Update()
    {
        CalcNewPoint();
    }

    private void GoToPointOfRoute()
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void CalcNewPoint()
    {
        if (Vector3.Distance(transform.position, destination) < 2.5f)
        {
            if (randomRoute == true)
            {
                index = Random.Range(0, _route.childCount);
                destination = _route.GetChild(index).position;
            }

            else if (randomPoint == true)
            {
                destination = new Vector3(Random.Range(minPoint.x, maxPoint.x), 0, Random.Range(minPoint.z, maxPoint.z));
            }

            else if (randomPoint && randomRoute)
            {
                Vector3 newPoint = Random.insideUnitSphere * 50; //Punto aleatorio alrededor nuestro multiplicado por una distancia (radio)
                NavMeshHit hit;
                NavMesh.SamplePosition(newPoint, out hit, 50, 1); //1 -> Walkable
                destination = hit.position;
            }

            else
            {
                if (index < _route.childCount)
                    index++;

                else
                    index = 0;

                destination = _route.GetChild(index).position;
            }

            GoToPointOfRoute();
        }
    }
}
