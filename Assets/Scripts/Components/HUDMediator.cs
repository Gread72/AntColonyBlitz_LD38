using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMediator : MonoBehaviour {

    [Header("Components")]
    public Button Colony1Button;
    public Button Colony2Button;
    public Button Colony3Button;
    public Button Colony4Button;

    public Text StatusLabel;

    public Image MessageDisplay;

    public Button QuitGameButton;

    private Text _messageLabel;
    private Button _exitButton;



    private bool _gameOverOpened = false;
    
    void Awake()
    {
        _messageLabel = MessageDisplay.GetComponentInChildren<Text>();
        _exitButton = MessageDisplay.GetComponentInChildren<Button>();
    }

    void Start()
    {
        Colony1Button.onClick.AddListener(delegate { OnColonyBtnClick("Colony1Button"); } );
        Colony2Button.onClick.AddListener(delegate { OnColonyBtnClick("Colony2Button"); });
        Colony3Button.onClick.AddListener(delegate { OnColonyBtnClick("Colony3Button"); });
        Colony4Button.onClick.AddListener(delegate { OnColonyBtnClick("Colony4Button"); });

        _exitButton.onClick.AddListener(delegate { GameManager.Instance.ExitGame(); });

        QuitGameButton.onClick.AddListener(delegate { GameManager.Instance.ExitGame(); });
    }

    void OnColonyBtnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "Colony1Button":
                GameManager.Instance.DeployAnt(0);
                break;

            case "Colony2Button":
                GameManager.Instance.DeployAnt(1);
                break;

            case "Colony3Button":
                GameManager.Instance.DeployAnt(2);
                break;

            case "Colony4Button":
                GameManager.Instance.DeployAnt(3);
                break;
        }
    }

    //// Update is called once per frame
    void Update() {

        GameManager gameMgr = GameManager.Instance;

        if (gameMgr != null) { 

            string status = "Particles:\n" + gameMgr.ParticlesFound + " / "
                + gameMgr.TotalParticles + "\n\nAnts:\n" +
                (gameMgr.MaxAnts - gameMgr.AntsDeployed) + " / " + gameMgr.MaxAnts;

            if (gameMgr.DeadAnts > 0) status = status + "\n\nDead Ants:\n" + gameMgr.DeadAnts;

            StatusLabel.text = status;

            if (gameMgr.GameOver && _gameOverOpened == false)
            {
                _gameOverOpened = true;
                MessageDisplay.gameObject.SetActive(true);
                if (gameMgr.Score > gameMgr.HighScore) gameMgr.HighScore = gameMgr.Score;
                _messageLabel.text = "The blitz is finished.\n\nYour Score: " + gameMgr.Score + "\n\n" +
                    "High Score: " + gameMgr.HighScore;

                SoundManager.Instance.PlaySound(SoundManager.GAME_OVER_AUDIO);
            }
        }

        
    }


}
