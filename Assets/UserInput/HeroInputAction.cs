// GENERATED AUTOMATICALLY FROM 'Assets/UserInput/HeroInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HeroInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HeroInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroInputAction"",
    ""maps"": [
        {
            ""name"": ""Hero"",
            ""id"": ""27a3cfa3-459d-44e0-a45c-4a2735e78a6e"",
            ""actions"": [
                {
                    ""name"": ""MainMenu"",
                    ""type"": ""Button"",
                    ""id"": ""dfb5aaa2-123f-45ad-8039-f05f23333573"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a23dd7d4-a883-410b-a69a-1a0ab42412de"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""a1efbc30-0729-4d82-aad0-87c7afa427fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""59a736f3-8a4c-4f6f-90cd-88f5495e8b21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""8387e30b-b744-4291-a39a-879e18b390e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseHPpotion"",
                    ""type"": ""Button"",
                    ""id"": ""bffb6900-dc9d-4bdb-b5fc-4d171304be45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""692a8487-178b-42c1-afad-1aa168e81fef"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99424ab5-f527-4390-8b46-21c562e1efb6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f74c0e78-b66c-4122-9a19-8ace7934f600"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5f58527d-77be-4e16-bac3-d2d515faedbd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""243e6967-3e92-4876-85bf-d750189c30fd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""79554f7c-23ed-4317-979e-8c1fda134e1b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""939f5c16-2e88-4ea7-92fe-95db9afdf6b3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12f53178-8f73-4f3e-b53d-aeedc6076100"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold(duration=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""208205d9-84d3-48aa-9e00-cf30c53a68f4"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseHPpotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""491788bb-0c58-4a82-80ff-e16621250231"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Hero
        m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
        m_Hero_MainMenu = m_Hero.FindAction("MainMenu", throwIfNotFound: true);
        m_Hero_Movement = m_Hero.FindAction("Movement", throwIfNotFound: true);
        m_Hero_Interact = m_Hero.FindAction("Interact", throwIfNotFound: true);
        m_Hero_Attack = m_Hero.FindAction("Attack", throwIfNotFound: true);
        m_Hero_Throw = m_Hero.FindAction("Throw", throwIfNotFound: true);
        m_Hero_UseHPpotion = m_Hero.FindAction("UseHPpotion", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Hero
    private readonly InputActionMap m_Hero;
    private IHeroActions m_HeroActionsCallbackInterface;
    private readonly InputAction m_Hero_MainMenu;
    private readonly InputAction m_Hero_Movement;
    private readonly InputAction m_Hero_Interact;
    private readonly InputAction m_Hero_Attack;
    private readonly InputAction m_Hero_Throw;
    private readonly InputAction m_Hero_UseHPpotion;
    public struct HeroActions
    {
        private @HeroInputAction m_Wrapper;
        public HeroActions(@HeroInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @MainMenu => m_Wrapper.m_Hero_MainMenu;
        public InputAction @Movement => m_Wrapper.m_Hero_Movement;
        public InputAction @Interact => m_Wrapper.m_Hero_Interact;
        public InputAction @Attack => m_Wrapper.m_Hero_Attack;
        public InputAction @Throw => m_Wrapper.m_Hero_Throw;
        public InputAction @UseHPpotion => m_Wrapper.m_Hero_UseHPpotion;
        public InputActionMap Get() { return m_Wrapper.m_Hero; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActions instance)
        {
            if (m_Wrapper.m_HeroActionsCallbackInterface != null)
            {
                @MainMenu.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnMainMenu;
                @MainMenu.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnMainMenu;
                @MainMenu.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnMainMenu;
                @Movement.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Throw.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @UseHPpotion.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHPpotion;
                @UseHPpotion.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHPpotion;
                @UseHPpotion.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHPpotion;
            }
            m_Wrapper.m_HeroActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MainMenu.started += instance.OnMainMenu;
                @MainMenu.performed += instance.OnMainMenu;
                @MainMenu.canceled += instance.OnMainMenu;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @UseHPpotion.started += instance.OnUseHPpotion;
                @UseHPpotion.performed += instance.OnUseHPpotion;
                @UseHPpotion.canceled += instance.OnUseHPpotion;
            }
        }
    }
    public HeroActions @Hero => new HeroActions(this);
    public interface IHeroActions
    {
        void OnMainMenu(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnUseHPpotion(InputAction.CallbackContext context);
    }
}
