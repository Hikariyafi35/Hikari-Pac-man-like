using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> Waypoint = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public Player player;
    private BaseState  _currentState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start() {
        if(player != null)
        {
            player.onPowerUpStart += startRetreating;
            player.onPowerUpStop += stopRetreating;
        }
    }
    private void Update() {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
    private void startRetreating()
    {
        SwitchState(RetreatState);
    }
    private void stopRetreating()
    {
        SwitchState(PatrolState);
    }
}
