using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    List<Vector2> point = new List<Vector2>();
    [SerializeField]
    [Range(0, 1)]
    private float t = 0;
    private float spd = 3f;
    private float radiusA = 1.55f;
    private float radiusB = 1.45f;
    private GameObject master;
    private Vector2 startPos;
   
    [SerializeField]
    Vector3 objctPos;
    void Start()
    {
        master = GameObject.Find("Player");
        //UIManager.Instance.master = master;
        if (master != null) 
        {
            transform.position = master.transform.position;
            startPos = new Vector2(master.transform.position.x + 1f, master.transform.position.y + 0.2f);
            objctPos = 3 * (Vector3.right + Vector3.up).normalized;
            point.Add(startPos);
            point.Add(SetRandomBezierPointP2(master.transform.position, radiusA));
            point.Add(SetRandomBezierPointP2(objctPos, radiusB));
            point.Add(new Vector3(objctPos.x - 0.3f, objctPos.y));
        }

    }
    private void OnEnable()
    {
        t = 0;
    }

    void Update()
    {
        if (t > 1)
        {
            this.gameObject.SetActive(false);
            return;
        }
        t += Time.deltaTime * spd;
        transform.position = MoveBezier();
    }
    Vector2 MoveBezier()
    {
        return new Vector2(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y)
            );
    }
    private float FourPointBezier(float a, float b, float c, float d)
    {
        return Mathf.Pow(1 - t, 3) * a + Mathf.Pow(1 - t, 2) * 3 * t * b + Mathf.Pow(t, 2) * 3 * (1 - t) * c + Mathf.Pow(t, 3) * d;
    }

    Vector2 SetRandomBezierPointP2(Vector2 origin, float radius)
    {
        return new Vector2(
            radius * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.x,
            radius * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.y

            );
    }
    
}
