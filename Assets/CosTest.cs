using System.Collections;
using UnityEngine;

namespace Assets
{
    public class CosTest : MonoBehaviour
    {

        [SerializeField, Range(0f, 1f)]
        private float m_test;

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Mathf.Cos(m_test));
        }
    }
}