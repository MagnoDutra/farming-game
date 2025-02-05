using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private float cameraHalfWidth, cameraHalfHeight;
    [SerializeField] private Transform clampMin;
    [SerializeField] private Transform clampMax;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = new Vector3(player.position.x, player.position.y, transform.position.z);

        pos.x = Mathf.Clamp(pos.x, clampMin.position.x + cameraHalfWidth, clampMax.position.x - cameraHalfWidth);
        pos.y = Mathf.Clamp(pos.y, clampMin.position.y + cameraHalfHeight, clampMax.position.y - cameraHalfHeight);

        transform.position = pos;
    }
}
