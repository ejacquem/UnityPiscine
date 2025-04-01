using TMPro.EditorUtilities;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public enum GameAction {Start, Win, Lose}
    public GameAction selectedAction;

    public UIManager ui;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        Debug.Log("triggered");
        if (other.CompareTag("Player"))
        {
            if (selectedAction == GameAction.Start)
                ui.StartTimer();
            if (selectedAction == GameAction.Win)
                ui.GameWon();
            if (selectedAction == GameAction.Lose)
                ui.GameOver();
        }
    }
}
