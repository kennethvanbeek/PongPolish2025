using UnityEngine;
using TMPro;
using System.Collections;

public class ScorePopupEffect : MonoBehaviour
{
    private GameObject popupPrefab; // mag leeg blijven, wordt in code gemaakt

    void Start()
    {
        popupPrefab = new GameObject("ScorePopup");
        var text = popupPrefab.AddComponent<TextMeshPro>();
        text.fontSize = 5;
        text.text = "+1";
        popupPrefab.SetActive(false);
    }

    void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore += ShowPopupOnScore;
    }

    void OnDisable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnScore -= ShowPopupOnScore;
    }

    void ShowPopupOnScore(int player)
    {
        // Kies links of rechts voor popup, op basis van speler
        float x = (player == 0) ? -8f : 8f; // pas aan op jouw goal-posities
        Vector3 pos = new Vector3(x, 0, 0);

        var popup = Instantiate(popupPrefab, pos, Quaternion.identity);
        popup.SetActive(true);
        StartCoroutine(PopupRoutine(popup));
    }

    IEnumerator PopupRoutine(GameObject popup)
    {
        float t = 0;
        Vector3 startPos = popup.transform.position;
        while (t < 1)
        {
            popup.transform.position = startPos + Vector3.up * t;
            t += Time.deltaTime * 1.5f;
            yield return null;
        }
        Destroy(popup);
    }
}
