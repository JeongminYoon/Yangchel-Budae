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

    public float sfxValue = 0.5f;

    public AudioSource unitAus;

    public AudioClip winAudio;
    public AudioClip loseAudio;

    //public AudioSource mainMenuBgm;
    //public AudioSource cardSelectBgm;
    //public AudioSource inGameBgm;

    // public AudioSource[] bgmList = new AudioSource[4];

    public void UnitAusSetting()
    {
        unitAus = this.gameObject.AddComponent<AudioSource>();
        unitAus.playOnAwake = false;
        unitAus.loop = false;
    }

    private void UnitAusVolumeSetting()
    {

        float mouseScroll = 0f;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mouseScroll += Input.GetAxis("Mouse ScrollWheel") * 0.1f;
        }
        unitAus.volume += mouseScroll;
        
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

       DontDestroyOnLoad(this.gameObject);

       bgmAus = GetComponent<AudioSource>();
		if (!bgmAus)
		{
            bgmAus = this.gameObject.AddComponent<AudioSource>();
		}


		UnitAusSetting();
    }



    public void BGMPlay(Enums.SceneNum sceneNum)
    {
        //bgmList[(int)sceneNum].Play();
        if (inGameAudios.Length <= 0)
        { return; } //오류 수정 : 0518근희
        bgmAus.Stop();
        bgmAus.PlayOneShot(inGameAudios[(int)sceneNum]);//그냥 Play로 하면 브금 깨지는 경우가 있다고 해서 요걸로 실행.
    }



    public void EndBgm()
    {





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
        UnitAusVolumeSetting();
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

    public void sfxSoundSet(float i)
    {
        sfxValue = i;
    }

}
