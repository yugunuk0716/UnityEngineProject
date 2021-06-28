using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject temp = new GameObject("GameManager");
                    instance = temp.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    #region 변수

    public bool isDungoen = false;
    public bool isSpawned = false;
    public bool isBossSpawned = false;

    public int enemyCount;

    #region integer
    public BigInteger money;
    public int stageLevel = 1;
    public int clickLevel;
    public BigInteger attackDamage;
    public BigInteger lvUpCost;
    public BigInteger skillLvUpCost;
    public int count;
    public int firstSkillLv = 0;
    public int secondSkillLv = 0;
    public int thirdSkillLv = 0;
    public int fourthSkillLv = 0;
    public int flameLv = 1;
    public int waterLv = 1;
    public int windLv = 1;
    public int groundLv = 1;
    public int forestLv = 1;
    public int blizzardLv = 1;
    public int darknessLv = 1;
    public int lightLv = 1;
    #endregion

    public Deck deck;
    public List<Card> cards = new List<Card>();
    
    #region Texts
    public Text firstSkill;
    //public Text secondSkill;
    public Text thirdSkill;
    public Text fourthSkill;
    public Text moneyTxt;
    public Text stageTxt;
    public Text lvUpCostTxt;
    public Text spiritCostTxt;
    #endregion


    public UnityEvent destroyAnim;
    public WaitForSeconds attackDelay;
  
   
    public List<Enemy> enemies = new List<Enemy>();
    public List<Boss> bosses = new List<Boss>();
    public List<DungoenEnemy> dEnemies = new List<DungoenEnemy>();
    public PlayerData data;

    private string filePath = "";
    private string jsonString = "";
    #endregion



    void Awake()
    {
        data = new PlayerData();
        LoadStat();
        SetValue();

        SetText();


    }
   

 
    public void SetValue()
    {
        lvUpCost = (BigInteger)(50 * Mathf.Pow(1.07f, clickLevel - 1));

        attackDamage = lvUpCost * 1 / 5;
    }
    public BigInteger SetSkillCost(int index ,int level) 
    {
        skillLvUpCost = (BigInteger)(500 * Mathf.Pow(1.09f, level*index));
        return skillLvUpCost;
    }

   
    public void SetText()
    {
        moneyTxt.text = string.Format("돈: {0}", GetMoney(money));
        lvUpCostTxt.text = string.Format("{0}", GetMoney(lvUpCost));


        if (SpritManager.Instance.nowCoworker > 7) 
        {
            spiritCostTxt.gameObject.SetActive(false);
        }
        else
        {
            spiritCostTxt.text = string.Format("가격: {0}", (BigInteger)(500 * Mathf.Pow(4f, SpritManager.Instance.nowCoworker)));
        }
        firstSkill.text = string.Format("{0}", GetMoney(SetSkillCost(1,firstSkillLv)));
        thirdSkill.text = string.Format("{0}", GetMoney(SetSkillCost(3,thirdSkillLv)));
        fourthSkill.text = string.Format("{0}", GetMoney(SetSkillCost(4,fourthSkillLv)));
        SetStageText();
    }

    string GetMoney(BigInteger _gold)
    {
        string gText = string.Empty;
        gText = _gold.ToString();
        switch (gText.Length)
        {
            case 1:
            case 2:
            case 3:
                break;
            case 4:
            case 5:
            case 6:
                gText = gText.Remove(gText.Length - 3, 3);
                gText += "K";
                break;
            case 7:
            case 8:
            case 9:
                gText = gText.Remove(gText.Length - 6, 6);
                gText += "M";
                break;
            case 10:
            case 11:
            case 12:
                gText = gText.Remove(gText.Length - 9, 9);
                gText += "B";
                break;
            case 13:
            case 14:
            case 15:
                gText = gText.Remove(gText.Length - 12, 12);
                gText += "T";
                break;
            default:
                gText = string.Format("{0}.{1}{2}E+{3}", gText[0], gText[1], gText[2], gText[3], gText.Length - 1);
                break;
        }
        return gText;
    }

    void SetStageText() 
    {
        if (stageLevel % 10 != 0)
        {
            if (stageLevel < 10)
            {
                stageTxt.text = string.Format("1-{0}",   (stageLevel % 10).ToString());
            }
            else
            {
                stageTxt.text = string.Format("{0}-{1}",((stageLevel / 10) + 1).ToString() , (stageLevel % 10).ToString());
            }
        }
        else
        {
            if (stageLevel < 11)
            {
                stageTxt.text = string.Format("1-10");
            }
            else
            {
                stageTxt.text = string.Format("{0}-10",(stageLevel / 10).ToString());
            }
        }
    }

    void SaveStat()
    {

        data.clickLevel = clickLevel;
        data.stageLevel = stageLevel;
        data.money = money.ToString();
        data.firstSkillLv = firstSkillLv;
        data.secondSkillLv = secondSkillLv;
        data.thirdSkillLv = thirdSkillLv;
        data.fourthSkillLv = fourthSkillLv;
        data.spritCount = SpritManager.Instance.nowCoworker;
        data.flameLv = flameLv;
        data.waterLv = waterLv;
        data.windLv = windLv;
        data.groundLv = groundLv;
        data.forestLv = forestLv;
        data.blizzardLv = blizzardLv;
        data.darknessLv = darknessLv;
        data.lightLv = lightLv;
        data.flameCardCount = DeckManager.Instance.flameCardCount;
        data.waterCardCount = DeckManager.Instance.waterCardCount;
        data.windCardCount = DeckManager.Instance.windCardCount;
        data.groundCardCount = DeckManager.Instance.groundCardCount;
        data.forestCardCount = DeckManager.Instance.forestCardCount;
        data.blizzardCardCount = DeckManager.Instance.blizzardCardCount;
        data.darknessCardCount = DeckManager.Instance.darknessCardCount;
        data.lightCardCount = DeckManager.Instance.lightCardCount;
        data.totalCard = DeckManager.Instance.flameCardCount + DeckManager.Instance.waterCardCount + DeckManager.Instance.windCardCount + DeckManager.Instance.groundCardCount + DeckManager.Instance.forestCardCount + DeckManager.Instance.blizzardCardCount + DeckManager.Instance.darknessCardCount + DeckManager.Instance.lightCardCount;
        for (int i = 0; i < DeckManager.Instance.flameCardCount; i++)
        {
            deck.Put(cards[0]);
        }
        for (int i = 0; i < DeckManager.Instance.waterCardCount; i++)
        {
            deck.Put(cards[1]);
        }
        for (int i = 0; i < DeckManager.Instance.windCardCount; i++)
        {
            deck.Put(cards[2]);
        }
        for (int i = 0; i < DeckManager.Instance.groundCardCount; i++)
        {
            deck.Put(cards[3]);
        }
        for (int i = 0; i < DeckManager.Instance.forestCardCount; i++)
        {
            deck.Put(cards[4]);
        }
        for (int i = 0; i < DeckManager.Instance.blizzardCardCount; i++)
        {
            deck.Put(cards[5]);
        }
        for (int i = 0; i < DeckManager.Instance.darknessCardCount; i++)
        {
            deck.Put(cards[6]);
        }
        for (int i = 0; i < DeckManager.Instance.lightCardCount; i++)
        {
            deck.Put(cards[7]);
        }

        string str = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", str);
    }
    
    void LoadStat()
    {
        if (!File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            data.clickLevel = 1;
            data.stageLevel = 1;
            data.money = 0.ToString();
            File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", JsonUtility.ToJson(data));
            data.GetData();
            
        }
        else
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData data2 = JsonUtility.FromJson<PlayerData>(jsonData);
            data2.GetData();
            for (int i = 0; i < data2.totalCard; i++)
            {
                Card card = deck.Draw();
                if(card != null)
                    DeckManager.Instance.InstantiateCardObject(card);
            }
        }
    }
    public void Kill() 
    {
        SetText();
        

        if (count >= 10)
        {
            stageLevel++;
            count = 0;
           
        }
        else
        {
            Instance.count++;
        }
    }
    private void OnApplicationQuit()
    {
        SaveStat();
    }



    public void Hit(BigInteger _damage)
    {
        if (!isDungoen)
        {
            if (isBossSpawned)
            {
                if (bosses.Count != 0)
                {
                    if (bosses[0].curEnemyHp > 0)
                    {
                        bosses[0].curEnemyHp -= _damage;
                        if (bosses[0].slider != null)
                            bosses[0].slider.value = (float)(bosses[0].curEnemyHp * 100 / bosses[0].maxEnemyHp) / 100f;
                    }
                    else
                    {

                        bosses[0].KillEnemy();
                    }
                }
            }
            else
            {
                if (enemies.Count != 0)
                {
                    if (enemies[0].curEnemyHp > 0)
                    {
                        enemies[0].curEnemyHp -= _damage;
                        if (enemies[0].slider != null)
                            enemies[0].slider.value = (float)(enemies[0].curEnemyHp * 100 / enemies[0].maxEnemyHp) / 100f;
                    }
                    else
                    {

                        enemies[0].KillEnemy();
                    }
                }
            }
        }
        else
        {

            if (dEnemies.Count != 0)
            {
                if (dEnemies[0].curEnemyHp > 0)
                {
                    dEnemies[0].curEnemyHp -= _damage;
                    if (dEnemies[0].slider != null)
                        dEnemies[0].slider.value = (float)(dEnemies[0].curEnemyHp * 100 / dEnemies[0].maxEnemyHp) / 100f;
                }
                else
                {

                    dEnemies[0].KillEnemy();
                }
            }
        }
       
       
    }

    #region 강화 재료 던전에 쓸 코드
    //     if (stageLevel == 10) 
    //            {
    //                File.Create(Application.persistentDataPath + "/이거 정령 강화 재료임 ㅋ.key");
    //            }
    //if (File.Exists(Application.persistentDataPath + "/이거 정령 강화 재료임 ㅋ.key"))
    //{
    //    이상한거.SetActive(true);
    //}
    #endregion

}
