using UnityEngine;

public class BallTrailEffect : MonoBehaviour
{
    GameObject ball;
    TrailRenderer trail;

    void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBallLaunched += AddTrail;
            EventManager.Instance.OnBallReset += RemoveTrail;
        }
    }

    void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBallLaunched -= AddTrail;
            EventManager.Instance.OnBallReset -= RemoveTrail;
        }
    }

    void AddTrail()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        if (ball != null && ball.GetComponent<TrailRenderer>() == null)
        {
            trail = ball.AddComponent<TrailRenderer>();
            trail.time = 0.3f;
            trail.startWidth = 0.25f;
            trail.endWidth = 0.05f;
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.startColor = Color.white;
            trail.endColor = Color.clear;
        }
    }

    void RemoveTrail()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        if (ball != null && ball.GetComponent<TrailRenderer>() != null)
            Destroy(ball.GetComponent<TrailRenderer>());
    }
}
