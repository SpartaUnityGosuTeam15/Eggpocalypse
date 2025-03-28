using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSkill : BaseSkill
{
    public Transform pivotTrans;
    public Collider attackCollider;
    private float y_angle;


    // Update is called once per frame
    void Update()
    {
        UseSkill();
        y_angle += Time.deltaTime * 360f;

        if(y_angle > 360f)
            y_angle = 0f;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public override void UseSkill()
    {
        pivotTrans.rotation = Quaternion.Euler(0, y_angle, 0);
    }
}
