// 이 파일은 게임의 전반적인 것을 모두 관리해주는 파일로, 게임 난이도 조정, 전체적인 흐름제어, 점수 제어 등을 합니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MainCam;
    public GameObject SpeedUp;

    public bool isGameOver;
    [HideInInspector] public float scrollSpeed;
    //[HideInInspector] public float JJSpeed;

    [HideInInspector] public float baseGravity;
    [HideInInspector] public float enforcedGravity;

    public GameObject GameOverScreen;
    public bool afterOneSec;
    public bool instantiateLock = true;

    public float fromTime = 0.9f;
    public float toTime = 1.2f;

    public GameObject Sky;
    public Animator SkyAnim;
    public GameObject Day;
    public Animator DayAnim;
    public GameObject Night;
    //public Animator NightAnim;
    public bool isNight;

    public int heartCount = 3;
    public GameObject[] heartIcon;
    public GameObject justWhite;
    public GameObject WinUI;

    private AudioSource audioSource;

    public Sprite[] whiteNums;
    public Sprite[] grayNums;

    public Animator scoreAnim;

    public bool isBlinking;
    private int tmpScore;
    public GameObject ScoreCounter;
    public GameObject HighCounter;
    private int score;
    public int Score
    {
        get => score;
        set
        {
            if(value > 9999) { return; }
            score = value;

            tmpScore = score;
            if (isBlinking) { return; }
            for (int i = 0; i<4; i++)
            {
                ScoreCounter.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = whiteNums[tmpScore % 10];
                tmpScore /= 10;
            }

            if (value % 100 == 0)
            {
                SpeedUp.SetActive(true);
                MakeHundredSound();
                scoreAnim.SetTrigger("Blink");
                isBlinking = true;
                if(value % 300 == 0)
                {
                    //ShiftBG();
                }

                if (value == 100)
                {
                    fromTime = 1.2f;
                    toTime = 1.7f;

                    scrollSpeed = -11f;
                }
                else if (value == 200)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;

                    scrollSpeed = -11f;
                }
                else if (value == 300)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;

                    scrollSpeed = -12f;
                }
                else if (value == 400)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;

                    scrollSpeed = -13f;
                }
                else if (value == 500)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;

                    scrollSpeed = -14f;
                }
                else if (value == 600)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;

                    scrollSpeed = -15f;
                }
                else if (value == 700)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;
                }
                else if (value == 800)
                {
                    fromTime -= 0.05f;
                    toTime -= 0.05f;
                }
                else if (value == 900)
                {
                    toTime -= 0.01f;
                }
                else if (value == 1000)
                {
                    toTime -= 0.01f;
                }
                else if (value == 1100)
                {
                    toTime -= 0.01f;
                }
                else if (value == 1200)
                {
                    toTime -= 0.01f;
                }
                else if (value == 1300)
                {
                    scrollSpeed = -21f;
                    toTime -= 0.01f;
                }
                else if (value == 1300)
                {
                    scrollSpeed = -22f;
                    fromTime -= 0.01f;
                    toTime -= 0.01f;
                }
            }
        }
    }

    private void Awake()
    {
        //싱글톤
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Screen.SetResolution(2960, 1440, true);
        Application.targetFrameRate = 60;

        baseGravity = 8f;
        enforcedGravity = 16f;
        scrollSpeed = -10f;

        fromTime = 1.5f;
        toTime = 2f;

        heartCount = 3;

        instantiateLock = true;

        audioSource = GetComponent<AudioSource>();
    }

    private int highScore = 0;
    private void Start()
    {
        afterOneSec = false;
        highScore = PlayerPrefs.GetInt("HI",0);
        tmpScore = highScore;
        for (int i = 0; i < 4; i++)
        {
            HighCounter.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = grayNums[tmpScore % 10];
            tmpScore /= 10;
        }

        Invoke("InitActive", 0.1f);

        scoreAnim = ScoreCounter.GetComponent<Animator>();

        SkyAnim = Sky.GetComponent<Animator>();
        DayAnim = Day.GetComponent<Animator>();
        //NightAnim = Night.GetComponent<Animator>();
    }

    IEnumerator ScoreUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isGameOver) { break; }
            Score += 1;
        }
    }

    public void ScoreSave()
    {
        if(PlayerPrefs.GetInt("HI", 0) < Score)
        {
            PlayerPrefs.SetInt("HI", Score);
        }
    }

    public void GameOverScreenOn()
    {
        Invoke("GameoverAfter1Sec",0.5f);
        GameOverScreen.SetActive(true);
    }

    public void GameoverAfter1Sec()
    {
        afterOneSec = true;
    }

    public void isBlinkingOff()
    {
        isBlinking = false;
    }

    public void InitActive()
    {
        instantiateLock = false;
        StartCoroutine(ScoreUp());
    }

    public void ShiftBG()
    {
        if (!isNight)
        {
            //SkyAnim.SetTrigger("Night");
            //Night.SetActive(true);
            //NightAnim.SetTrigger("On");
            //DayAnim.SetTrigger("Off");
        }
        else
        {
            //SkyAnim.SetTrigger("Day");
            //Night.SetActive(false);
            //NightAnim.SetTrigger("Off");
            //DayAnim.SetTrigger("On");
        }

        isNight = !isNight;
    }

    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip hundredSound;
    public void MakeGameOverSound()
    {
        audioSource.PlayOneShot(dieSound, 1.5f);
    }
    public void MakeJumpSound()
    {
        audioSource.PlayOneShot(jumpSound, 1f);
    }
    public void MakeHundredSound()
    {
        audioSource.PlayOneShot(hundredSound, 1f);
    }

    public void HeartDown()
    {
        MakeGameOverSound();
        if (heartCount > 0)
        {
            heartIcon[--heartCount].SetActive(false);
        }
        if (heartCount == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        APCtrl.instance.GameOver();
    }

    public void Win()
    {
        isGameOver = true;
        justWhite.SetActive(true);
        APCtrl.instance.gameObject.GetComponent<EscapeMove>().enabled = true;
        WinUI.SetActive(true);
    }
}
