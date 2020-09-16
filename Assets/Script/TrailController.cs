using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject Trail;
    public Bird TargetBird;

    private List<GameObject> trails;

    // Start is called before the first frame update
    void Start()
    {
        trails = new List<GameObject>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBird(Bird bird)
    {
        TargetBird = bird;
        for(int i = 0; i < trails.Count; i++)
        {
            Destroy(trails[i].gameObject);
        }
        trails.Clear();
    }

    public IEnumerator SpawnTrail()
    {
        trails.Add(Instantiate(Trail, TargetBird.transform.position, Quaternion.identity));
        yield return new WaitForSeconds(0.1f);

        if(TargetBird != null && TargetBird.State != Bird.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrail());
        }
    }
}
