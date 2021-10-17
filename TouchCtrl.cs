using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public bool fingerDown = false;

    public GameObject AP;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = AP.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) { return; }

        if ((Input.GetMouseButtonDown(0) && !fingerDown)||(Input.GetKeyDown(KeyCode.Space) && !fingerDown))
        {
            if (APCtrl.instance.isJumping) { return; }

            rb2d.gravityScale = GameManager.instance.baseGravity;
            fingerDown = true;
            Jump();
        }
        if (Input.GetMouseButtonUp(0)|| Input.GetKeyUp(KeyCode.Space))
        {
            fingerDown = false;
        }

        if(rb2d.velocity.y <= 0 && APCtrl.instance.isJumping)//하강하고 있으면
        {
            rb2d.gravityScale = GameManager.instance.enforcedGravity;
        }
    }

    void Jump()
    {
        Debug.Log("Jump");
        rb2d.velocity = new Vector2(0, 24f);
    }
}
