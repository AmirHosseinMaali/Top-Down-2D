using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }
    public void SetTransitionNum(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
}
