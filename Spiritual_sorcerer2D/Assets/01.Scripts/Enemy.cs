using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class Enemy : MonoBehaviour
{
    public GameObject hpObj;

    public Slider slider;

    public BigInteger maxEnemyHp;
    public BigInteger curEnemyHp;
    
    void Start()
    {
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        slider = Instantiate(hpObj, canvas.transform).GetComponent<Slider>();
        SetHP();
        slider.gameObject.SetActive(true);
        slider.value = (float)(curEnemyHp * 100 / maxEnemyHp) / 100f;
    }
    private void OnEnable()
    {

        SetHP();
        if (slider != null) 
        {
            slider.gameObject.SetActive(true);
            slider.value = (float)(curEnemyHp * 100 / maxEnemyHp) / 100f;
        }
        GameManager.Instance.enemies.Add(this);
    }
    private void OnDisable()
    {
        if (slider != null) 
        {
            slider.gameObject.SetActive(false); 
        }
        GameManager.Instance.enemies.RemoveAt(0);

    }
    void SetHP() 
    {
        
        maxEnemyHp = (BigInteger)(Mathf.Pow(10f,1.1f) * (Mathf.Pow(1.06f, 10) - Mathf.Pow(1.06f, 10 + GameManager.Instance.stageLevel)) * 3 / (1 - 1.06f) * 2);
        curEnemyHp = maxEnemyHp;
       
    }
    public void KillEnemy() 
    {
        print("KillEnemy");
        if(slider != null && !slider.gameObject.activeSelf)
            slider.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        GameManager.Instance.money += maxEnemyHp / 3;
        GameManager.Instance.Kill();
        GameManager.Instance.isSpawned = false;
    }
    void Update()
    {
        if (SceneController.isLoaded)
        {
            if (slider != null)
            {
                slider.gameObject.SetActive(true);
                slider.gameObject.transform.position = Camera.main.WorldToScreenPoint(new UnityEngine.Vector3(1.5f, 0.7f, 0));
            }
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Bullet")
        {
           GameManager.Instance.Hit(GameManager.Instance.attackDamage);
         
        }


    }

    


}
