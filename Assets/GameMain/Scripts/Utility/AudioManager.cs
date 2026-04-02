using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //将声音管理器写成单例模式
    public static AudioManager Am = null;
    //音乐播放器
    public AudioSource MusicPlayer;
    //音效播放器
    public AudioSource SoundPlayer;
    
    public static AudioManager getInstance(){
        if(Am==null){
            Am = new AudioManager();
        }
        return Am;
    }
    
    void Awake(){
        Debug.Log(transform);
        SoundPlayer = transform.GetComponent<AudioSource>();
        Debug.Log(SoundPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //播放音乐
    public void PlayMusic(string name)
    {
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }

    }

    //播放音效
    public void PlaySound(string name)
    {
        // SoundPlayer = transform.GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(name);
        Debug.Log(clip);
        Debug.Log(SoundPlayer);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }
}