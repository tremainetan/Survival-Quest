using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnderworldSkeletonController : MonoBehaviour
{

	private NavMeshAgent agent;
	private Animator enemyAnimator;

	public Transform playerTransform;

	public float attackInterval;
	public float speed;

	private bool attacked = false;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		enemyAnimator = GetComponent<Animator>();
		playerTransform = PlayerMovement.instance.transform;
	}

	private void Update()
	{

		ChasePlayer();

	}

	private void ChasePlayer()
	{
		//Run Towards Player
		agent.speed = speed;
		agent.SetDestination(playerTransform.position);
		transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

		//Check if agent reached destination
		bool reachedPlayer = false;
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) reachedPlayer = true;
			}
		}
		if (reachedPlayer) AttackPlayer();
		else
		{
			enemyAnimator.SetBool("WALKING", false);
			enemyAnimator.SetBool("CHASING", true);
		}

	}
	private void AttackPlayer()
	{
		agent.SetDestination(transform.position);

		if (!attacked)
		{

			//ATTACK
			int attackID = Random.Range(1, 3);
			string attackTrigger = "ATTACK" + attackID.ToString();
			enemyAnimator.SetBool("CHASING", false);
			enemyAnimator.SetTrigger(attackTrigger);
			attacked = true;
			Invoke(nameof(ResetAttack), attackInterval);

			PlayerStats.instance.TakeDamage(1);
		}
	}

	private void ResetAttack()
	{
		attacked = false;
	}

}
