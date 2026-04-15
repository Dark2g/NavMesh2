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
            // Punto aleatorio en el NavMesh (ambos flags activos): debe comprobarse primero
            if (randomPoint && randomRoute)
            {
                Vector3 newPoint = Random.insideUnitSphere * 50; // Radio de búsqueda
                NavMeshHit hit;
                NavMesh.SamplePosition(newPoint, out hit, 50, NavMesh.AllAreas);
                destination = hit.position;
            }

            // Ruta aleatoria: salta a un punto aleatorio de la lista de waypoints
            else if (randomRoute)
            {
                index = Random.Range(0, _route.childCount);
                destination = _route.GetChild(index).position;
            }

            // Ruta delimitada: punto aleatorio dentro de los límites minPoint/maxPoint
            else if (randomPoint)
            {
                destination = new Vector3(
                    Random.Range(minPoint.x, maxPoint.x),
                    0,
                    Random.Range(minPoint.z, maxPoint.z));
            }

            // Ruta secuencial: recorre los waypoints en orden
            else
            {
                index = (index + 1) % _route.childCount;
                destination = _route.GetChild(index).position;
            }

            GoToPointOfRoute();
        }
    }
}
