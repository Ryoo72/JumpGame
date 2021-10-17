// 이 파일은 적객체와 부딪혔을 때 화면을 흔들어주는 역할을 합니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeSmall : MonoBehaviour
{
    // Start is called before the first frame update
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    public float shakeInit = 1f;
    // How long the object should shake for.
    private float shake;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.3f;//0.7f
    public float decreaseFactor = 1.5f;//1.5f

    Vector3 originalPos;

    void Awake()
    {
        shake = shakeInit;

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;

        StartCoroutine(ShakeShake());
    }

    void Update()
    {
        if (shake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount * shake;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    IEnumerator ShakeShake()
    {
        yield return new WaitForSeconds(shake);
        shake = shakeInit;
        camTransform.localPosition = originalPos;
        enabled = false;
    }
}
