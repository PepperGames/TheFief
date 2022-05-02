using UnityEngine;

namespace SVS
{
    public class CameraMovement : MonoBehaviour
    {
        public Camera gameCamera;
        public float camerMovementSpeed = 5f;

        private void Start()
        {
            gameCamera = GetComponent<Camera>();
        }

        public void MoveCamera(Vector3 inputVector)
        {
            var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
            gameCamera.transform.position += movementVector * Time.deltaTime * camerMovementSpeed;
        }
    }
}

