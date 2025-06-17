using System.Collections;
using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    float ballVelocity = 3000;

    Rigidbody2D rb = null;
    bool isPlaying = false;
    int randInt = 0;

    int extraForce = 150;

    [SerializeField] TMP_Text[] playerScoreText = null;
    int[] playerScoreNumber = { 0, 0 };

    float ballDirSpeedP1 = 0;
    float ballDirSpeedP2 = 0;

    float dirSpeedMultiplier = 5f;

    [SerializeField] GameObject leftWall = null;
    [SerializeField] GameObject rightWall = null;

    [SerializeField] GameObject leftPlayer = null;
    [SerializeField] GameObject rightPlayer = null;

    float maxVerticalSpeed = 30;

    float ballResetDelay = 2;

    public bool IsPlaying()
    {
        return isPlaying;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        randInt = RndNmbr(1, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == leftPlayer)
            SetForce(extraForce, ballDirSpeedP2);
        else if (collision.gameObject == rightPlayer)
            SetForce(-extraForce, ballDirSpeedP1);
    }

    void SetForce(float pForce, float pSpeed)
    {
        float verticalSpeed = rb.velocity.y;

        if (verticalSpeed < 1 && verticalSpeed > -1)
            verticalSpeed = Random.Range(1, 3) * ((Random.Range(0, 2) == 0) ? 1 : -1);


        verticalSpeed += pSpeed;
        verticalSpeed = Mathf.Clamp(verticalSpeed, -maxVerticalSpeed, maxVerticalSpeed);

        rb.velocity = new(rb.velocity.x, verticalSpeed);
        rb.AddForce(new Vector3(pForce, pSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == leftWall)
            SetScore(1);

        if (other.gameObject == rightWall)
            SetScore(0);
    }

    public void SetScore(int _player)
    {
        playerScoreNumber[_player]++;
        playerScoreText[_player].text = playerScoreNumber[_player].ToString();
        StartCoroutine(ResetBall(_player));
    }

    IEnumerator ResetBall(int _p)
    {
        rb.Sleep();
        rb.WakeUp();
        rb.isKinematic = true;
        randInt = _p;

        yield return new WaitForSeconds(ballResetDelay);

        transform.position = new Vector3(0, 0, 0);
        isPlaying = false;
    }

    void Update()
    {
        ballDirSpeedP1 = Input.GetAxis(leftPlayer.GetComponent<PlayerMovement>().GetPlayerInputName()) * dirSpeedMultiplier;
        ballDirSpeedP2 = Input.GetAxis(rightPlayer.GetComponent<PlayerMovement>().GetPlayerInputName()) * dirSpeedMultiplier;

        if (Input.GetKeyDown(KeyCode.Space) && !IsPlaying())
            ShootBall();
    }

    void ShootBall()
    {
        transform.parent = null;
        isPlaying = true;
        rb.isKinematic = false;

        float _ballVelocity = (randInt == 1) ? ballVelocity : -ballVelocity;
        rb.AddForce(new Vector3(_ballVelocity, Random.Range(-2000, 2000), 0));
    }

    int RndNmbr(int _x, int _y)
    {
        return Random.Range(_x, _y);
    }
}

