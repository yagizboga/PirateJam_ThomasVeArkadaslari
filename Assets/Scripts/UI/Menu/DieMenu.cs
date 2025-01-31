using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DieMenu : MonoBehaviour
{
    public GameObject dieMenuUI;
    public TextMeshProUGUI Player;

    void Start()
    {
        dieMenuUI.SetActive(false);
        Player.text = "You";
    }

    public void DieMenuOpen(string playerName)
    {
        StartCoroutine(ShowDieMenu(playerName)); 
    }

    IEnumerator ShowDieMenu(string playerName)
    {
        yield return new WaitForSeconds(2f); 

        dieMenuUI.SetActive(true);
        Player.text = playerName; 
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void Quit()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
    }
}
