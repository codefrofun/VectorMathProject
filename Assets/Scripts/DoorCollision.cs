using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCollision : MonoBehaviour
{
    public GameManager gameManager;
    public keyPickup keyPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameManager.HasKey)
            {
                OpenDoor();
                keyPickup.DestroyKey();
            }
            else
            {
                Debug.Log("You need the key to open this door.");
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("You opened the door!");
        gameObject.SetActive(false);
        SceneManager.LoadScene("WinScene");
    }
}
