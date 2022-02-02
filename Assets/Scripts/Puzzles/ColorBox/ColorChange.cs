using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Color _thisColor;
    public Color ThisColor => _thisColor;
    int index;
    MeshRenderer _meshRenderer;
    public MeshRenderer ThisMeshRenderer => _meshRenderer;
    BoxCollider boxCollider;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        index = 0;
        _thisColor = Color.red;
        Disable();
    }

    public void ChangeColor()
    {
        index++;
        if (index > 5) index = 0;
        switch (index)
        {
            case 0:
                _thisColor = Color.red;
                break;
            case 1:
                _thisColor = Color.Lerp(Color.red, Color.yellow, 0.5f);
                break;
            case 2:
                _thisColor = Color.yellow;
                break;
            case 3:
                _thisColor = Color.green;
                break;
            case 4:
                _thisColor = Color.blue;
                break;
            case 5:
                _thisColor = Color.Lerp(Color.blue, Color.red, 0.5f);
                break;
        }
    }

    public void Enable()
    {
        boxCollider.enabled = true;
    }

    void Disable()
    {
        boxCollider.enabled = false;
    }
}
