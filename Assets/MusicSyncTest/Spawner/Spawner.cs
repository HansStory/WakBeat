using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Header("Audio")]
    private AudioSource m_audio;
    [SerializeField]
    private AudioClip m_clip;

    [SerializeField, Header("Prefabs")]
    private GameObject m_ballPrefab;

    private GameObject m_ballInstance = null;

    public void SpawnObjects()
    {
        DestroyObstacles();
        if (m_ballInstance != null)
        {
            Destroy(m_ballInstance.gameObject);
        }

        m_audio.Stop();
        m_audio.clip = m_clip;
        m_audio.Play();

        m_ballInstance = Instantiate(m_ballPrefab, Vector3.zero, Quaternion.identity);
    }

    private void DestroyObstacles()
    {
        DestroyTargets("even");
        DestroyTargets("even1");
        DestroyTargets("odd");
        DestroyTargets("odd1");
    }

    private void DestroyTargets(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i].gameObject);
        }
    }
}
