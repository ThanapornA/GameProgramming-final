using UnityEngine;

public class TimeBoostItem : MonoBehaviour
{
    public bool isTimeBoosted = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag("Player") )
        {
            isTimeBoosted = true;
        }
    }
}
