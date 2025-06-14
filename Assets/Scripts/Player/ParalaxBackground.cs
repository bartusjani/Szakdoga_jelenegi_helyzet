using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxFactor;
    private Transform camera;
    private Vector3 prevCamPos;

    private const int REPEAT_BACKGROUND = 2;
    private float backgroundWidth; // Total width of repeated background

    private void Start()
    {
        camera = Camera.main.transform;
        prevCamPos = camera.position;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Simple;
        sr.size = new Vector2(sr.size.x * REPEAT_BACKGROUND, sr.size.y * REPEAT_BACKGROUND);

        float textureWidth = sr.sprite.texture.width;
        float pixelsPerUnit = sr.sprite.pixelsPerUnit;
        float originalSpriteWidth = textureWidth / pixelsPerUnit;

        // Calculate total width after repetition
        backgroundWidth = originalSpriteWidth * REPEAT_BACKGROUND;
    }

    private void LateUpdate()
    {
        Vector3 currCamPos = camera.position;
        Vector3 deltaMovement = currCamPos - prevCamPos;

        // Apply immediate parallax movement without smoothing
        transform.position -= new Vector3(
            deltaMovement.x * parallaxFactor.x,
            deltaMovement.y * parallaxFactor.y,
            0
        );

        // Calculate camera distance from background center
        float xPosDifference = currCamPos.x - transform.position.x;
        float repositionThreshold = backgroundWidth * 0.5f;

        // Reposition when camera moves beyond half background width
        if (xPosDifference > repositionThreshold)
        {
            transform.position += new Vector3(backgroundWidth, 0, 0);
        }
        else if (xPosDifference < -repositionThreshold)
        {
            transform.position -= new Vector3(backgroundWidth, 0, 0);
        }

        prevCamPos = currCamPos;
    }
}