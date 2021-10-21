using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType {Move, Attack}
public class EnemyController : MonoBehaviour, IPoolObject
{
    [SerializeField] private StateType state = StateType.Move;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private Transform target;

    private State[] states;
    private State currentState;
    private CharacterController character;
    private EnemyHealthController healthController;

    public float Speed => speed;
    public float Damage => damage;
    public CharacterController CharacterController => character;
    public Transform Target => target;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        healthController = GetComponent<EnemyHealthController>();
        healthController.OnDead += ReturnToPool;
        InitState();
    }

    private void OnDestroy()
    {
        healthController.OnDead -= ReturnToPool;
    }

    private void InitState()
    {
        states = new State[Enum.GetNames(typeof(StateType)).Length];
        states[0] = new MoveState(StateType.Move, this);
        states[1] = new AttackState(StateType.Attack, this);
    }

    private void Start()
    {
        ChangeState(StateType.Move);
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private State GetState(StateType state)
    {
        return states[(int)state];
    }

    public void ChangeState(StateType stateType)
    {
        ChangeState(GetState(stateType));
    }

    private void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        state = currentState.Type;
        currentState.Enter();
    }

    public void Spawn(Vector3 position, Quaternion quaternion)
    {
        transform.position = position;
        transform.rotation = quaternion;
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        Debug.Log("ReturnToPool");
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
        healthController.Health.SetMaxValue();
    }
}
