using UnityEngine;

public class RestartManager : Singleton<RestartManager>
{
    [SerializeField]
    private Spawner m_spawner;

    protected override void Init()
    {
        
    }

    public void Restart()
    {
        Debug.Assert(m_spawner != null, "spawner is null");
        m_spawner.SpawnObjects();
    }
}
