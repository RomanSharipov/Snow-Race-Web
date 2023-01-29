using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testnavmesh : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out raycastHit))
            {
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}
