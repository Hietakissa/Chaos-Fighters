using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform object1;
    [SerializeField] Transform object2;

    [Header("Variables")]
    [SerializeField] float margin;
    [SerializeField] Vector2 offset;
    [SerializeField] float followTime;
    [SerializeField] float zoomTime;
    [SerializeField] float minZoom;

    Vector2 _followVel;
    Vector2 currentPos;
    float _zoomVel;
    float currentSize;
    Camera cam;


    void Awake()
    {
        cam = GetComponent<Camera>();
        currentSize = cam.orthographicSize;
        currentPos = transform.position;
    }

    void LateUpdate()
    {
        Vector2 targetPos = ((Vector2)(object1.position + object2.position) * 0.5f) + offset;
        currentPos = Vector2.SmoothDamp(currentPos, targetPos, ref _followVel, followTime);

        float distance = Vector2.Distance(object1.position, object2.position);
        float targetSize = distance * 0.5f + margin;
        targetSize = Mathf.Max(minZoom, targetSize);
        currentSize = Mathf.SmoothDamp(currentSize, targetSize, ref _zoomVel, zoomTime);

        transform.position = new Vector3(currentPos.x, currentPos.y, -10);
        cam.orthographicSize = currentSize;
    }
}