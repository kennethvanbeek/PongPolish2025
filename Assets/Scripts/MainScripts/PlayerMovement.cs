using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 20f;
    [SerializeField] float yClampValue = 7.5f;
    [SerializeField] string playerInputName;

    void Update()
    {
        if (string.IsNullOrEmpty(playerInputName)) return; // checkt of inputnaam bestaat

        float input = Input.GetAxis(playerInputName);
        float newY = Mathf.Clamp(transform.position.y + (input * Time.deltaTime * playerSpeed), -yClampValue, yClampValue);
        transform.position = new Vector3(transform.position.x, newY, 0);
    }

    public string GetPlayerInputName() => playerInputName;
}
