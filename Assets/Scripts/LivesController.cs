using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LivesController : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        UpdateLivesText(); // Llama a la función para asegurarte de que el TextMeshProUGUI esté actualizado al inicio.
    }

    // Llamado cada vez que el valor de coins cambia.
    public void UpdateLivesText()
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = GameManager.Instance.lives.ToString();
        }
    }
}
