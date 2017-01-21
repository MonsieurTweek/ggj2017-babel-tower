using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Public attributes
    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;

    // Use this for initialization
    void Start ()
	{
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);

    }

	// Update is called once per frame
	void Update ()
	{

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
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);

        Button selectedButton = GameObject.Find("Start").GetComponent<Button>();
        selectedButton.Select();
    }

    public void ShowCreditsPanel()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);

        Button selectedButton = GameObject.Find("Back").GetComponent<Button>();
        selectedButton.Select();
    }

    public void exitGame()
    {
        Application.Quit();
    }

}

