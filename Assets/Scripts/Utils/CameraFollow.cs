using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private PlayerController player;

    [Header("Follow Settings")]
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset;

    [Header("Zoom Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomStep = 1.5f;
    [SerializeField] private float maxZoom = 20f;

    private float targetZoom;

    void OnEnable()
    {
        player.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDisable()
    {
        player.OnLevelUp -= OnPlayerLevelUp;
    }

    void Start()
    {
        targetZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        FollowPlayer();
        SmoothZoom();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }

    void SmoothZoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 3f);
    }

    public void OnPlayerLevelUp()
    {
        targetZoom += zoomStep;
        targetZoom = Mathf.Clamp(targetZoom, cam.orthographicSize, maxZoom);
    }
}