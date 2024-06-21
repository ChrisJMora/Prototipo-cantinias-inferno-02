using UnityEngine;

namespace CantuniasInferno
{
    public abstract class EnemyBaseState : IState
    {
        protected readonly Enemy enemy;
        protected readonly Animator animator;

        protected static readonly int walkHash = Animator.StringToHash("Walk");
        protected static readonly int runHash = Animator.StringToHash("Run");
        protected static readonly int attackHash = Animator.StringToHash("Attack");
        protected static readonly int recieveDamageHash = Animator.StringToHash("ReceiveDamage");
        protected static readonly int deathHash = Animator.StringToHash("Die");

        protected const float crossFadeDuration = 0.1f;

        protected EnemyBaseState(Enemy enemy, Animator animator)
        {
            this.enemy = enemy;
            this.animator = animator;
        }

        public virtual void FixedUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public virtual void OnEnter()
        {
            // throw new System.NotImplementedException();
        }

        public virtual void OnExit()
        {
            // throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            // throw new System.NotImplementedException();
        }
    }
}
