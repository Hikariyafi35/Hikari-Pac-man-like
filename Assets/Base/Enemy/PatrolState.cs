using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
        
    }

    public void UpdateState(Enemy enemy)
    {
        if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position)< enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        if(! _isMoving)
        {
            _isMoving = true;
            //random index waypoint
            int index = UnityEngine.Random.Range(0, enemy.Waypoint.Count);
            //set posisi waypoint index yg sudah diacak
            _destination = enemy.Waypoint[index].position;
            enemy.navMeshAgent.destination = _destination;
        }
        else
        {
            if(Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
