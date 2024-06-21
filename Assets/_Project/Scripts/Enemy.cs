using UnityEngine;
using UnityEngine.AI;
using KBCore.Refs;
using Utilities;
using System.Collections;

namespace CantuniasInferno
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public partial class Enemy : Entity
    {
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Self] PlayerDetector playerDetector;
        [SerializeField, Self] Health health;
        [SerializeField, Child] Animator animator;

        [SerializeField] GameObject dropeable;
        [SerializeField] float wanderRadius = 10f;
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] float recieveDamageDuration = 1f;
        [SerializeField] float deathDuration = 1f;
        [SerializeField] int numCollectables = 1;

        StateMachine stateMachine;

        CountdownTimer attackTimer;
        CountdownTimer recieveDamageTimer;

        void OnValidate() => this.ValidateRefs();

        void Start()
        {
            attackTimer = new CountdownTimer(timeBetweenAttacks);
            recieveDamageTimer = new CountdownTimer(recieveDamageDuration);
            stateMachine = new StateMachine();    

            var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius);
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player);
            var attackState = new EnemyAttackState(this, animator, agent, playerDetector.Player);
            var recieveDamageState = new EnemyReciveDamageState(this, animator, agent, playerDetector.Player);
            var deathState = new EnemyDeathState(this, animator);

            At(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));

            At(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

            Any(recieveDamageState, new FuncPredicate(() => recieveDamageTimer.IsRunning && !health.IsDead));

            At(recieveDamageState, wanderState, new FuncPredicate(() => !recieveDamageTimer.IsRunning));

            Any(deathState, new FuncPredicate(() => health.IsDead));

            stateMachine.SetState(wanderState);
        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        void OnEnable()
        {
            health.RecieveDamage += OnRecieveDamage;
        }

        void OnDisable()
        {
            health.RecieveDamage -= OnRecieveDamage;
        }

        private void OnRecieveDamage()
        {
            recieveDamageTimer.Start();
        }

        void Update()
        {
            stateMachine.Update();
            attackTimer.Tick(Time.deltaTime);
            recieveDamageTimer.Tick(Time.deltaTime);
            if (health.IsDead)
            {
                StartCoroutine(DropCollectable());
            }
        }

        IEnumerator DropCollectable()
        {
            yield return new WaitForSeconds(deathDuration);
            Instantiate(dropeable, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        public void Attack()
        {
            if (attackTimer.IsRunning) return;

            attackTimer.Start();
            Debug.Log("Attacking player");
            playerDetector.PlayerHealth.TakeDamage(10);
        }
    }
}
