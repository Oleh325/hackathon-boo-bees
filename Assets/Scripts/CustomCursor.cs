using UnityEngine;

public class CustomCursor : MonoBehaviour
{

    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPos;
    }
}
