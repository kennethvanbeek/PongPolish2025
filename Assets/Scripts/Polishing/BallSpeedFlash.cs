using UnityEngine;

public class BallSpeedFlash : MonoBehaviour
{
    GameObject ball;
    SpriteRenderer rend;
    bool flashActive = false;

    void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBallLaunched += StartFlash;
            EventManager.Instance.OnBallReset += StopFlash;
        }
    }

    void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBallLaunched -= StartFlash;
            EventManager.Instance.OnBallReset -= StopFlash;
        }
    }

    void StartFlash()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        if (ball != null)
        {
            rend = ball.GetComponent<SpriteRenderer>();
            flashActive = true;
        }
    }

    void StopFlash()
    {
        flashActive = false;
        if (rend != null)
            rend.color = Color.white;
    }

    void Update()
    {
        if (!flashActive || ball == null || rend == null) return;
        if (ball.TryGetComponent<Rigidbody2D>(out var rb))
        {
            if (rb.linearVelocity.magnitude > 20f)
                rend.color = Color.red;
            else
                rend.color = Color.white;
        }
    }
}
