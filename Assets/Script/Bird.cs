using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public Rigidbody2D RigidBody;
    public CircleCollider2D Collider;
    //event delegate
    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };

    public BirdState State { get { return state; } }

    private BirdState state;
    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        state = BirdState.Idle;
    }

    //method bawaan monobehavior dimana method akan terus dipanggil pada setiap fixed frame
    void FixedUpdate()
    {
        if(state == BirdState.Idle && RigidBody.velocity.sqrMagnitude >= minVelocity)
        {
            state = BirdState.Thrown;
        }

        if((state == BirdState.Thrown || state == BirdState.HitSomething) && RigidBody.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
            //hancurkan objek setelah 2 detik jika kecepatannya sudah kurang dari batas minimum
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(state == BirdState.Thrown || state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        state = BirdState.HitSomething;
    }

    //menginisiasi posisi dan mengubah parent dari game object bird
    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    //melemparkan burung dengan arah, jarak tali yang ditarik dan kecepatan awal
    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        Collider.enabled = true;
        RigidBody.bodyType = RigidbodyType2D.Dynamic;
        RigidBody.velocity = velocity * speed * distance;
        //memberi tanda bahwa trail sudah dapat di spawn
        OnBirdShot(this);
    }

    //virtual : agar class turunan bisa override fungsi ini
    public virtual void OnTap()
    {
        //jika bird merah biasa yang dilempar maka waktu dilontarkan tidak ada efek apa2
    }
    
}
