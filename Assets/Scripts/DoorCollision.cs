using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCollision : MonoBehaviour
{
    public GameManager gameManager;
    public keyPickup key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You opened the door!");
            if (gameManager.HasKey)
            { 
                OpenDoor();
            }
            else
            {
                Debug.Log("You need the key to open this door.");
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        key.DestroyKey();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinScene");
    }
}
