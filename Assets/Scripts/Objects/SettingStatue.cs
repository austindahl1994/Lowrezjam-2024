using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingStatue : MonoBehaviour
{
    private bool byCollider, toggled = false;

    private void Update()
    {
        if (byCollider)
        {
            if (Input.GetKeyDown(KeyCode.E) && !UIManager.Instance.inUI)
            {
                UIManager.Instance.OpenSettings();
            }
            else if (Input.GetKeyDown(KeyCode.E) && UIManager.Instance.inUI) {
                UIManager.Instance.CloseSettingMenu();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            byCollider = true;
            ToggleChildren();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            byCollider = false;
            ToggleChildren();
        }
    }

    private void ToggleChildren() {
        toggled = !toggled;
        gameObject.transform.GetChild(1).gameObject.SetActive(toggled);
        gameObject.transform.GetChild(2).gameObject.SetActive(toggled);
    }
}
