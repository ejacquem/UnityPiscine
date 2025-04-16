using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointList;
    [SerializeField] private int _currentPoint = 0;
    [SerializeField] private float _visionRange = 10f;
    [SerializeField] private float _fieldOfView = 60f;
    [SerializeField] private LayerMask _obstacleMask;

    private Vector3 _origin;
    private NavMeshAgent _agent;
    private Transform _player;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _agent.SetDestination(_pointList[0].position);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (PlayerSpotted())
            SetPlayerTarget();

        Vector3 flatPos = transform.position;
        flatPos.y = 0;
        Vector3 flatTarget = _agent.destination;
        flatTarget.y = 0;

        if (Vector3.Distance(flatPos, flatTarget) <= 0.5f)
        {
            Debug.Log("Changing target");
            NextPoint();
        }        
    }

    private bool PlayerSpotted()
    {
        _origin = transform.position + Vector3.up * 0.75f;
        Vector3 directionToPlayer = _player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        Debug.DrawRay(_origin, directionToPlayer.normalized * _visionRange, Color.blue);

        if (directionToPlayer.magnitude <= _visionRange && angle <= _fieldOfView / 2f)
        {
            if (Physics.Raycast(_origin, directionToPlayer.normalized, out RaycastHit hit, _visionRange, _obstacleMask))
            {
                Debug.DrawRay(_origin, directionToPlayer.normalized * hit.distance, Color.red);
                return true;
            }
            else
            {
                Debug.DrawRay(_origin, directionToPlayer.normalized * hit.distance, Color.green);
            }
        }
        return false;
    }

    public void NextPoint()
    {
        _currentPoint++;
        _currentPoint %= _pointList.Count;
        CurrentPoint();
    }

    public void CurrentPoint()
    {
        _agent.SetDestination(_pointList[_currentPoint].position);
    }

    public void SetPlayerTarget()
    {
        _agent.SetDestination(_player.position);
    }
}
