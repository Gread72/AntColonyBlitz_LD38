using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractCharacter : MonoBehaviour {

    NavMeshAgent _navAgent;
    public NavMeshAgent NavMeshAgent
    {
        get { return _navAgent; }
    }


    [Header("Settings")]
    public Transform Target;
    public int Health;
    public int HitPoints;

    [HideInInspector]
    public float Speed;

    protected virtual void Reset()
    {
        Health = 100;
        HitPoints = 1;
        Speed = 1;
    }

    protected virtual void Awake()
    {
        _navAgent = gameObject.GetComponent<NavMeshAgent>();

        _navAgent.isStopped = false;
    }

    void Start()
    {
        _navAgent.speed = Speed;

        Init();
    }

    protected virtual void Init() { /*Override this method*/ }
    
    void FixedUpdate()
    {
        if (Target != null)
        {
            _navAgent.SetDestination(Target.position);
        }
        else
        {
            _navAgent.isStopped = true;
        }

        PostUpdate();
    }

    protected virtual void PostUpdate() { /*Override this method*/ }
}
