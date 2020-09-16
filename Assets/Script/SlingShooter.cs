using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D Collider;
    //menyimpan titik awal sebelum ketapel ditarik
    private Vector2 startPos;
    //radius/panjang maksimal dari tali ditarik
    [SerializeField] private float radius = 0.75f;
    //kecepatan awal yang diberikan ketapel saat melempar bird
    [SerializeField] private float throwspeed = 30f;
    private Bird _bird;
    public LineRenderer Trajectory;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateBird(Bird bird)
    {
        _bird = bird;
        _bird.MoveTo(gameObject.transform.position, gameObject);
        Collider.enabled = true;
    }

    void OnMouseUp()
    {
        Collider.enabled = false;
        Vector2 velocity = startPos - (Vector2)transform.position;
        float distance = Vector2.Distance(startPos, transform.position);

        _bird.Shoot(velocity, distance, throwspeed);

        //kembalikan ketapel ke posisi awal
        gameObject.transform.position = startPos;
        Trajectory.enabled = false;
    }

    void OnMouseDrag()
    {
        //mengubah posisi mouse ke world position
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //hitung supaya karet ketapel berada dalam radius yang ditentukan
        Vector2 dir = p - startPos;
        if(dir.sqrMagnitude > radius)
        {
            dir = dir.normalized * radius;
        }
        transform.position = startPos + dir;

        float distance = Vector2.Distance(startPos, transform.position);
        if (!Trajectory.enabled)
        {
            Trajectory.enabled = true;
        }

        DisplayTrajectory(distance);
    }

    void DisplayTrajectory(float distance)
    {
        if(_bird == null)
        {
            return;
        }

        Vector2 velocity = startPos - (Vector2)transform.position;
        //total poin/titik yang akan digambarkan dalam trajectory
        int segmentCount = 5;
        Vector2[] segments = new Vector2[segmentCount];

        //posisi awal trajectory merupakan posisi mouse dari player saat ini
        segments[0] = transform.position;

        //velocity awal
        Vector2 segVelocity = velocity * throwspeed * distance;

        for(int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }

        Trajectory.positionCount = segmentCount;
        for(int i = 0; i < segmentCount; i++)
        {
            Trajectory.SetPosition(i, segments[i]);
        }
    }

    

    
}
