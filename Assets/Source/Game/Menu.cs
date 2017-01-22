using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    // Public attributes
    public GameObject MenuPanel;
    public GameObject CreditsPanel;
    public GameObject PlayPanel;

    private GameObject selectedButtonForce;

    private Button topButton;

    // Use this for initialization
    void Start ()
	{
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        PlayPanel.SetActive(false);
        topButton = GameObject.Find("Play").GetComponent<Button>();
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

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);

        Button selectedButton = GameObject.Find("Play").GetComponent<Button>();
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

    public void ShowPlayPanel() {
        MenuPanel.SetActive(false);
        PlayPanel.SetActive(true);
        pressStart();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void pressStart() {
        StartCoroutine(pressStartCoroutine("Submit"));
    }

    IEnumerator pressStartCoroutine(string inputName) {
        yield return new WaitForSeconds(0.5f);
        while(!Input.GetButton(inputName)) {
            yield return null;
        }
        StartGame();
    }

}

