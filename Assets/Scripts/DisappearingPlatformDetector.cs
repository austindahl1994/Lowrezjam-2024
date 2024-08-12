using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformDetector : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _platformCollider;
    [SerializeField]
    private SpriteRenderer _platformSprite;
    [SerializeField]
    private float _disappearanceDuration = 1f, _appearanceDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(DisappearanceCoroutine());
        }
    }

    IEnumerator DisappearanceCoroutine()
    {
        yield return new WaitForSeconds(_appearanceDuration);
        _platformCollider.isTrigger = true;
        _platformSprite.color = new Color(_platformSprite.color.r, _platformSprite.color.g, _platformSprite.color.b, 0f);
        yield return new WaitForSeconds(_disappearanceDuration);
        _platformCollider.isTrigger = false;
        _platformSprite.color = new Color(_platformSprite.color.r, _platformSprite.color.g, _platformSprite.color.b, 1f);
    }
}
