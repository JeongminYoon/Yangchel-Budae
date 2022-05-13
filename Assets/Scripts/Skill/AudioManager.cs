using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    //public GameObject audioEmpty;
    
    public AudioClip[] inGameAudios;
    
    AudioSource bgmAus;

    AudioSource sfxAus;
    

    //public AudioSource mainMenuBgm;
    //public AudioSource cardSelectBgm;
    //public AudioSource inGameBgm;

   // public AudioSource[] bgmList = new AudioSource[4];

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

       DontDestroyOnLoad(this.gameObject);

        bgmAus = GetComponent<AudioSource>();
    }



    public void BGMPlay(Enums.SceneNum sceneNum)
    {
        //bgmList[(int)sceneNum].Play();
        bgmAus.Stop();
        bgmAus.PlayOneShot(inGameAudios[(int)sceneNum]);//�׳� Play�� �ϸ� ��� ������ ��찡 �ִٰ� �ؼ� ��ɷ� ����.
    }



   
    // Start is called before the first frame update
    void Start()
    {
      

       //bgmAus.PlayOneShot(inGameAudios[(int)GameManager.instance.curScene]);
    }

    // Update is called once per frame
    void Update()
    {


        bgmSound();



        

    }


    public void bgmSound()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            bgmAus.volume += 0.1f;

        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            bgmAus.volume -= 0.1f;

        }


    }

    public void bgmSoundSet(float i)
    {
        bgmAus.volume = i;
    }

}
