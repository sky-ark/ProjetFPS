using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(10, 2000)]
    public int Speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float mouseYValue = Input.GetAxis("Mouse Y") * Speed;
        transform.Rotate(mouseYValue * Time.deltaTime, 0, 0);
    }
}
