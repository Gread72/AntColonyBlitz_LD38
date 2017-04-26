using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroPanel : MonoBehaviour {

    public Button StartButton;
    public Button OptionsButton;
    public GameObject OptionsPanel;
    public Button CloseOptionPanelButton;
    public Toggle MusicToggle;
    public Toggle SoundToggle;

    MusicManager musicMgr;
    SoundManager soundMgr;

    // Use this for initialization
    void Start() {
        Init();
    }

    void Init()
    {
        musicMgr = GameObject.FindObjectOfType<MusicManager>();
        soundMgr = GameObject.FindObjectOfType<SoundManager>();

        StartButton.onClick.AddListener(OnStart);
        OptionsButton.onClick.AddListener(OnOptionsOpen);
        CloseOptionPanelButton.onClick.AddListener(OnOptionsClose);

        MusicToggle.onValueChanged.AddListener(OnMusicEnable);
        SoundToggle.onValueChanged.AddListener(OnSoundEnable);

        MusicToggle.isOn = musicMgr.Enable;
        SoundToggle.isOn = soundMgr.Enable;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnMusicEnable(bool value)
    {
        musicMgr.MusicEnable(!value);

        Debug.Log(value);
    }

    private void OnSoundEnable(bool value)
    {
        soundMgr.SoundEnable(value);

        Debug.Log(value);
    }

    private void OnOptionsOpen()
    {
        OptionsPanel.gameObject.SetActive(true);
    }

    private void OnOptionsClose()
    {
        OptionsPanel.gameObject.SetActive(false);
    }
    
}
