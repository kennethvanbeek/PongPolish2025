using UnityEngine;

public class BallImpactParticles : MonoBehaviour
{
    ParticleSystem particles;
    void Start()
    {
        particles = new GameObject("ImpactParticles").AddComponent<ParticleSystem>();
        particles.gameObject.SetActive(false);
        var main = particles.main;
        main.startLifetime = 0.2f;
        main.startSpeed = 3;
        main.startSize = 0.3f;
        main.maxParticles = 30;
        var emission = particles.emission;
        emission.rateOverTime = 0;
    }

    void Update()
    {
        var ball = GameObject.FindGameObjectWithTag("Player");
        if (ball == null) return;

        if (ball.TryGetComponent<Rigidbody2D>(out var rb))
        {
            if (rb.IsSleeping()) return;
            foreach (var tag in new[] { "PlayerLeft", "PlayerRight" })
            {
                var paddle = GameObject.FindGameObjectWithTag(tag);
                if (paddle != null &&
                    Vector2.Distance(ball.transform.position, paddle.transform.position) < 1.2f &&
                    Mathf.Abs(rb.linearVelocity.x) > 0.1f)
                {
                    particles.transform.position = ball.transform.position;
                    particles.gameObject.SetActive(true);
                    particles.Emit(20);
                }
            }
        }
    }
}
