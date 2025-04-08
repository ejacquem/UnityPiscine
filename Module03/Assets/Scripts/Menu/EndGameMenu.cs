using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private GameObject nextLevelButton;
    [SerializeField]
    private GameObject replayButton;

    [SerializeField]
    private GameObject confetti;

    [SerializeField]
    private bool _isLastLevel = false;

    public void LevelCompleted(bool gameWon, int score, String rank, String title)
    {
        if (_isLastLevel)
        {
            GameCompleted(gameWon, score, rank, title);
            return;
        }
        endGameMenu.SetActive(true);
        if(gameWon)
        {
            nextLevelButton.SetActive(true);
            titleText.SetText("Level Completed !");
            descriptionText.SetText($"Congratulations on completing the level.\nYou're a true {title} !\n\nScore : {score}\nRank : {rank}");
        }
        else
        {
            replayButton.SetActive(true);
            titleText.SetText("You Lost :(");
            descriptionText.SetText($"The villagers appreciated your help though.\nThey said you're a true {title} !\n\nScore : {score}\nRank : {rank}");
        }
    }

    public void GameCompleted(bool gameWon, int score, String rank, String title)
    {
        endGameMenu.SetActive(true);
        if(gameWon)
        {
            confetti.SetActive(true);
            nextLevelButton.SetActive(true); // is the mainMenuButton now
            titleText.SetText("Game Won !");
            descriptionText.SetText($"Congratulations on completing the Game.\nYou're a true {title} !\n\nScore : {score}\nRank : {rank}");
        }
        else
        {
            confetti.SetActive(true);
            for (int i = 0; i < confetti.transform.childCount; i++)
            {
                var child = confetti.transform.GetChild(i);
                child.gameObject.SetActive(child.name == "Background");
            }
            replayButton.SetActive(true);
            titleText.SetText("You Lost :(");
            descriptionText.SetText($"The villagers appreciated your help though.\nThey said you're a true {title} !\n\nScore : {score}\nRank : {rank}");
        }
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}


