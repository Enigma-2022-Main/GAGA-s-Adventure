using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    public BgmType[] BGMList;

    private AudioSource BGM;
    private int BGMCount;

    public static BackgroundMusicManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGMCount = 0;
    }

    private void Update()
    {
        if (BGMList.Length > 0)
        {
            if (!BGM.isPlaying)
            {
                BGM.clip = BGMList[BGMCount].audio;
                BGM.Play();
                BGMCount++;
                if (BGMCount == BGMList.Length)
                {
                    BGMCount = 0;
                }
            }
        }
    }
}