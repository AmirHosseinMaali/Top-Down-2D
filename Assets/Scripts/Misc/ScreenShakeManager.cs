using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    CinemachineImpulseSource source;
    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<CinemachineImpulseSource>();
    }
    public void ShakeScreen()
    {
        source.GenerateImpulse();
    }
}
