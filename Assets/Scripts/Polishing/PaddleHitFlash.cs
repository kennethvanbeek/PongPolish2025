using UnityEngine;
using System.Collections;

public class PaddleHitFlash : MonoBehaviour
{
    void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnPaddleHit += FlashPaddle;
    }

    void OnDisable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnPaddleHit -= FlashPaddle;
    }

    void FlashPaddle(GameObject paddle)
    {
        StartCoroutine(FlashRoutine(paddle));
    }

    IEnumerator FlashRoutine(GameObject paddle)
    {
        var rend = paddle.GetComponent<SpriteRenderer>();
        var origScale = paddle.transform.localScale;
        if (rend != null) rend.color = Color.yellow;
        paddle.transform.localScale = origScale * 1.2f;
        yield return new WaitForSeconds(0.1f);
        if (rend != null) rend.color = Color.white;
        paddle.transform.localScale = origScale;
    }
}
