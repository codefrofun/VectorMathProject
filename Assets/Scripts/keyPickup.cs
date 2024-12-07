using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickup : MonoBehaviour
{
    public GameManager gameManager;
    public Transform Player;
    private bool isFollowingPlayer;

    private void Update()
    {
        if(isFollowingPlayer)
        {
            transform.position = Player.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.HasKey = true;
            isFollowingPlayer = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void DestroyKey()
    {
        Destroy(gameObject);
    }
}
