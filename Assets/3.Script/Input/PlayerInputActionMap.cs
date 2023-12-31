//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/3.Script/Input/PlayerInputActionMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActionMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActionMap"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""73a0fef7-a454-40d0-b324-04ca147959f5"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""8cfe8aef-ec69-44e4-a4fa-88a99acd62e4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftMouseButton"",
                    ""type"": ""Button"",
                    ""id"": ""30f7d773-96db-4526-b67e-21903013dc12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""a1e675ae-0b25-4103-b463-984e90560166"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenSkillSetPanel"",
                    ""type"": ""Button"",
                    ""id"": ""00658bf3-7432-4017-85fe-9da08ee144ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""M2Skill"",
                    ""type"": ""Button"",
                    ""id"": ""dd383475-62d5-47df-b806-03be0ad0ea54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num1Skill"",
                    ""type"": ""Button"",
                    ""id"": ""b8711034-0fbe-4738-bd8d-21e83c3fe567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num2Skill"",
                    ""type"": ""Button"",
                    ""id"": ""c96267fe-e8ff-49f6-a1f2-1fb470ba4225"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num3Skill"",
                    ""type"": ""Button"",
                    ""id"": ""9bd2b81d-b909-4df7-94d8-ed093b0ab442"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num4Skill"",
                    ""type"": ""Button"",
                    ""id"": ""97395b1a-a69c-4715-9c33-e1db2d253cc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""19141cd5-0cd1-456e-bbac-b20f6e6d6ced"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""0903a8ac-f047-41e2-b390-086b0850989e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerPortal"",
                    ""type"": ""Button"",
                    ""id"": ""89d0e1eb-2ca1-496b-8aa5-a53a01728608"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Status"",
                    ""type"": ""Button"",
                    ""id"": ""661e31ed-52e6-4162-ab72-e6c7b71fda23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ForcedMove"",
                    ""type"": ""Button"",
                    ""id"": ""0df13e4c-91ff-4fae-b00d-d56bb7318eb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""143c9b6a-74fa-4455-b27e-abfa9f1375e3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a26b219-582d-4855-ae67-23c8fb0c0569"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0988c076-3f9c-4216-bd23-1073902e3228"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4e91174-2650-48e1-a87c-89e15ceb7ff3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenSkillSetPanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98f064d0-c427-4e6f-87cd-e7567c4a3c36"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""M2Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8efe053-e304-490a-966b-22b0908d2f96"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num1Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a739649c-c068-442b-b4c2-92367e0ed11a"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num2Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a288c7c5-31dc-41d5-94e0-c120433fe1e6"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num3Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa2c9aaf-abb7-41db-8af0-9e0ca1e53076"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96401cbd-1813-4427-a039-716a3b361faa"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db7d84ae-7585-4b5b-82d3-8215e123953d"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerPortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b9b8bc1-0ec1-4f3c-83a9-4722aba1757c"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num4Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33e3bdcc-8e64-43f8-90d8-87121806a6ea"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Status"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15f13799-70ea-41b9-aeab-a0d437798031"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ForcedMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_LeftMouseButton = m_Player.FindAction("LeftMouseButton", throwIfNotFound: true);
        m_Player_OpenInventory = m_Player.FindAction("OpenInventory", throwIfNotFound: true);
        m_Player_OpenSkillSetPanel = m_Player.FindAction("OpenSkillSetPanel", throwIfNotFound: true);
        m_Player_M2Skill = m_Player.FindAction("M2Skill", throwIfNotFound: true);
        m_Player_Num1Skill = m_Player.FindAction("Num1Skill", throwIfNotFound: true);
        m_Player_Num2Skill = m_Player.FindAction("Num2Skill", throwIfNotFound: true);
        m_Player_Num3Skill = m_Player.FindAction("Num3Skill", throwIfNotFound: true);
        m_Player_Num4Skill = m_Player.FindAction("Num4Skill", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_Test = m_Player.FindAction("Test", throwIfNotFound: true);
        m_Player_PlayerPortal = m_Player.FindAction("PlayerPortal", throwIfNotFound: true);
        m_Player_Status = m_Player.FindAction("Status", throwIfNotFound: true);
        m_Player_ForcedMove = m_Player.FindAction("ForcedMove", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_LeftMouseButton;
    private readonly InputAction m_Player_OpenInventory;
    private readonly InputAction m_Player_OpenSkillSetPanel;
    private readonly InputAction m_Player_M2Skill;
    private readonly InputAction m_Player_Num1Skill;
    private readonly InputAction m_Player_Num2Skill;
    private readonly InputAction m_Player_Num3Skill;
    private readonly InputAction m_Player_Num4Skill;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_Test;
    private readonly InputAction m_Player_PlayerPortal;
    private readonly InputAction m_Player_Status;
    private readonly InputAction m_Player_ForcedMove;
    public struct PlayerActions
    {
        private @PlayerInputActionMap m_Wrapper;
        public PlayerActions(@PlayerInputActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @LeftMouseButton => m_Wrapper.m_Player_LeftMouseButton;
        public InputAction @OpenInventory => m_Wrapper.m_Player_OpenInventory;
        public InputAction @OpenSkillSetPanel => m_Wrapper.m_Player_OpenSkillSetPanel;
        public InputAction @M2Skill => m_Wrapper.m_Player_M2Skill;
        public InputAction @Num1Skill => m_Wrapper.m_Player_Num1Skill;
        public InputAction @Num2Skill => m_Wrapper.m_Player_Num2Skill;
        public InputAction @Num3Skill => m_Wrapper.m_Player_Num3Skill;
        public InputAction @Num4Skill => m_Wrapper.m_Player_Num4Skill;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @Test => m_Wrapper.m_Player_Test;
        public InputAction @PlayerPortal => m_Wrapper.m_Player_PlayerPortal;
        public InputAction @Status => m_Wrapper.m_Player_Status;
        public InputAction @ForcedMove => m_Wrapper.m_Player_ForcedMove;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @LeftMouseButton.started += instance.OnLeftMouseButton;
            @LeftMouseButton.performed += instance.OnLeftMouseButton;
            @LeftMouseButton.canceled += instance.OnLeftMouseButton;
            @OpenInventory.started += instance.OnOpenInventory;
            @OpenInventory.performed += instance.OnOpenInventory;
            @OpenInventory.canceled += instance.OnOpenInventory;
            @OpenSkillSetPanel.started += instance.OnOpenSkillSetPanel;
            @OpenSkillSetPanel.performed += instance.OnOpenSkillSetPanel;
            @OpenSkillSetPanel.canceled += instance.OnOpenSkillSetPanel;
            @M2Skill.started += instance.OnM2Skill;
            @M2Skill.performed += instance.OnM2Skill;
            @M2Skill.canceled += instance.OnM2Skill;
            @Num1Skill.started += instance.OnNum1Skill;
            @Num1Skill.performed += instance.OnNum1Skill;
            @Num1Skill.canceled += instance.OnNum1Skill;
            @Num2Skill.started += instance.OnNum2Skill;
            @Num2Skill.performed += instance.OnNum2Skill;
            @Num2Skill.canceled += instance.OnNum2Skill;
            @Num3Skill.started += instance.OnNum3Skill;
            @Num3Skill.performed += instance.OnNum3Skill;
            @Num3Skill.canceled += instance.OnNum3Skill;
            @Num4Skill.started += instance.OnNum4Skill;
            @Num4Skill.performed += instance.OnNum4Skill;
            @Num4Skill.canceled += instance.OnNum4Skill;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @Test.started += instance.OnTest;
            @Test.performed += instance.OnTest;
            @Test.canceled += instance.OnTest;
            @PlayerPortal.started += instance.OnPlayerPortal;
            @PlayerPortal.performed += instance.OnPlayerPortal;
            @PlayerPortal.canceled += instance.OnPlayerPortal;
            @Status.started += instance.OnStatus;
            @Status.performed += instance.OnStatus;
            @Status.canceled += instance.OnStatus;
            @ForcedMove.started += instance.OnForcedMove;
            @ForcedMove.performed += instance.OnForcedMove;
            @ForcedMove.canceled += instance.OnForcedMove;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @LeftMouseButton.started -= instance.OnLeftMouseButton;
            @LeftMouseButton.performed -= instance.OnLeftMouseButton;
            @LeftMouseButton.canceled -= instance.OnLeftMouseButton;
            @OpenInventory.started -= instance.OnOpenInventory;
            @OpenInventory.performed -= instance.OnOpenInventory;
            @OpenInventory.canceled -= instance.OnOpenInventory;
            @OpenSkillSetPanel.started -= instance.OnOpenSkillSetPanel;
            @OpenSkillSetPanel.performed -= instance.OnOpenSkillSetPanel;
            @OpenSkillSetPanel.canceled -= instance.OnOpenSkillSetPanel;
            @M2Skill.started -= instance.OnM2Skill;
            @M2Skill.performed -= instance.OnM2Skill;
            @M2Skill.canceled -= instance.OnM2Skill;
            @Num1Skill.started -= instance.OnNum1Skill;
            @Num1Skill.performed -= instance.OnNum1Skill;
            @Num1Skill.canceled -= instance.OnNum1Skill;
            @Num2Skill.started -= instance.OnNum2Skill;
            @Num2Skill.performed -= instance.OnNum2Skill;
            @Num2Skill.canceled -= instance.OnNum2Skill;
            @Num3Skill.started -= instance.OnNum3Skill;
            @Num3Skill.performed -= instance.OnNum3Skill;
            @Num3Skill.canceled -= instance.OnNum3Skill;
            @Num4Skill.started -= instance.OnNum4Skill;
            @Num4Skill.performed -= instance.OnNum4Skill;
            @Num4Skill.canceled -= instance.OnNum4Skill;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @Test.started -= instance.OnTest;
            @Test.performed -= instance.OnTest;
            @Test.canceled -= instance.OnTest;
            @PlayerPortal.started -= instance.OnPlayerPortal;
            @PlayerPortal.performed -= instance.OnPlayerPortal;
            @PlayerPortal.canceled -= instance.OnPlayerPortal;
            @Status.started -= instance.OnStatus;
            @Status.performed -= instance.OnStatus;
            @Status.canceled -= instance.OnStatus;
            @ForcedMove.started -= instance.OnForcedMove;
            @ForcedMove.performed -= instance.OnForcedMove;
            @ForcedMove.canceled -= instance.OnForcedMove;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnLeftMouseButton(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnOpenSkillSetPanel(InputAction.CallbackContext context);
        void OnM2Skill(InputAction.CallbackContext context);
        void OnNum1Skill(InputAction.CallbackContext context);
        void OnNum2Skill(InputAction.CallbackContext context);
        void OnNum3Skill(InputAction.CallbackContext context);
        void OnNum4Skill(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnPlayerPortal(InputAction.CallbackContext context);
        void OnStatus(InputAction.CallbackContext context);
        void OnForcedMove(InputAction.CallbackContext context);
    }
}
