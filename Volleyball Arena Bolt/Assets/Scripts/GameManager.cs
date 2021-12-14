using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    
    public List<GameObject> spawnPositions;
    public GameObject ballSpawnPosition;
    private GameObject ball;
    
    private GameObject masterGameObject;
    private GameObject clientGameObject;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        
        if (BoltNetwork.IsServer)
        {
            masterGameObject = BoltNetwork.Instantiate(BoltPrefabs.Player,
                new Vector3(spawnPositions[0].transform.position.x, -3f,
                    0), Quaternion.identity);
        }
        else
        {
            clientGameObject = BoltNetwork.Instantiate(BoltPrefabs.Player,
                new Vector3(spawnPositions[1].transform.position.x, -3f,
                    0), Quaternion.identity);
        }
        
        //topu tam ortadan yukarıdan ya sağa atacak ya da sola
        if (BoltNetwork.IsServer)
        {
            ball = BoltNetwork.Instantiate(BoltPrefabs.Ball, ballSpawnPosition.transform.position,
                Quaternion.identity);

            int randomNumber = Random.Range(0, 2);

            if (randomNumber == 0)
                ball.GetComponent<Rigidbody>().AddForce(Vector3.left * 2f, ForceMode.Impulse);
            else
                ball.GetComponent<Rigidbody>().AddForce(Vector3.right * 2f, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Convert.ToInt32(ScoreManager.Instance.clientScoreText.text) == 3 ||
            Convert.ToInt32(ScoreManager.Instance.masterScoreText.text) == 3)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        //TODO: It will stop the game and open a ui panel. In that panel there will be winner name and the score. Deal with it in the UIManager script.


        if (BoltNetwork.IsServer)
        {
            UIManager.Instance.gameOverPanel.SetActive(true);

            if (Convert.ToInt32(ScoreManager.Instance.masterScoreText.text) == 3)
            {
                UIManager.Instance.winnerText.text = "You won";
                UIManager.Instance.scoreText.text = ScoreManager.Instance.masterScoreText.text + " - " +
                                                    ScoreManager.Instance.clientScoreText.text;
            }
            else
            {
                UIManager.Instance.winnerText.text = "You lost";
                UIManager.Instance.scoreText.text = ScoreManager.Instance.masterScoreText.text + " - " +
                                                    ScoreManager.Instance.clientScoreText.text;
            }
        }
        else
        {
            UIManager.Instance.gameOverPanel.SetActive(true);

            if (Convert.ToInt32(ScoreManager.Instance.clientScoreText.text) == 3)
            {
                UIManager.Instance.winnerText.text = "You won";
                UIManager.Instance.scoreText.text = ScoreManager.Instance.masterScoreText.text + " - " +
                                                    ScoreManager.Instance.clientScoreText.text;
            }
            else
            {
                UIManager.Instance.winnerText.text = "You lost";
                UIManager.Instance.scoreText.text = ScoreManager.Instance.masterScoreText.text + " - " +
                                                    ScoreManager.Instance.clientScoreText.text;
            }
        }

        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        
        BoltNetwork.Shutdown();
        Application.Quit();

    }

    public void SpawnCharacters()
    {
        if (BoltNetwork.IsServer)
        {
            masterGameObject.transform.position = new Vector3(spawnPositions[0].transform.position.x, -3f,
                0);


            ball.transform.position = ballSpawnPosition.transform.position;

            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

            int randomNumber = Random.Range(0, 2);

            if (randomNumber == 0)
                ball.GetComponent<Rigidbody>().AddForce(Vector3.left * 2f, ForceMode.Impulse);
            else
                ball.GetComponent<Rigidbody>().AddForce(Vector3.right * 2f, ForceMode.Impulse);
        }
        else
        {
            clientGameObject.transform.position = new Vector3(spawnPositions[1].transform.position.x, -3f,
                0);
        }
    }
}
