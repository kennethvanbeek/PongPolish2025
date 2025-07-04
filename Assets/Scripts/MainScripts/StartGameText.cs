using UnityEngine;

public class StartGameText : MonoBehaviour
{
    [SerializeField] GameObject playText;

    void ShowInfoText() => playText.SetActive(true);
    void HideInfoText() => playText.SetActive(false);
 
    void OnEnable()
    {
        EventManager.Instance.OnBallLaunched += HideInfoText;
        EventManager.Instance.OnBallReset += ShowInfoText;

    }
    void OnDisable()
    {
        EventManager.Instance.OnBallLaunched -= HideInfoText;
        EventManager.Instance.OnBallReset -= ShowInfoText;
    }
}
