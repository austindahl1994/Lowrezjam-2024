using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformDetector : MonoBehaviour
{
    [SerializeField]
    private Animator _platAnim;
    [SerializeField]
    private BoxCollider2D _platformCollider;
    [SerializeField]
    private SpriteRenderer _platformSprite;
    [SerializeField]
    private float _disappearanceDuration = 1f, _appearanceDuration = 2f;

    private bool _trigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_trigger)
        {
            if(collision.CompareTag("Player"))
            {
                StartCoroutine(DisappearanceCoroutine());
            }
            
        }
    }

    IEnumerator DisappearanceCoroutine()
    {
        _platAnim.Play("plat_disappear");
        yield return new WaitForSeconds(_appearanceDuration);
        _trigger = false;
        _platAnim.Play("plat_disappeared");
        _platformCollider.isTrigger = true;
        yield return new WaitForSeconds(_disappearanceDuration);
        _trigger = true;
        _platformCollider.isTrigger = false;
        _platAnim.Play("plat_idle");

    }
}
