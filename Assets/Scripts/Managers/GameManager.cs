using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* 
    guarantee this will be always a singleton only - can't use the constructor!
    Use this for initialization
    */
    
    [Header("Settings")]
    public List<GameObject> sandResource;
    public List<GameObject> foodResource;
    public List<AntMember> antMembers;
    public GameObject AntPrefab;
    public List<GameObject> AntColonySpawns;

    public int MaxAnts = 12;
    private int _totalParticle = 0;
    
    private int _antsDeployed = 0;
    public int AntsDeployed
    {
        get
        {
            return _antsDeployed;
        }

        set
        {
            _antsDeployed = value;
        }
    }

    private int _particlesFound = 0;
    public int ParticlesFound
    {
        get
        {
            return _particlesFound;
        }

        set
        {
            _particlesFound = value;
        }
    }
    
    public int TotalParticles
    {
        get { return _totalParticle;  }
    }
    
    private int _deadAnts = 0;
    public int DeadAnts
    {
        get
        {
            return _deadAnts;
        }

        set
        {
            _deadAnts = value;
        }
    }

    private int _numParticlesFound = 0;
    public int NumParticlesFound
    {
        get
        {
            return _numParticlesFound;
        }

        set
        {
            _numParticlesFound = value;
        }
    }

    private int _score = 0;
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
        }
    }
    
    private bool _gameOver = false;
    public bool GameOver
    {
        get
        {
            return _gameOver;
        }

        set
        {
            _gameOver = value;
        }
    }

    private int _highScore = 0;
    public int HighScore
    {
        get
        {
            if (PlayerPrefs.HasKey("AntColonyBlitz_HighScore"))
            {
                _highScore = PlayerPrefs.GetInt("AntColonyBlitz_HighScore");
            }

            return _highScore;
        }

        set
        {
            PlayerPrefs.SetInt("AntColonyBlitz_HighScore", value);
        }
    }

    public static GameManager Instance;
    
    void Awake()
    {
        Instance = this;
    }

    void Start () {
       _totalParticle = sandResource.Count;   
    }

    public GameObject GetNextItem(GameObject currentAnt)
    {
        List<SandDistance> sandDistances = new List<SandDistance>();
        GameObject currentHelper = null;
        if(sandResource.Count > 0) {
            float distance = 0;
            
            foreach (GameObject item in sandResource)
            {
                Vector3 vecDiff = currentAnt.transform.position - item.transform.position;
                distance = vecDiff.magnitude;

                sandDistances.Add(new SandDistance(item, distance));
            }

            sandDistances.Sort(
                delegate(SandDistance x, SandDistance y){
                    return x.Distance.CompareTo(y.Distance);
                });
            currentHelper = sandDistances[0].Item;
        }

        return currentHelper;
    }
    
    public void RemoveItem(GameObject currentHelper)
    {
        if (sandResource.Count > 0)
        {
            sandResource.Remove(currentHelper);
        }
    }

    void Update()
    {
        if(_particlesFound + DeadAnts >= TotalParticles && (_antsDeployed - DeadAnts) == 0) 
        {
            Debug.Log("End of Game");
            
            Score = 10 * _particlesFound;
            Score -= (DeadAnts * 10);
            if (TotalParticles == _particlesFound) Score += 20;

            GameOver = true;
            Debug.Log("Assessment score: " + Score);

        }

        if (GameOver)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void DeployAnt(int colonyNumber)
    {
        if(_antsDeployed < MaxAnts) {
            _antsDeployed += 1;
            AntMember ant = Instantiate(AntPrefab, AntColonySpawns[colonyNumber].transform.position,
                Quaternion.identity).GetComponent<AntMember>();
        }
        else
        {
            Debug.Log("Not enough ants to deploy!");
        }

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

}

public class SandDistance{

    public GameObject Item;
    public float Distance;

    public SandDistance(GameObject item, float distance)
    {
        Item = item;
        Distance = distance;
    }
}
