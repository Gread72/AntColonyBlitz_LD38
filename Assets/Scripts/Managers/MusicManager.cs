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
            return _enable;
        }
    }

    // Use this for initialization
    void Start () {

        if (GameObject.FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        _source = GetComponent<AudioSource>();

        _enable = _source.mute;

        DontDestroyOnLoad(this);
	}

    public void MusicEnable(bool value)
    {
        _source.mute = value;
    }

}
