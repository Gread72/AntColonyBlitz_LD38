using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : AbstractCharacter {

    public GameObject CurrentWayPoint;
    public GameObject[] WayPoints;

    private Animator _anim;
    private float _currentSpeed;
    private int _currentWaypointIndex = 0;

    private float _crawlingSpeed = 8;
    private float _attackSpeed = 12;

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Awake()
    {
        base.Awake();
        _anim = gameObject.GetComponent<Animator>();
    }

    protected override void Init() {
        Debug.Log("SpiderEnemy - Init()");

        CurrentWayPoint = WayPoints[_currentWaypointIndex];

        Speed = _crawlingSpeed;
    }

    void Update()
    {
        if (NavMeshAgent.isStopped)
        {
            _anim.SetFloat("Speed",0f);
        }
        else
        {
            _anim.SetFloat("Speed", 1f);
        }
    }

    protected override void PostUpdate() {
        //Debug.Log("SpiderEnemy - PostUpdate()");

        if (_currentSpeed != Speed)
        {
            NavMeshAgent.speed = Speed;

            _currentSpeed = Speed;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "TargetPoint")
        {
            //Destroy(col.gameObject);
            //NavMeshAgent.isStopped = true;
            //NavMeshAgent.speed = 1;
            if(_currentWaypointIndex < WayPoints.Length - 1) { 
                _currentWaypointIndex += 1;
            }
            else
            {
                _currentWaypointIndex = 0;
            }
            CurrentWayPoint = WayPoints[_currentWaypointIndex];
            Target = CurrentWayPoint.transform;
            Speed = _crawlingSpeed;
            _anim.SetBool("Attack", false);
        }

        if (col.tag == "Ant_M")
        {
            Target = col.transform;
            Speed = _attackSpeed;

            StartCoroutine("CheckTargetKill");
        }
    }

    IEnumerator CheckTargetKill()
    {

        yield return new WaitForSeconds(10f);

        Target = CurrentWayPoint.transform;
        _anim.SetBool("Attack", false);
        NavMeshAgent.isStopped = false;
        Speed = _crawlingSpeed;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Ant_M")
        {
            AntMember ant = col.collider.gameObject.GetComponent<AntMember>();

            if (!ant.IsKilled)
            {
                ant.Health -= HitPoints;
                _anim.SetBool("Attack", true);
            }
        }
    }
}
