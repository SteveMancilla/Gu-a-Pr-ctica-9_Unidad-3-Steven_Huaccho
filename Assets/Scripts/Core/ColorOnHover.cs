using UnityEngine;

public class ColorOnHover : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _normalColor = Color.red;
    [SerializeField] private Color _hoverColor = Color.green;

    private void Reset()
    {
        // Se asigna solo cuando agregas el componente
        _renderer = GetComponent<MeshRenderer>();
    }

    public void OnHoverEnter()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _hoverColor;
        }
    }

    public void OnHoverExit()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _normalColor;
        }
    }
}