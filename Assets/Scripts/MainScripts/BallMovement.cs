using System.Collections;
using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] TMP_Text[] playerScoreText;
    [SerializeField] GameObject leftWall, rightWall, leftPlayer, rightPlayer;
    float ballVelocity = 3000, dirSpeedMultiplier = 5f, maxVerticalSpeed = 30, ballResetDelay = 2f;
    int extraForce = 150, playerKickoff, isPlaying = 0;
    int[] playerScoreNumber = { 0, 0 };

    Rigidbody2D rb;
    PlayerMovement pmLeft, pmRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pmLeft = leftPlayer.GetComponent<PlayerMovement>();
        pmRight = rightPlayer.GetComponent<PlayerMovement>();
        playerKickoff = Random.Range(1, 3);
    }

    void Start()
    {
        EventManager.Instance.OnBallLaunched += ShootBall;
        EventManager.Instance.OnScore += SetScore;
    }
    void OnDisable()
    {
        EventManager.Instance.OnBallLaunched -= ShootBall;
        EventManager.Instance.OnScore -= SetScore;
    }

    void Update()
    {
        if (pmLeft != null && pmRight != null)
        {
            float ballDirSpeedP1 = Input.GetAxis(pmLeft.GetPlayerInputName()) * dirSpeedMultiplier;
            float ballDirSpeedP2 = Input.GetAxis(pmRight.GetPlayerInputName()) * dirSpeedMultiplier;

            // Bij ruimte starten
            if (Input.GetKeyDown(KeyCode.Space) && isPlaying == 0)
                EventManager.Instance?.BallLaunched();
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject == leftPlayer)
        {
            SetForce(extraForce, GetPaddleSpeed(rightPlayer));
            EventManager.Instance?.PaddleHit(leftPlayer);
        }
        else if (c.gameObject == rightPlayer)
        {
            SetForce(-extraForce, GetPaddleSpeed(leftPlayer));
            EventManager.Instance?.PaddleHit(rightPlayer);
        }
    }

    void SetForce(float pForce, float pSpeed)
    {
        float v = rb.linearVelocity.y;
        if (Mathf.Abs(v) < 1f)
            v = Random.Range(1, 3) * (Random.Range(0, 2) == 0 ? 1 : -1);
        v = Mathf.Clamp(v + pSpeed, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = new(rb.linearVelocity.x, v);
        rb.AddForce(new Vector2(pForce, pSpeed));
    }

    float GetPaddleSpeed(GameObject player) =>
        Input.GetAxis(player.GetComponent<PlayerMovement>().GetPlayerInputName()) * dirSpeedMultiplier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == leftWall) EventManager.Instance?.Score(1);
        if (other.gameObject == rightWall) EventManager.Instance?.Score(0);
    }

    void SetScore(int p)
    {
        StartCoroutine(ResetBall());
        playerKickoff = p;
        playerScoreText[p].text = (++playerScoreNumber[p]).ToString();
    }

    IEnumerator ResetBall()
    {
        rb.Sleep();
        rb.WakeUp();
        rb.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(ballResetDelay);

        transform.position = Vector3.zero;
        isPlaying = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;

        EventManager.Instance?.BallReset();
    }

    void ShootBall()
    {
        transform.parent = null;
        isPlaying = 1;
        rb.bodyType = RigidbodyType2D.Dynamic;

        float vel = (playerKickoff == 1) ? ballVelocity : -ballVelocity;
        rb.AddForce(new Vector2(vel, Random.Range(-2000, 2000)));
    }
}
