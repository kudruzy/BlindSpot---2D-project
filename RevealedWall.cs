using UnityEngine;

public class RevealedWall : MonoBehaviour
{
    private bool isRevealed = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetTileVisibility(false);
    }

    public void Reveal()
    {
        if (isRevealed) return;

        isRevealed = true;
        SetTileVisibility(true);
    }

    public void RevealTile()
    {
        Reveal();
    }

    private void SetTileVisibility(bool revealed)
    {
        if (spriteRenderer != null)
        {
            if (revealed)
            {
                // Fully lit / bright white when echolocated
                spriteRenderer.color = Color.white;
            }
            else
            {
                // Dark gray silhouette so they aren't totally invisible before being scanned
                spriteRenderer.color = new Color(0.15f, 0.15f, 0.15f, 1f);
            }
        }
    }
}