using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardHandler : MonoBehaviour
{
    public Text cardName;
    public Text cardDesc;

    public Image img;


    Card card;
    public void Init(Card drawCard)
    {
        card = drawCard;
        if(drawCard.id != null&& drawCard.tagString != null&& drawCard.power != null)
        {
            drawCard.Init(drawCard.id, drawCard.tagString, drawCard.power, drawCard.disposable, drawCard.usable);   
        }
        cardName.text = string.Format("{0}", drawCard.power.name);
        cardDesc.text = string.Format("{0}", drawCard.power.cardDescription);
        img.sprite = drawCard.power.illust;
    }
    public void OnUseCard() 
    {
        DeckManager.Instance.destroyCard.AddListener(() => { DestroyCard(); });
        card.OnUse();
    }

    public void DestroyCard() 
    {
        Destroy(this.gameObject);
    }
}
