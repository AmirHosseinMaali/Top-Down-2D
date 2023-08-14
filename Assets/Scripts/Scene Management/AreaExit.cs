using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoadName;
    [SerializeField] string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionNum(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }
    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoadName);
    }
}
