// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Editor"",
            ""id"": ""0df1e9a3-8050-4eb5-8dbe-a5a32fe9c25b"",
            ""actions"": [
                {
                    ""name"": ""ShootLaser"",
                    ""type"": ""Button"",
                    ""id"": ""cde3c18b-f137-42ad-9053-d5fbe21429f8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveBlock"",
                    ""type"": ""Button"",
                    ""id"": ""ff57b791-7ca4-474d-8b26-375de90142ff"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""acd6a5e3-c039-41eb-9db0-8ca094ccb512"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootLaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""b39db462-b537-4bda-97c7-60e4578cf772"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4846c77e-3b90-403a-ba01-638f0561a912"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2c358da3-c7f5-48c9-8443-b24bff08ecef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""27ddaaab-68b9-43d6-927c-7353b2c228c5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6cad17ff-ac11-42e6-8709-3eea097a6155"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""2d84c17a-8701-4ec1-bf09-9776be899e02"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3b39d508-572d-4fbb-8b9a-4d9d6e60948f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""320d74d6-bade-4590-9952-fd5d9bb03463"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9694f275-3d05-4c2c-a163-bfdf2240224e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""775e7ae7-ecb9-433f-a14e-ac3993f77fb2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Editor
        m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
        m_Editor_ShootLaser = m_Editor.FindAction("ShootLaser", throwIfNotFound: true);
        m_Editor_MoveBlock = m_Editor.FindAction("MoveBlock", throwIfNotFound: true);
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

    // Editor
    private readonly InputActionMap m_Editor;
    private IEditorActions m_EditorActionsCallbackInterface;
    private readonly InputAction m_Editor_ShootLaser;
    private readonly InputAction m_Editor_MoveBlock;
    public struct EditorActions
    {
        private @InputMaster m_Wrapper;
        public EditorActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShootLaser => m_Wrapper.m_Editor_ShootLaser;
        public InputAction @MoveBlock => m_Wrapper.m_Editor_MoveBlock;
        public InputActionMap Get() { return m_Wrapper.m_Editor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
        public void SetCallbacks(IEditorActions instance)
        {
            if (m_Wrapper.m_EditorActionsCallbackInterface != null)
            {
                @ShootLaser.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnShootLaser;
                @ShootLaser.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnShootLaser;
                @ShootLaser.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnShootLaser;
                @MoveBlock.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnMoveBlock;
                @MoveBlock.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnMoveBlock;
                @MoveBlock.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnMoveBlock;
            }
            m_Wrapper.m_EditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ShootLaser.started += instance.OnShootLaser;
                @ShootLaser.performed += instance.OnShootLaser;
                @ShootLaser.canceled += instance.OnShootLaser;
                @MoveBlock.started += instance.OnMoveBlock;
                @MoveBlock.performed += instance.OnMoveBlock;
                @MoveBlock.canceled += instance.OnMoveBlock;
            }
        }
    }
    public EditorActions @Editor => new EditorActions(this);
    public interface IEditorActions
    {
        void OnShootLaser(InputAction.CallbackContext context);
        void OnMoveBlock(InputAction.CallbackContext context);
    }
}
