using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusicStart : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backMusic;

    // Start is called before the first frame update
    void Awake()
    {
        BackgroundMusic = GameObject.Find("BackMusic");
        backMusic = BackgroundMusic.GetComponent<AudioSource>();
        
        if (backMusic.isPlaying)
        {
            return;
        }
        else
        {
            backMusic.Play();
            DontDestroyOnLoad(BackgroundMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BackgroundMusic = GameObject.Find("BackMusic");
            backMusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
            if (backMusic.isPlaying)
            {
                backMusic.Pause();
            }
            else
            {
                backMusic.Play();
            }
        }
    }
}
