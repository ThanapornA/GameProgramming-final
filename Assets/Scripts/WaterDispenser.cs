using UnityEngine;

public class WaterDispenser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        Debug.Log("This is WaterDispenser");
    }
}
