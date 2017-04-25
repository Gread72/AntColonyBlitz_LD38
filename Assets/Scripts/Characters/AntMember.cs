using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntMember : MonoBehaviour {

    NavMeshAgent _navAgent;

    [Header("Settings")]
    public Transform Target;
    public int Health;
    public int HitPoints;
    public int Speed;
    public int SandParticles;
    public bool isQueen = false;

    private Vector3 _originalPosition;
    private int _currentSpeed;

    private bool _particleFound = false;
    public bool ParticleFound
    {
        get
        {
            return _particleFound;
        }

        set
        {
            _particleFound = value;
        }
    }

    private bool _isKilled = false;
    public bool IsKilled
    {
        get
        {
            return _isKilled;
        }

        set
        {
            _isKilled = value;
        }
    }

    private bool _isDeployed = false;
    public bool IsDeployed
    {
        get
        {
            return _isDeployed;
        }

        set
        {
            _isDeployed = value;
        }
    }
    
    void Start () {
        _navAgent = gameObject.GetComponent<NavMeshAgent>();
        _navAgent.speed = Speed;

        _navAgent.isStopped = false;

        Init();
    }

    void Init()
    {
        _originalPosition = transform.position;

        Invoke("SetToDeploy", 1f);
    }

    private void SetToDeploy()
    {
        _isDeployed = true;
    }

    void Reset()
    {
        Health = 100;
        HitPoints = 1;
        Speed = 1;
        SandParticles = 0;
    }

    void Update()
    {
        if (IsKilled) return;

        Vector3 up = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, up, Color.green);

        if (Health >= 100) {
            if (Target != null) return;

            //GameManager.Instance.FindSand(gameObject);

            if (!isQueen) { 
                GameObject go = GameManager.Instance.GetNextItem(gameObject);
                if (go != null) Target = go.transform;
            }
        }

        if(Health < 0)
        {
            Debug.Log("Ant is dead");
            _navAgent.isStopped = true;
            Target = null;
            IsKilled = true;
            SoundManager.Instance.PlaySound(SoundManager.NEG_HIT_AUDIO);
            GameManager.Instance.DeadAnts += 1;
        }

        if (_currentSpeed != Speed)
        {
            _navAgent.speed = Speed;
            _currentSpeed = Speed;
        }

    }

    void FixedUpdate() {

        if (IsKilled) return;
        
        if(ParticleFound) 
        {
            _navAgent.SetDestination(_originalPosition);
        }
        else
        {
            if (Target != null)
            {
                _navAgent.SetDestination(Target.position);
            }
            else
            {
                _navAgent.SetDestination(_originalPosition);
            }
        }
    }


}
