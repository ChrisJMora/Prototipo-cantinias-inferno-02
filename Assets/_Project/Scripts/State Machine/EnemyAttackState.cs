﻿using UnityEngine;
using UnityEngine.AI;

namespace CantuniasInferno
{
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly NavMeshAgent agent;
        private readonly Transform player;

        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator) 
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Enemy Attack State");
            animator.CrossFade(attackHash, crossFadeDuration);
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
            enemy.Attack();
        }
    }
}
