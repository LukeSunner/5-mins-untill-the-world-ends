using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public static int scoreAmount;
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreAmount;
    }
}
