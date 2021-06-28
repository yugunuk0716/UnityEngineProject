using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 싱글톤
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject temp = new GameObject("UIManager");
                    instance = temp.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    #region 변수
    #region Buttons
    public Button onClickAttak;
    public Button damageUp;
    public Button noMoneyPanel;
    public Button spritShop;
    public Button sorcererShop;
    public Button fullHandPanel;
    public Button cardInventory;
    public Button dungoenBtn;
    public Button dungoenListBtn;
    #endregion

    #region GameObjects
    public GameObject cardContents;
    public GameObject socerer;
    public GameObject sprit;
    public GameObject dungoen;
    public GameObject card;
    public GameObject fire;
    public GameObject menu;
    public GameObject nullSpirit;
    public GameObject master;
    private GameObject bullet;
    #endregion

    private Animator playerAnim;

    public List<Text> skillCostTxt = new List<Text>();
    public int tap2Atk = 1;
    public bool isAttacked = false;

    public AudioClip attack;

    private AudioSource audioSource;

    public float exitTime = 1.1f;
    public float clickCoolTime = 0.5f;

    #region WaitForSec
    private float firstCoolTime = 10f;
    private float secondCoolTime = 10f;
    private float thirdCoolTime = 10f;
    private float fourthCoolTime = 10f;
    private float secondOnTime = 10f;
    private float thirdOnTime = 10f;
    private float fourthOnTime = 10f;
    private float animDelay = 0.5f;
    private WaitForSeconds fCoolTime;
    private WaitForSeconds sCoolTime;
    private WaitForSeconds tCoolTime;
    private WaitForSeconds foCoolTime;
    private WaitForSeconds sOnTime;
    private WaitForSeconds tOnTime;
    private WaitForSeconds foOnTime;
    private WaitForSeconds waitAnimDelay;
    #endregion

    #endregion

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        SetFunc();
        SetWait();
        StartCoroutine(PlayerAnim());
     
    }

    void Update() 
    {
        clickCoolTime += Time.deltaTime;
    }


    public void SetFunc() 
    {
        
        onClickAttak.onClick.AddListener(() => { OnClickAttackBtn(); });
        damageUp.onClick.AddListener(() => { OnClickAttakLevelBtn(); });
        noMoneyPanel.onClick.AddListener(() => { Close(); });
        fullHandPanel.onClick.AddListener(() => { Close(); });
        sorcererShop.onClick.AddListener(() => { OnClickSorcererShop(); });
        spritShop.onClick.AddListener(() => { OnClickSpritShop(); });
        cardInventory.onClick.AddListener(() => { OnClickCardIventory(); });
        dungoenBtn.onClick.AddListener(() => { OnClickDungeon(); });
        dungoenListBtn.onClick.AddListener(() => { OnClickDungoenList(); });
    }

    public void LoadScene(string sName) 
    {
        SceneController.LoadScene(sName);
    }
   


    private void SetWait() 
    {
        fCoolTime= new WaitForSeconds(firstCoolTime);
        sCoolTime= new WaitForSeconds(secondCoolTime);
        tCoolTime= new WaitForSeconds(thirdCoolTime);
        foCoolTime = new WaitForSeconds(fourthCoolTime);
        sOnTime = new WaitForSeconds(secondOnTime);
        tOnTime = new WaitForSeconds(thirdOnTime);
        foOnTime = new WaitForSeconds(fourthOnTime);
        waitAnimDelay = new WaitForSeconds(animDelay);
    } //WaitForSeconds 초기화
    public void CheckSkill() // Json으로 데이터를 긁어왔을 때 스킬 레벨 확인
    {
       
        if (GameManager.Instance.thirdSkillLv > 1)
        {
            StartCoroutine(AutoSkill3());
        }
        if (GameManager.Instance.fourthSkillLv > 1)
        {
            StartCoroutine(AutoSkill4());
        }
        if (GameManager.Instance.firstSkillLv > 1)
        {
            StartCoroutine(AutoSkill1());
        }
       
    }

    

    public void OnClickAttakLevelBtn()
    {
        if (GameManager.Instance.lvUpCost < GameManager.Instance.money)
        {
            GameManager.Instance.money -= GameManager.Instance.lvUpCost;
            GameManager.Instance.clickLevel++;
            GameManager.Instance.SetValue();
            GameManager.Instance.SetText();

        }
        else
        {
            NoMoneyPanel();
        }
    } //power 증가
    
    public void NoMoneyPanel()
    {
        noMoneyPanel.gameObject.SetActive(true);
    }
    public void OnClickMenu() 
    {
        menu.SetActive(true);
    }
    public void Close() 
    {
        menu.SetActive(false);
        noMoneyPanel.gameObject.SetActive(false);
        fullHandPanel.gameObject.SetActive(false);
    }
    public void FirstSkillLvUp() 
    {
        if (GameManager.Instance.SetSkillCost(1,GameManager.Instance.firstSkillLv) < GameManager.Instance.money) 
        {
            
            if (GameManager.Instance.firstSkillLv == 1)
                StartCoroutine(AutoSkill1());
            if (GameManager.Instance.firstSkillLv / 10 == 10 && firstCoolTime > 5)
            {
                firstCoolTime--;
            }

            GameManager.Instance.money -= GameManager.Instance.SetSkillCost(1,GameManager.Instance.firstSkillLv);
            GameManager.Instance.firstSkillLv++;
            GameManager.Instance.SetValue();
            GameManager.Instance.SetText();
        }
        else
        {
            NoMoneyPanel();
        }


    }
    public void SecondSkillLvUp()
    {
        
    }
    public void ThirdSkillLvUp()
    {

        if (GameManager.Instance.SetSkillCost(3,GameManager.Instance.thirdSkillLv) < GameManager.Instance.money)
        {
            if (GameManager.Instance.thirdSkillLv == 1)
                StartCoroutine(AutoSkill3());
            if (GameManager.Instance.thirdSkillLv / 10 == 10 && thirdCoolTime > 5)
            {
                thirdCoolTime--;
            }

            GameManager.Instance.money -= GameManager.Instance.SetSkillCost(3,GameManager.Instance.thirdSkillLv);
            GameManager.Instance.thirdSkillLv++;
            GameManager.Instance.SetValue();
            GameManager.Instance.SetText();
        }
        else
        {
            NoMoneyPanel();
        }
    }
    public void FourthSkillLvUp()
    {
        if (GameManager.Instance.SetSkillCost(4,GameManager.Instance.fourthSkillLv) < GameManager.Instance.money)
        {
            if (GameManager.Instance.fourthSkillLv == 1)
                StartCoroutine(AutoSkill4());
            if (GameManager.Instance.fourthSkillLv / 10 == 10 && fourthCoolTime > 5)
            {
                thirdCoolTime--;
            }

            GameManager.Instance.money -= GameManager.Instance.SetSkillCost(4,GameManager.Instance.fourthSkillLv);
            GameManager.Instance.fourthSkillLv++;
            GameManager.Instance.SetValue();
            GameManager.Instance.SetText();
        }
        else
        {
            NoMoneyPanel();
        }
    }
    IEnumerator PlayerAnim() 
    {
        while (true)
        {
            if (master == null)
                master = GameObject.Find("Player");
            if (master != null&& SceneController.isLoaded) 
            {
                playerAnim = master.GetComponent<Animator>();
                if (isAttacked)
                {

                    playerAnim.Play("PlayerAttack");

                    while (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime)
                    {
                        if (playerAnim == null)
                            yield break;
                        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                        {
                            isAttacked = false;
                            yield return waitAnimDelay;
                            if (playerAnim != null)
                                playerAnim.Play("PlayerIdle");
                        }
                        yield return null;

                    }


                }
            }
            yield return null;
            
        }
    }
    private void OnClickAttackBtn()
    {
        if (clickCoolTime > 1f) 
        {
            audioSource.clip = attack;
            clickCoolTime = 0;
            if (!isAttacked)
                isAttacked = true;
            for (int i = 0; i < tap2Atk; i++)
            {
                GameManager.Instance.Hit(GameManager.Instance.attackDamage);
            }
            audioSource.Play();

        }

    }
    #region 스킬
    IEnumerator AutoSkill1()
    {
        while (true)
        {
            yield return fCoolTime;
            if (SceneController.isLoaded) 
            {
                for (int i = 0; i < GameManager.Instance.firstSkillLv; i++)
                {
                    GameObject obj = PoolManager.Instance.GetBullet();
                    obj.transform.position = transform.position;
                }
            }
            

        }
    }
    
    IEnumerator AutoSkill3()
    {
        while (true)
        {
            if (SceneController.isLoaded)
            {
                GameManager.Instance.attackDamage *= 2;
                yield return tOnTime;
                GameManager.Instance.SetValue();
                yield return tCoolTime;
            }
            yield return null;
        }
    }
    IEnumerator AutoSkill4()
    {
        while (true)
        {
            if (SceneController.isLoaded)
            {
                tap2Atk += GameManager.Instance.fourthSkillLv;
                yield return foOnTime;
                tap2Atk = 1;
                yield return foCoolTime;
            }
            yield return null;
        }
    }
    #endregion

    private void OnClickSorcererShop()
    {
        dungoen.SetActive(false);
        card.SetActive(false);
        sprit.SetActive(false);
        socerer.SetActive(true);
        for (int i = 0; i < SpritManager.Instance.nowCoworker; i++)
        {
            SpritManager.Instance.spritImgs[i].SetActive(false);
        }
    }

    private void OnClickSpritShop()
    {
        dungoen.SetActive(false);
        card.SetActive(false);
        socerer.SetActive(false);
        sprit.SetActive(true);
        for (int i = 0; i < SpritManager.Instance.nowCoworker; i++)
        {
            SpritManager.Instance.spritImgs[i].SetActive(true);
        }
    }
    private void OnClickDungoenList()
    {
        dungoen.SetActive(true);
        card.SetActive(false);
        sprit.SetActive(false);
        socerer.SetActive(false);
        for (int i = 0; i < SpritManager.Instance.nowCoworker; i++)
        {
            SpritManager.Instance.spritImgs[i].SetActive(false);
        }
    }
    private void OnClickCardIventory()
    {
       
        dungoen.SetActive(false);
        card.SetActive(true);
        sprit.SetActive(false);
        socerer.SetActive(false);
        for (int i = 0; i < SpritManager.Instance.nowCoworker; i++)
        {
            SpritManager.Instance.spritImgs[i].SetActive(false);
        }
    }

    public void OnClickDungeon() 
    {
        if (cardContents.transform.childCount < 24)
        {
            GameManager.Instance.isDungoen = true;

            if (GameManager.Instance.isSpawned) 
            {
                GameManager.Instance.enemies[0].KillEnemy();
            }
            LoadScene("Dungoen");
            if (GameManager.Instance.isDungoen)
            {
                dungoenBtn.interactable = false;
            }
        }
        else
        {
            fullHandPanel.gameObject.SetActive(true);
        }
    }

    public void NullSprit() 
    {
        nullSpirit.SetActive(true);
    }
    public void CloseNullSprit()
    {
        nullSpirit.SetActive(false);
    }
}
