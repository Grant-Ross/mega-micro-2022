using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace grant
{
    public class Hat : MonoBehaviour
    {
        private Tween movement;
        private bool moving = true;
        public AudioClip clapSound;

        private void Start()
        {
            StartCoroutine(MoveHat());
        }

        private IEnumerator MoveHat()
        {
            var time = .7f;
            while (true)
            {
                movement = transform.DOMoveY(.07f, time).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(time);
                if (!moving) break;
                movement = transform.DOMoveY(0.676f, time).SetEase(Ease.InOutQuad);
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
            }
        }

        public Animator animator;
        public IEnumerator EndSequence()
        {
            animator.Play("main");
            yield return new WaitForSeconds(1);
            AudioSource clap = Managers.AudioManager.CreateAudioSource();
            clap.PlayOneShot(clapSound);
        }
        
    }
}

