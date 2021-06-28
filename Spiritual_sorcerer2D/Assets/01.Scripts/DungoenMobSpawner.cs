using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DungoenMobSpawner : MonoBehaviour
{
    public int dungoenEnemyCount = 0;
    public int dungoenEnemyMax = 10;
    public Text dungoenTxt;
    private WaitForSeconds ws = new WaitForSeconds(1.5f);

    void Start()
    {
        dungoenTxt.text = string.Format("진행 상황: {0}/{1}", dungoenEnemyCount, dungoenEnemyMax);
        for (int i = 0; i < SpritManager.Instance.sprits.Count; i++)
        {
            SpritManager.Instance.sprits[i].GetComponent<Sprit>().ds = this;
        }
        StartCoroutine(EnemyInstantiate());
    }


    public IEnumerator EnemyInstantiate()
    {
        GameObject obj = null;
        while (true)
        {

            if (!GameManager.Instance.isSpawned)
            {
                yield return ws;

                GameManager.Instance.isSpawned = true;
                dungoenEnemyCount++;
                dungoenTxt.text = string.Format("진행 상황: {0}/{1}",dungoenEnemyCount,dungoenEnemyMax);
                obj = PoolManager.Instance.GetDungoenEnemy();
                print("적생성");
                obj.transform.position = transform.position;
            }
            if (dungoenEnemyCount > dungoenEnemyMax)
            {
               
                DeckManager.Instance.Draw();
                
                UIManager.Instance.LoadScene("InGame");
                if (GameManager.Instance.isSpawned)
                {
                    GameManager.Instance.dEnemies[0].KillEnemy();
                }
                GameManager.Instance.isDungoen = false;
                UIManager.Instance.dungoenBtn.interactable = true;

                
            }

            yield return null;
        }
    }

    public void DungoenQuit() 
    {
        UIManager.Instance.LoadScene("InGame");
        GameManager.Instance.isDungoen = false;
        UIManager.Instance.dungoenBtn.interactable = true;
    }
}
