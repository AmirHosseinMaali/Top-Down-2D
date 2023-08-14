using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxOffset = -.15f;
    Camera cam;
    private Vector2 startPos;
    Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}
