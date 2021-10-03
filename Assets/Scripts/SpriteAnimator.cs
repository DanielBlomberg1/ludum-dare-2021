using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private float frameRate = .1f;
    private SpriteRenderer spriteRenderer;
    public bool DestroyOnLoop;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.1f)
        {
            timer -= frameRate;
            if ((currentFrame + 1) == frameArray.Length && DestroyOnLoop)
            {
                Destroy(gameObject);
            }
            currentFrame = (currentFrame + 1) % frameArray.Length;
            spriteRenderer.sprite = frameArray[currentFrame];
        }
    }
}
