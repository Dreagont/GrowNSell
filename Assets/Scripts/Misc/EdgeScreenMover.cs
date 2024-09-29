using UnityEngine;

public class EdgeScreenMover : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float edgeThreshold = 10f; 

    private Camera mainCamera;
    private Vector3 objectPosition;

    void Start()
    {
        mainCamera = Camera.main;
        objectPosition = transform.position;
    }

    void Update()
    {
        
        Vector3 mousePos = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePos.x <= edgeThreshold) 
        {
            objectPosition.x -= moveSpeed * Time.deltaTime;
        }
        else if (mousePos.x >= screenWidth - edgeThreshold) 
        {
            objectPosition.x += moveSpeed * Time.deltaTime;
        }

        objectPosition.x = Mathf.Clamp(objectPosition.x, -8, 8);

        /* if (mousePos.y <= edgeThreshold) 
         {
             objectPosition.y -= moveSpeed * Time.deltaTime;
         }
         else if (mousePos.y >= screenHeight - edgeThreshold)
         {
             objectPosition.y += moveSpeed * Time.deltaTime;
         } */

        // Apply the updated position to the object
        transform.position = objectPosition;
    }
}
