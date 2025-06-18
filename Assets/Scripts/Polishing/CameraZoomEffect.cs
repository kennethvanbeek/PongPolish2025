using UnityEngine;
using System.Collections;

public class CameraZoomEffect : MonoBehaviour
{
    Camera cam;
    float origSize;

    void Start()
    {
        cam = Camera.main;
        if (cam) origSize = cam.orthographicSize;
    }

    void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore += ZoomOnScore;
    }

    void OnDisable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore -= ZoomOnScore;
    }

    void ZoomOnScore(int playerWhoScored)
    {
        StartCoroutine(ZoomRoutine(0.75f, 0.5f));
    }

    IEnumerator ZoomRoutine(float factor, float duration)
    {
        cam.orthographicSize = origSize * factor;
        yield return new WaitForSeconds(duration);
        cam.orthographicSize = origSize;
    }
}
