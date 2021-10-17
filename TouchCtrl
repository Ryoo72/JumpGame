using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    public bool fingerDown = false;
    private float timeCounter = 0f;

    public GameObject AP;
    private Rigidbody2D rb2d;

    public int jumpCount = 0;

    void Start()
    {
        rb2d = AP.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver && GameManager.instance.afterOneSec) {
            SceneManager.LoadScene("WorkHard");
            return; 
        }

        if (GameManager.instance.isGameOver) { return; }

        if (fingerDown)
        {
            timeCounter += Time.deltaTime;
        }

        if ((Input.GetMouseButtonDown(0) && !fingerDown)||(Input.GetKeyDown(KeyCode.Space) && !fingerDown))
        {
            rb2d.gravityScale = GameManager.instance.baseGravity;
            fingerDown = true;
            Jump();
        }

        if (Input.GetMouseButtonUp(0)|| Input.GetKeyUp(KeyCode.Space))
        {
            fingerDown = false;
        }

        if(rb2d.velocity.y <= 0 && APCtrl.instance.isJumping)
        {
            rb2d.gravityScale = GameManager.instance.enforcedGravity;
        }
    }

    void Jump()
    {        
        Debug.Log("Jump");
        GameManager.instance.MakeJumpSound();
        rb2d.velocity = new Vector2(0, 24f);
    }
}
