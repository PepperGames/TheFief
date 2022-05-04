using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minOrthographicSize;

    private Vector2 cameraMovementVector;

    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }

    public Camera gameCamera;
    public float camerMovementSpeed = 5f;
    public float zoomSpeed;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }


    private void CheckArrowInput()
    {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void Move(Vector3 inputVector)
    {
        var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
        gameCamera.transform.position += movementVector * Time.deltaTime * camerMovementSpeed;
    }

    private void Zoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float orthographicSize = gameCamera.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed / 100;

            if (orthographicSize < minOrthographicSize)
            {
                orthographicSize = minOrthographicSize;
            }

            gameCamera.orthographicSize = orthographicSize;
        }
    }

    private void Update()
    {
        CheckArrowInput();
        Move(new Vector3(CameraMovementVector.x, CameraMovementVector.y, 0));

        Zoom();
    }
}

