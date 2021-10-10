using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Level01");
    }
}
