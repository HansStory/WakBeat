using System;
using UnityEngine;

public class EventParams<T> : MonoBehaviour /*where T : Enum*/
{
    [SerializeField]
    private T m_arg1;

    public T Arg1 { get { return m_arg1; } }
}

public class EventParams<T1, T2> : EventParams<T1> /*where T1 : Enum where T2 : class*/
{
    [SerializeField]
    private T2 m_arg2;

    public T2 Arg2 { get { return m_arg2; } }
}
