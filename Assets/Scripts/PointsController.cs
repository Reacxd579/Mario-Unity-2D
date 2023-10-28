using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsController : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        UpdatePointsText(); // Llama a la función para asegurarte de que el TextMeshProUGUI esté actualizado al inicio.
    }

    // Llamado cada vez que el valor de coins cambia.
    public void UpdatePointsText()
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = GameManager.Score.ToString();
        }
    }
}
