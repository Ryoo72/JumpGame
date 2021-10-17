// 이 파일은 적 객체를 일정한 시간 간격으로 활성화시켜주는 역할을 합니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObj : MonoBehaviour
{
    private int activeIdx;
    public int ActiveIdx { get => activeIdx;
        set
        {
            activeIdx = value % transform.childCount;
        }
    }

    private void Awake()
    {
        ActiveIdx = Random.Range(0,transform.childCount);
    }

    private void Start()
    {
        StartCoroutine(Activation());
    }

    int i = 0;
    IEnumerator Activation()
    {
        yield return new WaitUntil(() => !GameManager.instance.instantiateLock);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(GameManager.instance.fromTime, GameManager.instance.toTime));

            if (GameManager.instance.isGameOver){ break; }

            ActiveIdx = Random.Range(0, transform.childCount);
            while (transform.GetChild(ActiveIdx).gameObject.activeSelf)
            {
                ActiveIdx++;
                i++;
                if (i > transform.childCount)
                {
                    Debug.Log("Should Add More JJ Set");
                    break;
                }
            }
            i = 0;

            transform.GetChild(ActiveIdx).gameObject.SetActive(true);
        }
    }

}
