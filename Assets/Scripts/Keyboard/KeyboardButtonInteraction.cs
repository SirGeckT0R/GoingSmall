using TMPro;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class KeyboardButtonInteraction : ZoneInteraction
{
    [SerializeField] private KeyCode key = KeyCode.None;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private float cooldown = 0.8f;
    [SerializeField] private float pressedAnimationDuration = 0.4f;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;
    private float timer = 0f;

    public delegate void ButtonPress(KeyCode pressedKey);
    public static event ButtonPress OnButtonPressed;

    private void Start()
    {
        if (keyText != null)
        {
            keyText.text = key.ToString().Replace("Alpha", "");
        }
    }

    void Update()
    {
        if (_isObjectInside)
        {
            timer += Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = pressedSprite;
            if (timer > pressedAnimationDuration)
            {
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
            if(timer > cooldown)
            {
                timer = 0f;
                Execute();
            }
        }
        else
        {
            timer = 0f;
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }
    }

    protected override void Execute()
    {
        OnButtonPressed(key);
        Debug.Log("Pressed the button");
    }
}
