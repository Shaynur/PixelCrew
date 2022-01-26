using System.Collections;
using Assets.PixelCrew.Components;
using Assets.PixelCrew.Components.ColliderBased;
using Assets.PixelCrew.Components.GoBase;
using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Creatures.Hero.Features;
using Assets.PixelCrew.Effects;
using Assets.PixelCrew.Effects.CameraRelated;
using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.Model.Definitions.Player;
using Assets.PixelCrew.Model.Definitions.Repository;
using Assets.PixelCrew.Model.Definitions.Repository.Items;
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
        [SerializeField] private ShieldComponent _shield;
        [SerializeField] private HeroFlashlight _flashlight;


        private bool _allowDoubleJump;
        private bool _isOnWall;
        private GameSession _session;
        private HealthComponent _health;
        private CameraShakeEffect _cameraShake;
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

        private void Start() {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();
            _cameraShake = FindObjectOfType<CameraShakeEffect>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgraded += OnHeroUpgraded;

            _health.SetHealth(_session.Data.Hp.Value);

            UpdateHeroWeapon();
            StartCoroutine(FadeIn());
        }

        private void OnHeroUpgraded(StatId stateId) {
            switch (stateId) {
                case StatId.Hp:
                    var health = (int)_session.StatsModel.GetValue(stateId);
                    _health.SetHealth(health);
                    break;
                case StatId.Speed:
                    break;
                case StatId.RangeDamage:
                    break;
            }
        }

        public void OnHealthChanged(int newHealth) {
            var maxHealth = (int)_session.StatsModel.GetValue(StatId.Hp);
            if (newHealth > maxHealth) {
                newHealth = maxHealth;
                _health.SetHealth(newHealth);
            }
            _session.Data.Hp.Value = newHealth;
        }

        private void OnInventoryChanged(string id, int value) {
            if (id == SwordId) {
                UpdateHeroWeapon();
            }
        }

        public void NextItem() {
            _session.QuickInventory.SetNextItem();
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
            if (!_isGrounded && _allowDoubleJump && _session.PerksModel.IsDoubleJumpSupported) {
                _session.PerksModel.Cooldown.Reset();
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(yVelocity);
        }

        public bool AddInInventory(string id, int value) {
            return _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage() {
            base.TakeDamage();
            _cameraShake.Shake();
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

        private readonly Cooldown _speedUpCooldown = new Cooldown();
        private float _additionalSpeed;
        protected override float CalculateSpeed() {
            if (_speedUpCooldown.IsReady) {
                _additionalSpeed = 0f;
            }
            var defaultSpeed = _session.StatsModel.GetValue(StatId.Speed);
            return defaultSpeed + _additionalSpeed;
        }

        public override void Attack() {
            if (SwordCount <= 0) return;
            base.Attack();
        }

        public void UseInventory() {
            if (IsSelectedItemHasTag(ItemTag.Throwable))
                PerformThrow();
            else if (IsSelectedItemHasTag(ItemTag.Potion))
                UsePotion();
        }

        private bool IsSelectedItemHasTag(ItemTag tag) {
            return _session.QuickInventory.SelectedDef.HasTag(tag);
        }

        private void UsePotion() {
            var potion = DefsFacade.I.Potions.Get(SelectedItemId);
            switch (potion.Effect) {
                case Effect.AddHp:
                    _health.ModifyHealth((int)potion.Value);
                    break;
                case Effect.SpeedUp:
                    _speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
                    _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                    _speedUpCooldown.Reset();
                    break;
            }
            _session.Data.Inventory.Remove(potion.Id, 1);
        }

        private void PerformThrow() {
            if (_throwCooldown.IsReady && CanThrow) {
                TryThrowItem();
            }
        }

        public IEnumerator SuperThrowAbility() {
            bool isThrowableItem = IsSelectedItemHasTag(ItemTag.Throwable);
            bool isHasTrowPerk = _session.PerksModel.IsSuperThrowSupported;
            if (isThrowableItem && isHasTrowPerk) {
                var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
                var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
                var numThrows = Mathf.Min(3, possibleCount);
                _session.PerksModel.Cooldown.Reset();
                for (int i = 0; i < numThrows; i++) {
                    TryThrowItem();
                    yield return new WaitForSeconds(0.3f);
                }
            }
            // yield return null; // ??
        }

        private void TryThrowItem() {
            Sounds.Play("Range");
            CreatureAnimator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }

        public void OnDoThrow() {
            var throwableId = SelectedItemId;
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            var instance = _throwSpawner.SpawnInstance();
            ApplyRangeDamageStat(instance);
            _session.Data.Inventory.Remove(throwableId, 1);
        }

        private void ApplyRangeDamageStat(GameObject projectile) {
            var hpModify = projectile.GetComponent<ModifyHealthComponent>();
            var rangeDamageValue = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
            rangeDamageValue = ModifyDamageByCrit(rangeDamageValue);
            hpModify.SetHpDelta(-rangeDamageValue);
        }

        private int ModifyDamageByCrit(int damage) {
            var critChance = _session.StatsModel.GetValue(StatId.CriticalDamage);
            if (Random.value * 100 <= critChance) {
                return damage * 2;
            }
            return damage;
        }

        public void UsePerk() {
            if (_session.PerksModel.IsShieldSupported) {
                _shield.Use();
                _session.PerksModel.Cooldown.Reset();
            }
        }

        public void ToggleFlashLight() {
            var isActive = _flashlight.gameObject.activeSelf;
            _flashlight.gameObject.SetActive(!isActive);
        }

        private void OnDestroy() {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
            _session.StatsModel.OnUpgraded -= OnHeroUpgraded;
        }
    }
}
