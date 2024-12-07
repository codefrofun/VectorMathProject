using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
