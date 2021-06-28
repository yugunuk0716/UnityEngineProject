using UnityEngine;

[CreateAssetMenu(fileName ="Card",menuName ="AfterSchool/CardGame/Card")]
public class Card : ScriptableObject
{
    public string id;
    public string tagString;

    public bool usable;
    public bool disposable;

    public CardPower power;

    public void Init(string id, string tagString, CardPower defaultCP, bool dispose = false, bool usable = true) 
    {
        this.id = id;
        this.tagString = tagString;
        this.disposable = dispose;

        power = defaultCP;// 카드 파워가 바뀔 가능성을 고려하여 디폴트를 넣는 거임
    }

    public Card Clone(bool setDispose = false) 
    {
        var card = CreateInstance<Card>();
        bool dispose = setDispose || this.disposable;
        card.Init(id, tagString, power, dispose);
        return card;
    }
    public void OnUse() 
    {
        Debug.Log("Card Use : " + power.cardName);

        switch (power.cardName)
        {
            case "Flame":
                if (SpritManager.Instance.nowCoworker > 0)
                {
                    GameManager.Instance.flameLv++;
                    DeckManager.Instance.flameCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else 
                {
                    UIManager.Instance.NullSprit();
                }
                    break;
            case "Water":
                if (SpritManager.Instance.nowCoworker > 1)
                {
                    GameManager.Instance.waterLv++;
                    DeckManager.Instance.waterCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;
            case "Wind":
                if (SpritManager.Instance.nowCoworker > 2)
                {
                    GameManager.Instance.windLv++;
                    DeckManager.Instance.windCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;
            case "Ground":
                if (SpritManager.Instance.nowCoworker > 3)
                {
                    GameManager.Instance.groundLv++;
                    DeckManager.Instance.groundCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();

                }
                break;
            case "Forest":
                if (SpritManager.Instance.nowCoworker > 4)
                {
                    GameManager.Instance.forestLv++;
                    DeckManager.Instance.forestCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;
            case "Blizzard":
                if (SpritManager.Instance.nowCoworker > 5)
                {
                    GameManager.Instance.blizzardLv++;
                    DeckManager.Instance.blizzardCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;
            case "Darkness":
                if (SpritManager.Instance.nowCoworker > 6)
                {
                    GameManager.Instance.darknessLv++;
                    DeckManager.Instance.darknessCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;
            case "Light":
                if (SpritManager.Instance.nowCoworker > 7)
                {
                    GameManager.Instance.lightLv++;
                    DeckManager.Instance.lightCardCount--;
                    DeckManager.Instance.destroyCard.Invoke();
                }
                else
                {
                    UIManager.Instance.NullSprit();
                }
                break;

            default:
                break;
        }
        for (int i = 0; i < SpritManager.Instance.nowCoworker; i++)
        {
            SpritManager.Instance.sprits[i].GetComponent<Sprit>().SetLv();
        }
    }

    
    public void OnDraw() 
    {
        Debug.Log("Card Draw : " + power.cardName);
        switch (power.cardName)
        {
            case "Flame":
                DeckManager.Instance.flameCardCount++;
                break;
            case "Water":
                DeckManager.Instance.waterCardCount++;
                break;
            case "Wind":
                DeckManager.Instance.windCardCount++;
                break;
            case "Ground":
                DeckManager.Instance.groundCardCount++;
                break;
            case "Forest":
                DeckManager.Instance.forestCardCount++;
                break;
            case "Blizzard":
                DeckManager.Instance.blizzardCardCount++;
                break;
            case "Darkness":
                DeckManager.Instance.darknessCardCount++;
                break;
            case "Light":
                DeckManager.Instance.lightCardCount++;
                break;
            default:
                break;
        }
    }
    public void OnDrop()
    {
        Debug.Log("Card Drop : " + power.cardName);
    }
    public void TurnEnd() 
    {
        Debug.Log("TurnEnd : " + power.cardName);
    }

}
