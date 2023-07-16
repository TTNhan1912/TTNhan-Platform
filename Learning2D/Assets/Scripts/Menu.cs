using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private bool isMenu = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.P)){ 
            if (!isMenu)
            {
                isMenu = true;
                menu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isMenu = false;
                menu.SetActive(false);
                Time.timeScale = 1;
            }
        }*/
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void Settings()
    {
        if (!isMenu)
        {
            isMenu = true;
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void OutSettings()
    {
        Debug.Log(isMenu);

        if (isMenu)
        {
            isMenu = false;
            menu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
