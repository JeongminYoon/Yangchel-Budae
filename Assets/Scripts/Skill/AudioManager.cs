using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<AudioManager>();
                if (!instance)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    //public GameObject audioEmpty;
    
    public AudioClip[] inGameAudios;
    
    AudioSource bgmAus;

   // AudioSource sfxAus;

    public float sfxValue = 0.5f;

    public AudioSource unitAus;

    public AudioSource skillAus;
   

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

    public void SkillAusSetting()
    {
        skillAus = this.gameObject.AddComponent<AudioSource>();
        

    }


    private void UnitAusVolumeSetting()
    {

        //float mouseScroll = 0f;
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    mouseScroll += Input.GetAxis("Mouse ScrollWheel") * 0.1f;
        //}
        //unitAus.volume += mouseScroll;

        unitAus.volume = sfxValue;
        skillAus.volume = sfxValue;

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
        SkillAusSetting();

    }


    
    public void BGMPlay(Enums.SceneNum sceneNum)
    {
        //bgmList[(int)sceneNum].Play();
        if (inGameAudios.Length <= 0)
        { return; } //???? ???? : 0518????
        bgmAus.Stop();
        bgmAus.PlayOneShot(inGameAudios[(int)sceneNum]);

     
        
        //???? Play?? ???? ???? ?????? ?????? ?????? ???? ?????? ????.
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
