// 이 파일은 적 객체를 이동시켜주는 코드입니다.
// 게임이 끝났을 때 적 객체의 이동 애니메이션을 멈춰주는 역할 도 합니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchMove : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            if (this.gameObject.CompareTag("Group"))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<Animator>() != null)
                    {
                        transform.GetChild(i).GetComponent<Animator>().SetFloat("Mult", 0f);
                    }
                }
            }
            else
            {
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().SetFloat("Mult", 0f);
                }
            }
            return;
        }

        transform.Translate(new Vector2((GameManager.instance.scrollSpeed - 2), 0) * Time.deltaTime);
    }
}
