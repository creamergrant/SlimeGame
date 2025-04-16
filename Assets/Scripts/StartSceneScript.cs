using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{

    public GameObject[] StartButtons = new GameObject[3];
    public GameObject[] OptionButtons = new GameObject[3];

    public Text[] SliderText = new Text[2];

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        SharedFloatsScript.sens = OptionButtons[2].GetComponent<Slider>().value;
        SharedFloatsScript.fov = OptionButtons[1].GetComponent<Slider>().value;
    }

    public void Options()
    {
        for(int i = 0; i < StartButtons.Length; i++)
        {
            StartButtons[i].SetActive(false);
            OptionButtons[i].SetActive(true);
        }
    }

    public void Return()
    {
        for (int i = 0; i < StartButtons.Length; i++)
        {
            StartButtons[i].SetActive(true);
            OptionButtons[i].SetActive(false);
        }
    }

    private void Update()
    {
        for(int i = 0; i < SliderText.Length; i++)
        {
            SliderText[i].text = OptionButtons[i+1].GetComponent<Slider>().value.ToString();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
