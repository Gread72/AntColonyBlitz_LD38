using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip positiveHitAudio;
    public AudioClip negativeHitAudio;
    public AudioClip GameOverAudio;

    private AudioSource _source;

    public static string POS_HIT_AUDIO = "POS_HIT_AUDIO";
    public static string NEG_HIT_AUDIO = "NEG_HIT_AUDIO";
    public static string GAME_OVER_AUDIO = "GAME_OVER_AUDIO";

    private static SoundManager instance;

    private bool _enable = true;

    public static SoundManager Instance
    {
        get
        {
            return GameObject.FindObjectOfType<SoundManager>();
        }
    }

    public bool Enable
    {
        get
        {
            return _enable;
        }

        set
        {
            _enable = value;
        }
    }

    // Use this for initialization
    void Start () {
        
        if (GameObject.FindObjectsOfType<SoundManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
	}

    public void PlaySound(string tag)
    {
        if (!Enable) return;

        _source = GetComponent<AudioSource>();
        switch (tag)
        {
            case "POS_HIT_AUDIO":
                _source.PlayOneShot(positiveHitAudio);
                break;

            case "NEG_HIT_AUDIO":
                _source.PlayOneShot(negativeHitAudio);
                break;

            case "GAME_OVER_AUDIO":
                _source.PlayOneShot(GameOverAudio);
                break;
        }
    }

    public void SoundEnable(bool value)
    {
        Enable = value;
    }

    //// Update is called once per frame
    //void Update () {

    //}


}
