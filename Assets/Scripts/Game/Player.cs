using UnityEngine;

public class Player : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("SavePoint"))
        {
            Stage.Instance.SavePointEnter();

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Stage.Instance.PlayerDie();
        }

        if (other.gameObject.CompareTag("DodgePoint"))
        {
            Stage.Instance.IntegrationChangeDirection();
        }
    }
}
