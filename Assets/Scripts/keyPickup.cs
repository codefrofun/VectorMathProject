using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickup : MonoBehaviour
{
    public GameManager gameManager;
    public Transform player;
    private bool isFollowingPlayer;

    private void Update()
    {
        if(isFollowingPlayer)
        {
            transform.position = player.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.HasKey = true;
            isFollowingPlayer = true;
            GetComponent<Collider2D>().enabled = false;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void DestroyKey()
    {
        Destroy(gameObject);
    }
}
