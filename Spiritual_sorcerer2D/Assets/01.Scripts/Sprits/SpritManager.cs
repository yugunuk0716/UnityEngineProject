using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;

public class SpritManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static SpritManager instance;
    public static SpritManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpritManager>();
                if (instance == null)
                {
                    GameObject temp = new GameObject("SpiritManager");
                    instance = temp.AddComponent<SpritManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    public float asd;
    public List<GameObject> sprits = new List<GameObject>();
    public List<GameObject> spritImgs = new List<GameObject>();
    public List<AudioClip> audioClips = new List<AudioClip>();

    public AudioSource source;
    public Button buySprit;
    
    public int nowCoworker = 0;

    public BigInteger lvUpCost;
    public BigInteger fireDamage;
    public BigInteger waterDamage;
    public BigInteger windDamage;
    public BigInteger groundDamage;
    public BigInteger forestDamage;
    public BigInteger blizzardDamage;
    public BigInteger darknessDamage;
    public BigInteger lightDamage;
    BigInteger cost;

    private void Awake()
    {
        source = this.gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        if (nowCoworker > 7)
        {
            buySprit.interactable = false;
        }
        for (int i = 0; i < nowCoworker; i++)
        {
            sprits[i].SetActive(true);
        }
    }
    

    public void BuyCoworker()
    {
        GameManager.Instance.SetText();
        cost = (BigInteger)(500 * Mathf.Pow(4f, nowCoworker));
        if (GameManager.Instance.money >= cost && nowCoworker < 8)
        {
            sprits[nowCoworker].SetActive(true);
            GameManager.Instance.money -= cost;
            nowCoworker++;
            GameManager.Instance.SetText();
            for (int i = 0; i < nowCoworker; i++)
            {
                spritImgs[i].SetActive(true);
            }

        }
        else
        {
            UIManager.Instance.NoMoneyPanel();
        }
        if (nowCoworker > 7)
        {
            buySprit.interactable = false;
        }

    }
    public void LevelUpCoworker(int coworkerIndex)
    {

        Sprit co = sprits[coworkerIndex].GetComponent<Sprit>();
        co.lv++;
        
    }

}
