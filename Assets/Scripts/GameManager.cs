using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool HasKey = false;

    private void Update()
    {
        {
            if(HasKey)
            {
                Debug.Log("You have the key!");
            }
        }
    }
}
