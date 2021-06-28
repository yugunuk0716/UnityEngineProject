using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private WaitForSeconds ws = new WaitForSeconds(1.5f);
    private int mobCount = 0;


    public void Start()
    {
        for (int i = 0; i < SpritManager.Instance.sprits.Count; i++)
        {
            SpritManager.Instance.sprits[i].GetComponent<Sprit>().es = this;
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
                if (mobCount > 3)
                {
                    mobCount = 0;
                    GameManager.Instance.isSpawned = true;
                    GameManager.Instance.isBossSpawned = true;
                    obj = PoolManager.Instance.GetBoss();
                    obj.transform.position = transform.position;
                }
                else
                {
                    mobCount++;
                    GameManager.Instance.isSpawned = true;
                    obj = PoolManager.Instance.GetEnemy();
                    print("Àû»ý¼º");
                    obj.transform.position = transform.position;
                }

            }

            yield return null;
        }
    }
}
