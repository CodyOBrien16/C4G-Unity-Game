using UnityEngine;

public class GridSnapCamera : MonoBehaviour
{
    public Transform player;
    public Transform background;

    public float scrollSpeed = 10f;

    private Camera cam;
    private Vector3 targetPos;

    private float camWidth;
    private float camHeight;

    private float minX, maxX, minY, maxY;

  void Start()
{
    cam = Camera.main;
    camHeight = 2f * cam.orthographicSize;
    camWidth = camHeight * cam.aspect;

    Debug.Log("Camera size: " + camWidth + " x " + camHeight);

    SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
    if (bgRenderer == null)
    {
        Debug.LogError("No SpriteRenderer on background!");
        return;
    }

    Vector2 bgSize = bgRenderer.bounds.size;
    Vector3 bgCenter = bgRenderer.bounds.center;

    Debug.Log("Background size: " + bgSize);
    Debug.Log("Background center: " + bgCenter);

    minX = bgCenter.x - bgSize.x / 2f + camWidth / 2f;
    maxX = bgCenter.x + bgSize.x / 2f - camWidth / 2f;

    minY = bgCenter.y - bgSize.y / 2f + camHeight / 2f;
    maxY = bgCenter.y + bgSize.y / 2f - camHeight / 2f;

    Debug.Log("X bounds: " + minX + " to " + maxX);
    Debug.Log("Y bounds: " + minY + " to " + maxY);

    targetPos = transform.position;
}


    void LateUpdate()
    {
        Vector3 playerPos = player.position;

        // Determine which "cell" the player is in based on camera size
        int cellX = Mathf.FloorToInt(playerPos.x / camWidth);
        int cellY = Mathf.FloorToInt(playerPos.y / camHeight);

        // Calculate new target camera position
        targetPos = new Vector3(
            cellX * camWidth + camWidth / 2f,
            cellY * camHeight + camHeight / 2f,
            transform.position.z
        );

        // Clamp camera inside background bounds
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // Smoothly move toward target position
        transform.position = Vector3.Lerp(transform.position, targetPos, scrollSpeed * Time.deltaTime);
    }
}
