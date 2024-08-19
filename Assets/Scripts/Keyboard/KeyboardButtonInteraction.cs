using TMPro;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class KeyboardButtonInteraction : ZoneInteraction
{
    [SerializeField] private KeyCode key = KeyCode.None;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private float cooldown = 0.8f;
    private float timer = 0f;

    public delegate void ButtonPress(KeyCode pressedKey);
    public static event ButtonPress OnButtonPressed;

    private void Start()
    {
        keyText.text = key.ToString().Replace("Alpha","");
    }

    void Update()
    {
        if (_isObjectInside)
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
        OnButtonPressed(key);
        Debug.Log("Pressed the button");
    }
}
