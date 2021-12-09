using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public Player player;

    public float playTime;
    public bool isStart;

    public GameObject[] prefabs;
    private List<GameObject> gameObject = new List<GameObject>();



    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject overPanel;
    public GameObject rulePanel;
    
    public Text maxScoreTxt;
    public Text scoreTxt;
    public Text playTimeTxt;
    public Text playerHeartTxt;
    public Text playerCoinTxt;
    public Text curScoreTxt;
    public Text bestText;

    public bool over;
    
    
    void Awake(){
        PlayerPrefs.SetInt("MaxScore", 0);
        maxScoreTxt.text = string.Format("{0:n0}",PlayerPrefs.GetInt("MaxScore"));
    }

    public void GameRule(){
        menuPanel.SetActive(false);
        rulePanel.SetActive(true);
    }

    public void GameStart(){
        isStart=true;
        rulePanel.SetActive(false);
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        player.gameObject.SetActive(true);

        player.score = 0;

        InvokeRepeating("Spawn",1,0.3f);
    }

    public void GameOver(){
        CancelInvoke();
        over = true;
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        curScoreTxt.text = scoreTxt.text;

        int maxScore = PlayerPrefs.GetInt("MaxScore");
        if(player.score > maxScore) {
            bestText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", player.score);
        }

    }

    public void Restart(){
        over = false;
        SceneManager.LoadScene(0);
    }

    void Update(){
        if(isStart) playTime += Time.deltaTime;
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void LateUpdate(){
        if(isStart){

        scoreTxt.text = string.Format("{0:n0}", player.score);

        int min = (int)(playTime/60);
        int second = (int)(playTime % 60);
        playTimeTxt.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
        playerHeartTxt.text = player.heart + " / " + player.maxHeart;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = new Vector3(70,100,70);
        
        float posX = basePosition.x + Random.Range(-size.x/2f, size.x/2f);
        float posY = basePosition.y + Random.Range(-size.y/2f, size.y/2f) + 30;
        float posZ = basePosition.z + Random.Range(-size.z/2f, size.z/2f);
        
        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        
        return spawnPos;
    }

    private void Spawn()
    {
        int selection = Random.Range(0, prefabs.Length);
        
        GameObject selectedPrefab = prefabs[selection];
        
        Vector3 spawnPos = GetRandomPosition();
        
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        gameObject.Add(instance);
        if(!over) player.score ++;
    }

    
    
}
