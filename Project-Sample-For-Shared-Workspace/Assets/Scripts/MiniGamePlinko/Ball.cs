using UnityEngine;

namespace MiniGamePlinko
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        public PhysicsMaterial2D _material;

        // Set the initial bounciness value
        public float bounceFactor = 0.5f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();

            //GetComponent<CircleCollider2D>().sharedMaterial = _material;
            // cd.sharedMaterial = _material;

            //_material = rb.sharedMaterial;
            //if (_material == null)
            //{
            //    Debug.LogError("ChangeBounciness script requires a Physics Material 2D component!");
            //    return;
            //}
            //else
            //{
            //}
            rb.Sleep();
        }

        private void Update()
        {
            if (Input.anyKeyDown && rb.IsSleeping())
            {
                rb.WakeUp();
            }

            if (!rb.IsSleeping())
            {
                Mathf.Clamp(bounceFactor, 0f, 1f);
                _material.bounciness = bounceFactor;
                cd.sharedMaterial = _material;
            }

            Debug.Log(_material.bounciness);
        }
    }
}