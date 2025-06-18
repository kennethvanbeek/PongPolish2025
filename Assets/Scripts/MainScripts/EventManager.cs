using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public event Action<int> OnScore; 
    public event Action<GameObject> OnPaddleHit; 
    public event Action OnBallReset; 
    public event Action OnBallLaunched;

    public void Score(int player) => OnScore?.Invoke(player);
    public void PaddleHit(GameObject paddle) => OnPaddleHit?.Invoke(paddle);
    public void BallReset() => OnBallReset?.Invoke();
    public void BallLaunched() => OnBallLaunched?.Invoke();
}
