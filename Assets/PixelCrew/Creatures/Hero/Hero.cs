using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.PixelCrew.Components.ColliderBased;
using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Model;
using Assets.PixelCrew.Utils;

namespace Assets.PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        [SerializeField] private ParticleSystem _hitParticles;


        private bool _allowDoubleJump;
        //private bool _firstJumpPressing;
        private bool _isOnWall;
        private GameSession _session;
        private float _defaultGravityScale;

        private int CoinsCount => _session.Data.Inventory.Count("Coins");
        private int SwordCount => _session.Data.Inventory.Count("Sword");

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }


        private void Start()
        {
            StartCoroutine(FadeIn());
            _session = FindObjectOfType<GameSession>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            var health = GetComponent<HealthComponent>();
            if (health != null)
            {
                health.SetHealth(_session.Data.Hp);
            }
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
            {
                UpdateHeroWeapon();
            }
        }

        private void UpdateHeroWeapon()
        {
            CreatureAnimator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        private IEnumerator FadeIn()
        {
            var renderer = GetComponent<Renderer>();
            for (float f = 0f; f <= 1; f += 0.1f)
            {
                Color c = renderer.material.color;
                c.a = f;
                renderer.material.color = c;
                yield return new WaitForSeconds(.1f);
            }
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();

            if (_wallCheck.IsTouchingLayer && Direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (_isGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(yVelocity);
        }

        public bool AddInInventory(string id, int value)
        {
            return _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= 20)
                {
                    _particles.Spawn("SlamDown");
                }
            }
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;
            base.Attack();
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_throwCooldown.IsReady)
            {
                TryThrowSword();
            }
        }

        public void UseHPpotion()
        {
            if (_session.Data.Inventory.Count("HPpotion") < 1) return;
            _session.Data.Inventory.Remove("HPpotion", 1);
            var healthComponent = GetComponent<HealthComponent>();
            healthComponent.ModifyHealth(5);
        }

        public IEnumerator ThrowAbility()
        {
            for (int i = 0; i < 3; i++)
            {
                if (TryThrowSword())
                {
                    yield return new WaitForSeconds(0.3f);
                }
            }
            yield return null;
        }

        private bool TryThrowSword()
        {
            if (SwordCount > 1)
            {
                _session.Data.Inventory.Remove("Swords", 1);
                CreatureAnimator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
                return true;
            }
            return false;
        }
    }

}

