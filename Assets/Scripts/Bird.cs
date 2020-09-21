using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [SerializeField]
    int birdFlyForce = 200;
    [SerializeField]
    Sprite[] flyAnimationSprites;
    SpriteRenderer spriteRenderer;
    Rigidbody2D birdRB;
    CircleCollider2D birdCollider;
    Vector3 resetPosition;

    public delegate void OnScore();
    public OnScore OnScoreIncrease;

    public delegate void OnHit();
    public OnHit OnHitNotifier;

    private void Awake()
    {
        resetPosition = transform.position;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        birdRB = GetComponent<Rigidbody2D>();
        birdCollider = GetComponent<CircleCollider2D>();

        OnHitNotifier += BirdFall;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pass")
        {
            OnScoreIncrease();
            SoundManager.Instance.PlaySoundSource("score");
            //SoundManager.Instance.PlaySfxFromThirdAudioSource("score");
        }
        else
        {
            OnHitNotifier();
            SoundManager.Instance.PlaySoundSource("hit");
            //SoundManager.Instance.PlaySfxFromSecondAudioSource("hit");
            SetBirdUntouchable();
        }
    }
    // Set enable/disable collider for extra sound
    public void SetBirdUntouchable()
    {
        birdCollider.enabled = false;
    }
    public void SetBirdTouchable()
    {
        birdCollider.enabled = true;
    }

    public void Fly()
    {
        FlyForce();
        BirdFlyAnimation();
        SoundManager.Instance.PlaySoundSource("flap");
        //SoundManager.Instance.PlaySfxFromFirstAudioSource("flap");
    }

    public void CheckForRotation()
    {
        if (birdRB.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 25);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
    }

    public void ResetPosition()
    {
        transform.position = resetPosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void BirdFall()
    {
        birdRB.velocity = new Vector2(0, 0);
    }

    void FlyForce()
    {
        birdRB.velocity = new Vector2(0, 0);
        birdRB.AddForce(new Vector2(0, birdFlyForce));
    }

    void BirdFlyAnimation()
    {
        StartCoroutine(PlayFlyAnimation());
    }

    IEnumerator PlayFlyAnimation()
    {
        if (flyAnimationSprites != null)
        {
            //Flaping down side
            for (int i = 0; i < flyAnimationSprites.Length; i++)
            {
                spriteRenderer.sprite = flyAnimationSprites[i];
                yield return new WaitForSeconds(0.1f);
            }
            //Flaping up side
            for (int i = flyAnimationSprites.Length - 1; i >= 0; i--)
            {
                spriteRenderer.sprite = flyAnimationSprites[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForEndOfFrame();
    }

}
