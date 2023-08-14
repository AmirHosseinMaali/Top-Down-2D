using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] string transitionName;
    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
