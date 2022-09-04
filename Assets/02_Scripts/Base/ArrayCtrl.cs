using UnityEngine;

public abstract class ArrayCtrl<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T[] m_arr = null;

    public int Length { get { return m_arr.Length; } }
    public T this[int i] { get { return m_arr[i]; } }

    private void Awake()
    {
        if(m_arr == null)
        {
            m_arr = GetComponentsInChildren<T>();
        }

        Init();
    }

    protected abstract void Init();
}
