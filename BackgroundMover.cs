// 이 파일은 배경을 움직여주는 코드입니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    Material material;
    Vector2 offset;

    private float xVelocity, yVelocity;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        xVelocity = 0.3f;
        yVelocity = 0f;
    }
    
    private void Update()
    {
        if (GameManager.instance.isGameOver) { return; }
        offset = new Vector2(xVelocity*-GameManager.instance.scrollSpeed/10, yVelocity);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
