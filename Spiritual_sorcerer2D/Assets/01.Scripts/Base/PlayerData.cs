using System;
using System.Numerics;

[Serializable]
public class PlayerData 
{
    public string money;

    public int stageLevel;
    
    public int clickLevel;

    public int firstSkillLv; //일정 시간마다 기본공격 여러개 발사 스킬 레벨
    public int secondSkillLv; //일정시간 동안 정령 공격속도 증가 스킬 레벨
    public int thirdSkillLv; // 일정시간 동안 기본공격 데미지 상승
    public int fourthSkillLv; // 일정시간 동안 탭당 기본공격 횟수 추가

    public int flameLv;
    public int waterLv;
    public int windLv;
    public int groundLv;
    public int forestLv;
    public int blizzardLv;
    public int darknessLv;
    public int lightLv;

    public int totalCard;
    public int flameCardCount;
    public int waterCardCount;
    public int windCardCount;
    public int groundCardCount;
    public int forestCardCount;
    public int blizzardCardCount;
    public int darknessCardCount;
    public int lightCardCount;


    public int spritCount;
    public int level;
    public void GetData() 
    {
        GameManager.Instance.money = BigInteger.Parse(money);
        GameManager.Instance.stageLevel = stageLevel;
        GameManager.Instance.clickLevel = clickLevel;
        GameManager.Instance.firstSkillLv = firstSkillLv;
        GameManager.Instance.secondSkillLv = secondSkillLv;
        GameManager.Instance.thirdSkillLv = thirdSkillLv;
        GameManager.Instance.fourthSkillLv = fourthSkillLv;
        GameManager.Instance.flameLv = flameLv;
        GameManager.Instance.waterLv = waterLv;
        GameManager.Instance.windLv = windLv;
        GameManager.Instance.groundLv = groundLv;
        GameManager.Instance.forestLv = forestLv;
        GameManager.Instance.blizzardLv = blizzardLv;
        GameManager.Instance.darknessLv = darknessLv;
        GameManager.Instance.lightLv = lightLv;

        DeckManager.Instance.flameCardCount = flameCardCount;
        DeckManager.Instance.waterCardCount = waterCardCount;
        DeckManager.Instance.windCardCount = windCardCount;
        DeckManager.Instance.groundCardCount = groundCardCount;
        DeckManager.Instance.forestCardCount = forestCardCount;
        DeckManager.Instance.blizzardCardCount = blizzardCardCount;
        DeckManager.Instance.darknessCardCount = darknessCardCount;
        DeckManager.Instance.lightCardCount = lightCardCount;

        SpritManager.Instance.nowCoworker = spritCount;
    }
}


