// 이 파일은 적 객체 들이 사라졌을 때 위치를 시작위치로 옮겨주는 코드입니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void OnDisable()
    {
        transform.position = initPos;
    }
}
