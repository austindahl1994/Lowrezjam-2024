using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuTiles, _collisionTiles, _objectGrid, _menuCollTiles;

    private void Start()
    {
        _collisionTiles.SetActive(false);
        _objectGrid.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 pos = new Vector2(0.5f, -1.5f);
        if (collision.CompareTag("Player"))
        {
            _menuTiles.SetActive(false);
            _menuCollTiles.SetActive(false);
            _collisionTiles.SetActive(true);
            _objectGrid.SetActive(true);
            collision.gameObject.transform.position = new Vector2(pos.x, pos.y + 0.1f);
            gameObject.SetActive(false);
        }
    }
}
