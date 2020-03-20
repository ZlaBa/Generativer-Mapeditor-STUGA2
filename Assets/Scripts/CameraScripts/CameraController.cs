using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CameraMovement;
    public float dampTime = 0.4f;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        cameraPos = new Vector3(CameraMovement.position.x, CameraMovement.position.y, -10f);
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, cameraPos, ref velocity, dampTime);
    }
}
