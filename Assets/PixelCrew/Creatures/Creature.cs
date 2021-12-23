using Assets.PixelCrew.Components.Audio;
using Assets.PixelCrew.Components.ColliderBased;
using Assets.PixelCrew.Components.GoBase;
using UnityEngine;

namespace Assets.PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params:")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [Header("Checkers:")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [Header("Particles:")]
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator CreatureAnimator;
        protected PlaySoundsComponent Sounds;
        public bool _isGrounded;
        private bool _isJumping;
        private const float _decay = 0.5f;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            CreatureAnimator = GetComponent<Animator>();
            Sounds = GetComponentInChildren<PlaySoundsComponent>();
        }

        protected virtual void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            var newVelocity = new Vector2(xVelocity, yVelocity);
            Rigidbody.velocity = newVelocity;

            CreatureAnimator.SetBool(IsGroundKey, _isGrounded);
            CreatureAnimator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            CreatureAnimator.SetBool(IsRunning, Direction.x != 0);

            UpdateSpriteDirection(Direction);
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (_isGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= _decay;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (_isGrounded)
            {
                yVelocity = _jumpSpeed;
                DoJumpVfx();
            }
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.Play("Jump");
        }

        public virtual void TakeDamage()
        {
            _isJumping = false;
            CreatureAnimator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        public virtual void Attack()
        {
            CreatureAnimator.SetTrigger(AttackKey);
            Sounds.Play("Melee");
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
        }
    }
}