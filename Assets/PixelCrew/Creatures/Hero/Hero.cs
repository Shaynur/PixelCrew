using System.Collections;
using Assets.PixelCrew.Components.ColliderBased;
using Assets.PixelCrew.Components.GoBase;
using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.PixelCrew.Creatures.Hero {
    public class Hero : Creature, ICanAddInInventory {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private SpawnComponent _throwSpawner;


        private bool _allowDoubleJump;
        private bool _isOnWall;
        private GameSession _session;
        private HealthComponent _health;
        private float _defaultGravityScale;
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private const string SwordId = "Sword";
        private int CoinsCount => _session.Data.Inventory.Count("Coins");
        private int SwordCount => _session.Data.Inventory.Count(SwordId);
        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;
        private bool CanThrow {
            get {
                if (SelectedItemId == SwordId) {
                    return SwordCount > 1;
                }
                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }


        protected override void Awake() {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        public void NextItem() {
            _session.QuickInventory.SetNextItem();
        }

        private void Start() {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _health = GetComponent<HealthComponent>();
            _health.SetHealth(_session.Data.Hp.Value);

            UpdateHeroWeapon();
            StartCoroutine(FadeIn());
        }

        public void OnHealthChanged(int newHealth) {
            _session.Data.Hp.Value = Mathf.Min(newHealth, DefsFacade.I.Player.MaxHealth);
            if (newHealth > DefsFacade.I.Player.MaxHealth) {
                _health.SetHealth(DefsFacade.I.Player.MaxHealth);
            }
        }

        private void OnDestroy() {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value) {
            if (id == SwordId) {
                UpdateHeroWeapon();
            }
        }

        private void UpdateHeroWeapon() {
            CreatureAnimator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        private IEnumerator FadeIn() {
            var renderer = GetComponent<Renderer>();
            for (float f = 0f; f <= 1; f += 0.1f) {
                Color c = renderer.material.color;
                c.a = f;
                renderer.material.color = c;
                yield return new WaitForSeconds(.1f);
            }
        }

        protected override void Update() {
            base.Update();

            if (_wallCheck.IsTouchingLayer && Direction.x == transform.localScale.x) {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }
        }

        protected override float CalculateYVelocity() {
            var isJumpPressing = Direction.y > 0;

            if (_isGrounded || _isOnWall) {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall) {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity) {
            if (!_isGrounded && _allowDoubleJump) {
                DoJumpVfx();
                _allowDoubleJump = false;
                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(yVelocity);
        }

        public bool AddInInventory(string id, int value) {
            return _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage() {
            base.TakeDamage();
            if (CoinsCount > 0) {
                SpawnCoins();
            }
        }

        private void SpawnCoins() {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact() {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.IsInLayer(_groundLayer)) {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= 20) {
                    _particles.Spawn("SlamDown");
                }
            }
        }

        public override void Attack() {
            if (SwordCount <= 0) return;
            base.Attack();
        }

        public void OnDoThrow() {
            var throwableId = SelectedItemId;
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();
            _session.Data.Inventory.Remove(throwableId, 1);
            //_particles.Spawn("Throw");
        }

        public void Throw() {
            if (_throwCooldown.IsReady && CanThrow) {
                TryThrowItem();
            }
        }

        public IEnumerator SuperThrowAbility() {
            var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
            var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
            var numThrows = Mathf.Min(3, possibleCount);
            for (int i = 0; i < numThrows; i++) {
                TryThrowItem();
                yield return new WaitForSeconds(0.3f);
            }
            yield return null;
        }

        private void TryThrowItem() {
            Sounds.Play("Range");
            CreatureAnimator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }
    }
}
