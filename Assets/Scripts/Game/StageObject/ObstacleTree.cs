using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObstacleTree : MonoBehaviour
{
    [SerializeField] private Image tree;
    private Color whiteAlpha0 = new Vector4(1f, 1f, 1f, 0f);
    public float duration = 1f;

    private void Awake()
    {
        duration = 1.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TweenShowTree();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TweenHideTree();
        }
    }

    void TweenShowTree()
    {
        tree.color = whiteAlpha0;
        tree.DOColor(Color.white, duration).SetEase(Ease.Linear).SetAutoKill();
    }

    void TweenHideTree()
    {
        tree.color = Color.white;
        tree.DOColor(whiteAlpha0, duration).SetEase(Ease.Linear).SetAutoKill().OnComplete(() => this.gameObject.SetActive(false));
    }
}
