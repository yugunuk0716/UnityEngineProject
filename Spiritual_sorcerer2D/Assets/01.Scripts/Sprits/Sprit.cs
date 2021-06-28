using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
public enum Type
{
    flame,
    light,
    darkness,
    blizzard,
    forest,
    ground,
    wind,
    water
}

public class Sprit : MonoBehaviour
{
    public Type type;
    public int lv = 1;
    public float speed = 0.1f;
    public DungoenMobSpawner ds;
    

    public EnemySpawner es;
    private float typePerDelay = 0;
    private Transform ePos;
    private GameObject obj;
    private WaitForSeconds ws = new WaitForSeconds(1f);

    private void Update()
    {
        
    }
    void Start()
    {

        GameManager.Instance.attackDelay = new WaitForSeconds(3f);
        switch (type)
        {
            case Type.flame:
                typePerDelay = 0.112f;
                SpritManager.Instance.fireDamage = SetDamage(1f);
                break;
            case Type.light:
                typePerDelay = 0.218f;
                SpritManager.Instance.lightDamage = SetDamage(8f);
                break;
            case Type.darkness:
                typePerDelay = 0.202f;
                SpritManager.Instance.darknessDamage = SetDamage(7f);
                break;
            case Type.blizzard:
                typePerDelay = 0.188f;
                SpritManager.Instance.blizzardDamage = SetDamage(6f);
                break;
            case Type.forest:
                typePerDelay = 0.172f;
                SpritManager.Instance.forestDamage = SetDamage(5f);
                break;
            case Type.ground:
                typePerDelay = 0.158f;
                SpritManager.Instance.groundDamage = SetDamage(4f);
                break;
            case Type.wind:
                typePerDelay = 0.142f;
                SpritManager.Instance.windDamage = SetDamage(3f);
                break;
            case Type.water:
                typePerDelay = 0.128f;
                SpritManager.Instance.waterDamage = SetDamage(2f);
                break;
            default:
                break;
        }
        SetLv();
        StartCoroutine(SpritAttack());

    }

    public void SetLv()
    {
        switch (type)
        {
            case Type.flame:
                lv = GameManager.Instance.flameLv;
                break;
            case Type.light:
                lv = GameManager.Instance.lightLv;
                break;
            case Type.darkness:
                lv = GameManager.Instance.darknessLv;
                break;
            case Type.blizzard:
                lv = GameManager.Instance.blizzardLv;
                break;
            case Type.forest:
                lv = GameManager.Instance.forestLv;
                break;
            case Type.ground:
                lv = GameManager.Instance.groundLv;
                break;
            case Type.wind:
                lv = GameManager.Instance.windLv;
                break;
            case Type.water:
                lv = GameManager.Instance.waterLv;
                break;
            default:
                break;
        }
    }
    private WaitForSeconds SetWaitForSec(float _typePerDelay)
    {
        WaitForSeconds ws = new WaitForSeconds(_typePerDelay * 50f * 2f);
        return ws;
    }
    private BigInteger SetDamage(float _typePerDamage)
    {

        BigInteger temp = (BigInteger)(20 * Mathf.Pow(2, _typePerDamage) * Mathf.Pow(1.07f, lv) * 2f);
        return temp;
    }

    private IEnumerator SpritAttack()
    {

        while (true)
        {

            SetLv();
            if (GameManager.Instance.isSpawned)
            {
                yield return SetWaitForSec(typePerDelay);

                if (SceneController.isLoaded)
                {
                    if (es != null)
                    {
                        ePos = es.transform;
                    }
                    else if (ds != null)
                    {
                        ePos = ds.transform;
                    }
                    GameObject prefab;
                    PoolManager.Instance.bulletType.TryGetValue(type.ToString(), out prefab);
                    if (prefab != null)
                    {
                        print(prefab.name);
                        obj = Instantiate(prefab, this.gameObject.transform);
                        if (ePos != null)
                        {
                            obj.transform.position = ePos.transform.position;
                        }
                        else
                        {
                            obj.transform.position = ePos.transform.position;
                        }

                    }
                    Destroy(obj, 0.5f);


                    if (GameManager.Instance.enemies.Count != 0)
                    {
                        switch (type)
                        {
                            case Type.flame:

                                GameManager.Instance.Hit(SpritManager.Instance.fireDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[0];
                                break;
                            case Type.water:
                                GameManager.Instance.Hit(SpritManager.Instance.waterDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[1];
                                break;
                            case Type.wind:
                                GameManager.Instance.Hit(SpritManager.Instance.windDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[2];
                                break;
                            case Type.ground:
                                GameManager.Instance.Hit(SpritManager.Instance.groundDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[3];
                                break;
                            case Type.forest:
                                GameManager.Instance.Hit(SpritManager.Instance.forestDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[4];
                                break;
                            case Type.blizzard:
                                GameManager.Instance.Hit(SpritManager.Instance.blizzardDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[5];
                                break;
                            case Type.darkness:
                                GameManager.Instance.Hit(SpritManager.Instance.darknessDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[6];
                                break;
                            case Type.light:
                                GameManager.Instance.Hit(SpritManager.Instance.lightDamage);
                                SpritManager.Instance.source.clip = SpritManager.Instance.audioClips[7];
                                break;
                            default:
                                break;
                        }
                        SpritManager.Instance.source.Play();
                    }
                }
                if (!GameManager.Instance.isSpawned)
                {
                    Destroy(obj);
                    SpritManager.Instance.source.Stop();
                }
            }

            yield return null;
        }

    }

}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy")) 
    //    {
    //        Destroy(obj);
    //    }
    //}

