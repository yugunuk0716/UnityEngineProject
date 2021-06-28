using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PoolManager>();

                if (instance == null)
                {
                    GameObject temp = new GameObject("PoolManager");
                    instance = temp.AddComponent<PoolManager>();
                }
            }

            return instance;
        }
    }


    public Dictionary<string, GameObject> bulletType = new Dictionary<string, GameObject>();
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> bossQueue = new Queue<GameObject>();
    private Queue<GameObject> dungoenQueue = new Queue<GameObject>();


    private int bulletCount = 100;
    private int enemyCount = 10;
    private int bossCount = 5;
    private int dungoenEnemyCount = 10;
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private GameObject dungoenEnemy = null;
    [SerializeField]
    private GameObject bossPrefab = null;
    [SerializeField]
    private GameObject enemyPrefab = null;
    [SerializeField]
    private GameObject fireWallPrefab = null;
    [SerializeField]
    private GameObject waterBallPrefab = null;
    [SerializeField]
    private GameObject tornadoPrefab = null;
    [SerializeField]
    private GameObject groundPunchPrefab = null;
    [SerializeField]
    private GameObject pollenPrefab = null;
    [SerializeField]
    private GameObject blizzardTempestPrefab = null;
    [SerializeField]
    private GameObject darknessKnifePrefab = null;
    [SerializeField]
    private GameObject lightSimbolPrefab = null;


    private void Start()
    {
        string bulletName;

        bulletName = "Bullet";
        bulletType.Add(bulletName, bulletPrefab);
        bulletName = "flame";
        bulletType.Add(bulletName, fireWallPrefab);
        bulletName = "water";
        bulletType.Add(bulletName, waterBallPrefab);
        bulletName = "wind";
        bulletType.Add(bulletName, tornadoPrefab);
        bulletName = "ground";
        bulletType.Add(bulletName, groundPunchPrefab);
        bulletName = "forest";
        bulletType.Add(bulletName, pollenPrefab);
        bulletName = "blizzard";
        bulletType.Add(bulletName, blizzardTempestPrefab);
        bulletName = "darkness";
        bulletType.Add(bulletName, darknessKnifePrefab);
        bulletName = "light";
        bulletType.Add(bulletName, lightSimbolPrefab);

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            bulletQueue.Enqueue(obj);
            
        }
        for (int i = 0; i < dungoenEnemyCount; i++)
        {
            GameObject obj = Instantiate(dungoenEnemy, transform);
            obj.SetActive(false);
            dungoenQueue.Enqueue(obj);
        }
        for (int i = 0; i < bossCount; i++)
        {
            GameObject obj = Instantiate(bossPrefab, transform);
            obj.SetActive(false);
            bossQueue.Enqueue(obj);
            
        }
        print(bulletQueue.Count); 
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, transform);
            obj.SetActive(false);
            enemyQueue.Enqueue(obj);
        }
    }

    public GameObject GetBullet()
    {
        GameObject obj = bulletQueue.Peek();
        if (obj != null) 
        {
            obj = Instantiate(bulletPrefab, transform);
        }
        else
        {
            obj = bulletQueue.Dequeue();
        }
        obj.SetActive(true);
        bulletQueue.Enqueue(obj);

        return obj;
    }
    public GameObject GetDungoenEnemy()
    {
        GameObject obj = dungoenQueue.Peek();
        if (obj != null)
        {
            obj = Instantiate(dungoenEnemy, transform);
        }
        else
        {
            obj = dungoenQueue.Dequeue();
        }
        obj.SetActive(true);
        dungoenQueue.Enqueue(obj);

        return obj;
    }
    public GameObject GetBoss()
    {
        GameObject obj = bossQueue.Peek();
        if (obj != null)
        {
            obj = Instantiate(bossPrefab, transform);
        }
        else
        {
            obj = bossQueue.Dequeue();
        }
        obj.SetActive(true);
        bossQueue.Enqueue(obj);

        return obj;
    }

    public GameObject GetEnemy() 
    {
        GameObject obj = enemyQueue.Peek();

        if (obj != null) 
        {
            obj = Instantiate(enemyPrefab, transform);
        }
        else
        {
            obj = enemyQueue.Dequeue();
        }

        obj.SetActive(true);
        enemyQueue.Enqueue(obj);

        return obj;
    }



}
