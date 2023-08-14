using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _virtualCamera;
    public void SetPlayerCameraFollow()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _virtualCamera.Follow=PlayerController.Instance.transform;
    }
}
