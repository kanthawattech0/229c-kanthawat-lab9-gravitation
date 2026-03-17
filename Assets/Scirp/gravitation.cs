using UnityEngine;
using System.Collections.Generic;
public class gravitation : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    // List of attractable objects
    public static List<gravitation> otherObjectlist;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectlist == null) 
        {
            otherObjectlist = new List<gravitation>(); 
        }
        otherObjectlist.Add(this);

    }

    private void FixedUpdate()
    {
        foreach (gravitation obj in otherObjectlist)
        {
            if (obj != this) { AttractForce(obj); }

        }
    }
    void AttractForce(gravitation other)
    {
        Rigidbody otherRb = other.rb;
        //หาทิศทางระหว่างวัตถุ
        Vector3 direction = rb.position - otherRb.position;
        //ระยะห่างระหว่างวัตถุ
        float distance = direction.magnitude;
        //ถ้าวัตถุอยู่ตำแหน่งเดียวกัน ไม่ได้ทำอะไร
        if (distance == 0f) { return; }
        //ใช้สูตรหาแรงดึงดูด F = G*((m1*m2)/r^2)
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        //รวมทิศทาง เข้ากับแรงดึงดูดที่ได้
        Vector3 GravityForce = forceMagnitude * direction.normalized;
        //ใส่แรงที่ได้ให้กับวัตถุอื่น
        otherRb.AddForce(GravityForce);
    }



}