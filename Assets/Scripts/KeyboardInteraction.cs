using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class KeyboardInteraction : ZoneInteraction
{
    [SerializeField] private float cooldown = 0.5f;
    private float timer = 0f;

    void Update()
    {
        if (_isPlayerInside)
        {
            timer += Time.deltaTime;
            if(timer > cooldown)
            {
                timer = 0f;
                Execute();
            }
        }
    }

    protected override void Execute()
    {
        Debug.Log("Pressed the button");
    }
}
