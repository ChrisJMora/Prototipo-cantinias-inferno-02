using UnityEngine;
using UnityEngine.AI;

namespace CantuniasInferno
{
    public class EnemyReciveDamageState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Transform player;

        public EnemyReciveDamageState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            // Debug.Log("Recive Damage State");
            animator.CrossFade(recieveDamageHash, crossFadeDuration);
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
        }
    }

    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(Enemy enemy, Animator animator) : base(enemy, animator) { }

        public override void OnEnter()
        {
            // Debug.Log("Death State");
            animator.CrossFade(deathHash, crossFadeDuration);
        }
    }

    public class EnemyWanderState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Vector3 startPoint;
        readonly float wanderRadius;

        public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent agent, float wanderRadius) : base(enemy, animator)
        {
            this.agent = agent;
            startPoint = enemy.transform.position;
            this.wanderRadius = wanderRadius;
        }

        public override void OnEnter()
        {
            // Debug.Log("Wander State");
            animator.CrossFade(walkHash, crossFadeDuration);
        }

        public override void Update()
        {
            if (HasReachDestination())
            {
                // find a new destination
                var randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += startPoint;
                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, 1);
                var finalPosition = hit.position;

                agent.SetDestination(finalPosition);
            }
        }

        bool HasReachDestination()
        {
            return !agent.pathPending 
                && agent.remainingDistance <= agent.stoppingDistance
                && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        }
    }
}
