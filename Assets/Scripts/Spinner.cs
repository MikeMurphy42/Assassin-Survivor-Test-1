using UnityEngine;

public class Spinner : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float rotationSpeed = 100f;
    public bool clockwise = true;

    private void Start()
    {
        UpdateRotation();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float rotationDirection = clockwise ? 1f : -1f;
        transform.Rotate(0f, rotationDirection * rotationSpeed * Time.deltaTime, 0f);
    }
}
