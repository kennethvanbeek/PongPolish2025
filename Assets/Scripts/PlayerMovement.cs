using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerSpeed = 20f;
    [SerializeField] float yClampValue = 7.5f;
    [SerializeField] string playerInputName;

    void Update()
    {
        var yPos = transform.position.y + (Input.GetAxis(playerInputName) * Time.deltaTime * playerSpeed);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(yPos, -yClampValue, yClampValue), 0);
    }

    public string GetPlayerInputName() { return playerInputName; }
}
