using Photon.Bolt;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    public Text masterScoreText;
    public Text clientScoreText;

    private PlayerScript masterPlayer;
    private PlayerScript clientPlayer;
    
    private int masterScore;
    private int clientScore;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {

        masterScore = 0;
        clientScore = 0;

    }
        



    public void AddScoreToMaster()
    {

        masterScore++;
        
        GameManager.Instance.SpawnCharacters();
        
        
    }

    public void AddScoreToClient()
    {

        clientScore++;
        
        GameManager.Instance.SpawnCharacters();
        
    }
    // Update is called once per frame
    void Update()
    {
        masterScoreText.text = masterScore.ToString();
        clientScoreText.text = clientScore.ToString();
    }
}
