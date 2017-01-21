using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    // Public attributes
    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;

    private GameObject selectedButtonForce;

    private Button topButton;

    // Use this for initialization
    void Start ()
	{
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        topButton = GameObject.Find("Start").GetComponent<Button>();
    }

	// Update is called once per frame
	void Update ()
	{
        // Avoid the lose of focus after a mouse click
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            topButton.Select();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowOptionsPanel()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);

        Button selectedButton = GameObject.Find("Back").GetComponent<Button>();
        selectedButton.Select();

        topButton = OptionsPanel.GetComponentsInChildren<Button>()[0];
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);

        Button selectedButton = GameObject.Find("Start").GetComponent<Button>();
        selectedButton.Select();

        topButton = MenuPanel.GetComponentsInChildren<Button>()[0];
    }

    public void ShowCreditsPanel()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);

        Button selectedButton = GameObject.Find("Back").GetComponent<Button>();
        selectedButton.Select();

        topButton = CreditsPanel.GetComponentsInChildren<Button>()[0];
    }

    public void exitGame()
    {
        Application.Quit();
    }

}

