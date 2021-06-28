using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;

public class DeckManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static DeckManager instance;
    public static DeckManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DeckManager>();
                if (instance == null)
                {
                    GameObject temp = new GameObject("GameManager");
                    instance = temp.AddComponent<DeckManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    public BigInteger hp;
    public BigInteger damage;
    public GameObject cardPrefab;


    public Transform tr;
    public Deck initialDeck;

    public UnityEvent destroyCard;

    public List<CardHandler> cardInHand;

    public int totalCard;
    public int flameCardCount;
    public int waterCardCount;
    public int windCardCount;
    public int groundCardCount;
    public int forestCardCount;
    public int blizzardCardCount;
    public int darknessCardCount;
    public int lightCardCount;


    private Deck playerDeck;

    public void Start()
    {
        //tr = GameObject.Find("CardContent").transform;
        if (playerDeck == null) 
        {
            playerDeck = initialDeck.Clone();
        }

    }

    public void Draw() 
    {

        Card drawCard = playerDeck.Draw();
        InstantiateCardObject(drawCard);

        for (int i = 0; i < 5f; i++)
        {
            playerDeck.Shuffle();
        }
    }

    public void InstantiateCardObject(Card cardData) 
    {
        var cardObject = Instantiate(cardPrefab, tr.transform);
        cardObject.GetComponent<CardHandler>().Init(cardData);
        cardInHand.Add(cardObject.GetComponent<CardHandler>());
        
    }
}
