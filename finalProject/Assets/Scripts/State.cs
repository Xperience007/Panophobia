using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public GroundEnemy enemy;

    public State(GroundEnemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void OnEnter();
    public abstract void HandleUpdate();
    public abstract void OnExit();
}

public class PatrolState : State
{
    public PatrolState(GroundEnemy enemy) : base(enemy) { }

    public override void OnEnter()
    {
        enemy.ResetWaypointIndex();
        enemy.SetNextDestination();
    }

    public override void HandleUpdate()
    {
        Debug.Log("Patrol HandleUpdate called");
        if (enemy.CanSeePlayer())
            enemy.SetState(new ChaseState(enemy));
        else
            enemy.MoveToNextWaypoint();
    }

    public override void OnExit() { }
}

public class ChaseState : State
{
    public ChaseState(GroundEnemy enemy) : base(enemy) { }

    public override void OnEnter()
    {
        enemy.agent.speed = enemy.chaseSpeed;


    }

    public override void HandleUpdate()
    {
        if (!enemy.CanSeePlayer() || enemy.IsPlayerTooFar())
        {
            enemy.SetState(new PatrolState(enemy));
        }
        else
        {
            enemy.ChasePlayer();
        }
    }


    public override void OnExit()
    {
        enemy.agent.speed = enemy.patrolSpeed;
    }
}