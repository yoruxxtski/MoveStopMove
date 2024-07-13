using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowStats : MonoBehaviour
{
    [SerializeField] private GameObject levelImage;
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private TextMeshProUGUI arrowLevel;

    public GameObject GetLevelImage() {
        return levelImage;
    }
    public GameObject GetArrowImage() {
        return arrowImage;
    }

    public TextMeshProUGUI GetArrowLevel() {
        return arrowLevel;
    }
}
