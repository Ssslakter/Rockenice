using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : StateMachineBehaviour
{
    public Transform enemy;
    public Transform player;
    float attackRange;
    float sightRange;
    float distanceToPlayer;
    float enemySpeed = 5f;
    float enemyRotationSpeed = 3.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("RigidBodyFPSController").transform;
        enemy = animator.transform;
        attackRange = animator.GetFloat("attackRange");
        sightRange = animator.GetFloat("sightRange");
        Debug.Log(enemy);
        Debug.Log(animator);
    }

    void chasePlayer(Transform player)
    {
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.position - enemy.position), enemyRotationSpeed * Time.deltaTime);
        // Run to the player
        enemy.position += enemy.forward * enemySpeed * Time.deltaTime;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        chasePlayer(player);

        distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer < attackRange)
        {
            animator.SetBool("isAttacking", true);
        }
        else if (distanceToPlayer >= sightRange)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
