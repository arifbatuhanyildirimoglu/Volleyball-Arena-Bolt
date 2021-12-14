using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject gameOverPanel;
    public Text winnerText;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnExitButtonClicked()
    {
        
        GameManager.Instance.ExitGame();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
