using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform, _teleportPointTransform;
    [SerializeField]
    private RectTransform _startText;
    [SerializeField]
    private GameObject _mainCam, _secondCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _mainCam.SetActive(true);
            _secondCam.SetActive(false);
            _playerTransform.position = new Vector2(_teleportPointTransform.position.x, _teleportPointTransform.position.y);
            _startText.gameObject.SetActive(false);
        }
    }
}
