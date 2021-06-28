using System;
using System.Numerics;

[Serializable]
public class PlayerData 
{
    public string money;

    public int stageLevel;
    
    public int clickLevel;

    public int firstSkillLv; //���� �ð����� �⺻���� ������ �߻� ��ų ����
    public int secondSkillLv; //�����ð� ���� ���� ���ݼӵ� ���� ��ų ����
    public int thirdSkillLv; // �����ð� ���� �⺻���� ������ ���
    public int fourthSkillLv; // �����ð� ���� �Ǵ� �⺻���� Ƚ�� �߰�

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


