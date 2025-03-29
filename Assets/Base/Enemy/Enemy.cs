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
    [HideInInspector]
    public Animator animator;
    public Enemy enemy;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
        
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
        animator.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }
    private void startRetreating()
    {
        SwitchState(RetreatState);
    }
    private void stopRetreating()
    {
        SwitchState(PatrolState);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(_currentState != RetreatState)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
}
