using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseInteraction : ZoneInteraction
{
    private CursorInteraction _cursor;
    private SpriteRenderer _mouseSpriteRenderer;
    private bool _isMouseButtonPressed;
    private float _timer = 0f;
    [SerializeField] private GameObject PopUp;
    [SerializeField] private float pressedAnimationDuration = 0.4f;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private AudioClip pressedSound;
    private void Start()
    {
        _cursor = transform.parent.GetComponent<CursorHandling>().cursorTransform.GetComponent<CursorInteraction>();
        _mouseSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_isObjectInside && Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySound(pressedSound);
            _cursor.Interact();
            _isMouseButtonPressed = true;
        }
        if (_isMouseButtonPressed)
        {
            _timer += Time.deltaTime;
            _mouseSpriteRenderer.sprite = pressedSprite;
            if (_timer > pressedAnimationDuration)
            {
                _timer = 0f;
                _mouseSpriteRenderer.sprite = defaultSprite;
                _isMouseButtonPressed = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {

            _isObjectInside = true;
            if (PopUp != null)
            {
                PopUp.SetActive(true);
            }
            Execute();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isObjectInside = false;
            if (PopUp != null)
            {
                PopUp.SetActive(false);
            }
        }
    }
}
