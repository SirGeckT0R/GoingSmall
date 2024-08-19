using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CursorInteraction : ZoneInteraction
{
    private ScreenButton _button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _button = collision.gameObject.GetComponent<ScreenButton>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _button = _button == collision.gameObject.GetComponent<ScreenButton>() ? null: _button;
    }

    public void Interact()
    {
        if(_button != null)
        {
            _button.Execute();
        }
    }
}
