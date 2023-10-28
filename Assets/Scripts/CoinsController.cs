using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinsController : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        UpdateCoinsText(); // Llama a la función para asegurarte de que el TextMeshProUGUI esté actualizado al inicio.
    }

    // Llamado cada vez que el valor de coins cambia.
    public void UpdateCoinsText()
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = GameManager.Instance.coins.ToString();
        }
    }
}
