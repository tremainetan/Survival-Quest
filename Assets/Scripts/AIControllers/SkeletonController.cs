using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{

	private NavMeshAgent agent;
	private Animator enemyAnimator;
	private SkinnedMeshRenderer meshRenderer;

	public LayerMask groundMask;
	public Transform playerCam;
	public Transform playerTransform;
	public Material blackEyes;
	public Material redEyes;
	public Vector3 walkPoint;

	public float playerSightDistance;
	public float walkPointRange;
	public float attackInterval;
	public float walkSpeed;
	public float runSpeed;

	private bool walkPointSet = true;
	private bool attacked = false;

	private void Start()
	{
		meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		agent = GetComponent<NavMeshAgent>();
		enemyAnimator = GetComponent<Animator>();
		playerCam = Camera.main.transform;
		playerTransform = PlayerMovement.instance.transform;
		if (walkPoint == null) SearchWalkPoint();
	}

	private void Update()
	{

		if (SkeletonSpawn.instance.night)
        {
			float distance = Vector3.Distance(playerTransform.position, transform.position);
			if (distance <= playerSightDistance) ChasePlayer();
			else Wander();
		}
		else
        {
			//Day Time
			ChangeEyeColour(blackEyes);
			agent.speed = 8f;
			enemyAnimator.SetBool("CHASING", false);
			enemyAnimator.SetBool("WALKING", true);
			Vector3 originalSpawnPosition = SkeletonSpawn.instance.skeletonSpawn.position;
			Vector3 spawnPosition = new Vector3(originalSpawnPosition.x, transform.position.y, originalSpawnPosition.z);
			agent.SetDestination(spawnPosition);

			if (!agent.pathPending)
			{
				if (agent.remainingDistance <= agent.stoppingDistance)
				{
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) Destroy(GetComponent<EnemyAttackable>().itemToDestroy);
				}
			}
		}
		

	}

	private void Wander()
    {
		//Turn Eyes to Black
		ChangeEyeColour(blackEyes);
		agent.speed = walkSpeed;
		if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
			enemyAnimator.SetBool("CHASING", false);
			enemyAnimator.SetBool("WALKING", true);
			agent.SetDestination(walkPoint);
        }

		//Check if Agent has reached destination
		//If Agent Reached, reset walkpoint for new patrol location

		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) walkPointSet = false;
			}
		}

	}

	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) walkPointSet = true;

	}

	private void ChasePlayer()
	{
		//Turn Eyes to Red
		ChangeEyeColour(redEyes);

		//Run Towards Player
		agent.speed = runSpeed;
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

	private void ChangeEyeColour(Material mat)
    {
		Material[] materials = meshRenderer.materials;
		materials[2] = mat;
		meshRenderer.materials = materials;
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
