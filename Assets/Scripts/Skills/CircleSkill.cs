using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSkill : AttackSkill
{
    public Transform pivotTrans;
    private float y_angle;
    public int shotCount;

    public GameObject projectilePrefab;

    public List<GameObject> projectiles;

    private void Awake()
    {
        projectiles = new List<GameObject>();
    }


    private void Start()
    {
        LevelUP();
    }

    // Update is called once per frame
    void Update()
    {
        UseSkill();
        y_angle += Time.deltaTime * 360f; //1초에 1바퀴

        if(y_angle > 360f)
            y_angle = 0f;
        
    }

    public override void UseSkill()
    {
        pivotTrans.rotation = Quaternion.Euler(0, y_angle, 0);
    }

    public void MakePrefabs()
    {
        projectiles.Add(Instantiate(projectilePrefab, transform));

        //발사체 개수에 따라 위치 조정
        for (int i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].transform.localPosition = Quaternion.Euler(0, (360.0f / projectiles.Count) * i, 0) * new Vector3(5f, 0f);
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
        while (shotCount > projectiles.Count)
        {
            MakePrefabs(); //투사체 하나 증가
                           //그 이외 공격력 등의 수치 변경
        }
    }
}
