using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 cameraMovementVector;

    public Camera gameCamera;

    [SerializeField] private float minOrthographicSize;
    public float camerMovementSpeed = 5f;
    public float zoomSpeed;

    private static CameraMovement _instance;

    public static CameraMovement Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
        Instance = this;
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
        //if (Input.mouseScrollDelta.y != 0)
        //{
        float orthographicSize = gameCamera.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed / 100;

        Zoom(orthographicSize);
    }

    private void Zoom(float orthographicSize)
    {
        if (orthographicSize < minOrthographicSize)
        {
            orthographicSize = minOrthographicSize;
        }
        //}
        gameCamera.orthographicSize = orthographicSize;
    }

    private void Update()
    {
        CheckArrowInput();
        Move(new Vector3(CameraMovementVector.x, CameraMovementVector.y, 0));

        Zoom();
    }

    public void ZoomOn(Vector2 finalPoint, float orthographicSize = 4)
    {
        gameCamera.transform.position = new Vector3(finalPoint.x, finalPoint.y, gameCamera.transform.position.z);
        Zoom(orthographicSize);
    }
}

