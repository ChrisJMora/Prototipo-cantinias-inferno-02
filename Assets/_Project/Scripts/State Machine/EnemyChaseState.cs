using UnityEngine;
using UnityEngine.AI;

namespace CantuniasInferno
{
    public class EnemyChaseState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Transform player;

        public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            // Debug.Log("Chase State");
            animator.CrossFade(runHash, crossFadeDuration);
        }

        public override void Update()
        {
            Debug.DrawLine(enemy.transform.position, player.position, Color.green);
            agent.SetDestination(player.position);
        }
    }
}
