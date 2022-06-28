using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public Transform enemy;
    public Transform player;

    //Patrolling
    bool walkPointSet;
    float distanceToPlayer;
    float sightRange;
    public float enemySpeed = 3f;
    public float enemyRotationSpeed = 2.5f;
    public Vector3 walkPoint;
    public float walkPointRange;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("RigidBodyFPSController").transform;
        enemy = animator.transform;
        walkPointRange = animator.GetFloat("patrollingRange");
        sightRange = animator.GetFloat("sightRange");
    }

    void patrolling(Vector3 walkPoint)
    {
        // Turn the head straight to the player
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(walkPoint - enemy.position), enemyRotationSpeed * Time.deltaTime);

        // Run to the player
        enemy.position += enemy.forward * enemySpeed * Time.deltaTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!walkPointSet) SearchWalkPoint(animator);

        if (walkPointSet)
            patrolling(walkPoint);
        Vector3 distanceToWalkPoint = animator.transform.position - walkPoint;

        distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceToPlayer < sightRange)
        {
            animator.SetBool("isChasing", true);
        }
        //Walkpoint reached
        else if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("isPatrolling", false);
        }
    }
    private void SearchWalkPoint(Animator animator)
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(animator.transform.position.x + randomX, animator.transform.position.y, animator.transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -animator.transform.up, 2f))
            walkPointSet = true;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

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
