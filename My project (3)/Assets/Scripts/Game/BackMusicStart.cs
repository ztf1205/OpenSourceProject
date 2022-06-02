using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*배경 음악 재생하는 스크립트
 * m버튼 입력 시, 뮤트*/

public class BackMusicStart : MonoBehaviour
{
    GameObject BackgroundMusic;         //배경음을 담당하는 오브젝트
    AudioSource backMusic;              //배경음 자체를 저장하는 변수

    // Start is called before the first frame update
    void Awake()
    {
        //게임 실행시 오브젝트를 찾아 배경음 변수에 저장
        BackgroundMusic = GameObject.Find("BackMusic");
        backMusic = BackgroundMusic.GetComponent<AudioSource>();
        
        //실행 중일시 그대로
        if (backMusic.isPlaying)
        {
            return;
        }

        // 씬 이동 시, 계속 재생
        else
        {
            backMusic.Play();
            DontDestroyOnLoad(BackgroundMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //m 버튼 입력시 뮤트
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
