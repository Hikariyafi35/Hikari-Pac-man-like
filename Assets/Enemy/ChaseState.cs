using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Chasing");
        enemy.animator.SetTrigger("ChaseState");
    }

    public void UpdateState(Enemy enemy)
    {
        if(enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.player.transform.position;
            if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position)> enemy.ChaseDistance)
            {
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Chasing");
    }
}
