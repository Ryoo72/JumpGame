// 이 파일은 지원 캐릭터를 전반적으로 관리해주는 파일입니다.
// 뛰기, 점프 등의 애니메이션을 관리합니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APCtrl : MonoBehaviour
{
    public static APCtrl instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    private Rigidbody2D rb2d;
    private Animator anim;

    [HideInInspector] public float groundY;

    [HideInInspector] public bool isJumping;
    public bool isLongJumping;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundY = transform.position.y; //-1.634027
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) { return; }

        if (gameObject.transform.position.y < groundY)
        {
            transform.position = new Vector2(transform.position.x, groundY);
        }
        if (gameObject.transform.position.y - groundY < 0.000001f)
        {
            isJumping = false;
            rb2d.gravityScale = GameManager.instance.baseGravity;
        }
        else
        {
            isJumping = true;
        }
    }


    public void RunAnim()
    {
        anim.SetTrigger("Run");
    }

    public void JumpAnim()
    {
        anim.ResetTrigger("Run");
        anim.SetTrigger("Jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JJ")&&!GameManager.instance.isGameOver)
        {
            GameManager.instance.MainCam.GetComponent<CamShakeSmall>().enabled = true;
        }
    }

    public void GameOver()
    {
        GameManager.instance.MakeGameOverSound();
        GameManager.instance.isGameOver = true;
        GameManager.instance.ScoreSave();
        GameManager.instance.GameOverScreenOn();
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        anim.SetFloat("Mult", 0f);
        transform.GetChild(1).GetComponent<Animator>().SetFloat("Mult", 0f);
    }
}
