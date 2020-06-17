using UnityEngine;
using UnityEngine.UI;

public class ChangingBackground : MonoBehaviour
{
    public Sprite[] sprites;
    public float delay;
    public float transitionDelay;
    public float opacity;

    public Image currentImage;
    public Image nextImage;

    bool transitioning;
    int currentSprite;
    float currentDelay;
    float currentTransitionDelay;

    public void Awake()
    {
        currentImage = GetComponent<Image>();
    }

    public void Start()
    {
        currentImage.sprite = sprites[0];
        currentImage.color = Color.white * opacity;
        nextImage.color = Color.white * 0;
        currentDelay = delay;
    }

    public void Update()
    {
        if (transitioning)
        {
            currentTransitionDelay -= Time.deltaTime;

            currentImage.color = Color.white * (currentTransitionDelay / transitionDelay) * opacity;
            nextImage.color = Color.white * (1 - currentTransitionDelay / transitionDelay) * opacity;

            if (currentTransitionDelay <= 0)
            {
                transitioning = false;
                GetComponent<Image>().sprite = nextImage.sprite;

                currentDelay = delay;
            }
        }
        else if (currentDelay <= 0)
        {
            // transition
            currentSprite++;
            if (currentSprite >= sprites.Length)
                currentSprite = 0;
            nextImage.sprite = sprites[currentSprite];

            currentImage.color = Color.white * opacity;
            nextImage.color = Color.white * 0;

            transitioning = true;
            currentTransitionDelay = transitionDelay;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
