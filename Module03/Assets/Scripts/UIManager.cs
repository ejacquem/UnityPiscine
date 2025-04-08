using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI baseHPText;

    void Update()
    {
        SetEnergy(GameManager.Instance.GetEnergy());
        SetBaseHP(GameManager.Instance.GetBaseHP());
    }

    public void SetEnergy(float energy)
    {
        energyText.SetText($"Energy: {Mathf.FloorToInt(energy)}");
    }

    public void SetBaseHP(float hp)
    {
        baseHPText.SetText($"Base HP: {Mathf.FloorToInt(hp)}");
    }

    public void SetTitle(String text, Color textColor)
    {
        titleText.SetText(text);
        titleText.color = textColor;
    }
}
