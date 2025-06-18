using UnityEngine;
using System.Collections;

public class CameraShakeEffect : MonoBehaviour
{
    Camera cam;
    Vector3 origPos;

    void Start()
    {
        cam = Camera.main;
        if (cam != null) origPos = cam.transform.position;
    }

    void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore += ShakeOnScore;
    }
    void OnDisable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore -= ShakeOnScore;
    }

    void ShakeOnScore(int playerWhoScored)
    {
        StartCoroutine(ShakeRoutine(0.25f, 0.25f));
    }

    IEnumerator ShakeRoutine(float duration, float amount)
    {
        float t = 0f;
        while (t < duration)
        {
            cam.transform.position = origPos + (Vector3)Random.insideUnitCircle * amount;
            t += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = origPos;
    }
}
