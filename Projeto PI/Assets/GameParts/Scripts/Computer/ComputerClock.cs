using UnityEngine;
using TMPro;
using System;

public class ComputerClock : MonoBehaviour
{
    [SerializeField] private TMP_Text clockText;

    private void Update()
    {
        DateTime now = DateTime.Now;

        clockText.text = now.ToString("HH:mm");
    }
}