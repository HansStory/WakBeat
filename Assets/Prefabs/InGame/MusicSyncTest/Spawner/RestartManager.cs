using UnityEngine;

public class RestartManager : MonoBehaviourSingleton<RestartManager>
{
    [SerializeField]
    private Spawner m_spawner;

    public void Restart()
    {
        Debug.Assert(m_spawner != null, "spawner is null");
        m_spawner.SpawnObjects();
    }
}
