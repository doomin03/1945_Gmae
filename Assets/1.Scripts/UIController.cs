using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreTxt;

    private void Update()
    {
        scoreTxt.text = $"Score:{GameController.Instance.score}";
    }
}
