using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager> {
    
    private AudioSource _source;
    
    public MusicManager() { }

    private bool _enable = true;

    public bool Enable
    {
        get
        {
            if (PlayerPrefs.HasKey("AntColonyBlitz_MusicEnable"))
            {
                _enable = PlayerPrefs.GetInt("AntColonyBlitz_MusicEnable") == 1 ? true : false;
            }

            return _enable;
        }
        set
        {
            _enable = value;
            int enableValue = _enable == true ? 1 : 0;
            PlayerPrefs.SetInt("AntColonyBlitz_MusicEnable", enableValue);
        }
    }

    // Use this for initialization
    void Start () {

        if (GameObject.FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        _source = GetComponent<AudioSource>();
        
        DontDestroyOnLoad(this);
	}

    public void MusicEnable(bool value)
    {
        _source.mute = value;

        Enable = !value;
    }

}
