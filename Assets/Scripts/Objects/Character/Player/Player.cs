using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerCondition condition;
    public List<Monster> monsters = new List<Monster>();
    public float detectRadius = 10;
    public LayerMask monsterLayer;
    public Monster closest;
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);

    private void Awake()
    {
        GameManager.Instance.player = this;

        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        StartCoroutine(findNearest());
    }

    IEnumerator findNearest()
    {
        while (true)
        {
            closest = ObjectManager.Instance.GetNearesrMonster(detectRadius);

            yield return waitTime;
        }
    }

    private void Update()
    {
        Debug.Log(closest);
        Debug.Log(closest);
        Debug.Log(closest);
    }

    //public void DetectMonster()
    //{
    //    monsters.Clear();

    //    Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius, monsterLayer);

    //    foreach (Collider hit in hits)
    //    {
    //        monsters.Add(hit.GetComponent<Monster>());
    //    }
    //}

    //public void FindClosestMonster()
    //{
    //    if (monsters.Count == 0) return;

    //    float min = detectRadius;
    //    foreach (Monster monster in monsters)
    //    {
    //        //float distance = Vector3.Distance(transform.position, monster.transform.position);
    //        min = Mathf.Min(min, monster.Agent.remainingDistance);
    //        closest = monster;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawSphere(transform.position, detectRadius);
    }
}
