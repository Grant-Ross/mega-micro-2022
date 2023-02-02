using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace grant
{
    public class Hand : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Tween movement;
        private bool moving = true;

        public Sprite tap1;
        public Sprite tap2;

        public Image BG;
        public Transform Light;

        private void Start()
        {
            StartCoroutine(MoveHand());
            _renderer = GetComponent<SpriteRenderer>();
        }

        private IEnumerator MoveHand()
        {
            var time = .5f;
            while (true)
            {
                movement = transform.DOMoveY(-1.733f, time).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(time);
                if (!moving) break;
                movement = transform.DOMoveY(2.293f, time).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(time);
                if (!moving) break;
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Space"))
            {
                moving = false;
                movement.Kill();
                movement = transform.DOMoveX(8.7f, .8f).SetEase(Ease.InQuad);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Object 1"))
            {
                (BG.transform as RectTransform).DOShakeAnchorPos(.7f, 10);
                Light.gameObject.SetActive(true);
                Light.transform.position += new Vector3(Random.Range(2f, -2f),0,0);
                int flip = Random.Range(0f, 1f) > .5f ? 1 : -1;
                Light.GetComponent<Rigidbody2D>().AddTorque(3 * flip, ForceMode2D.Impulse);
                //Light.transform.DORotate(new Vector3(0, 0, 179 * flip), Random.Range(2.5f, 3f)).SetEase(Ease.OutExpo);
                return;
            }
            
            movement.Kill();
            StartCoroutine(DoWinSequence());
            
        }

        public Hat hat;
        private IEnumerator DoWinSequence()
        {
            Managers.MinigamesManager.DeclareCurrentMinigameWon();
            yield return new WaitForSeconds(.1f);
            _renderer.sprite = tap1;
            yield return new WaitForSeconds(.1f);
            _renderer.sprite = tap2;
            yield return new WaitForSeconds(.1f);
            _renderer.sprite = tap1;
            yield return new WaitForSeconds(.1f);
            _renderer.sprite = tap2;
            yield return new WaitForSeconds(.2f);
            StartCoroutine(hat.EndSequence());
        }
    }
}
