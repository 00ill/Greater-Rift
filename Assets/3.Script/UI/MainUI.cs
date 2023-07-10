using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : UI_Scene
{
    enum Buttons
    {
        NewStartButton,
        LoadDataButton,
        OptionButton,
        QuitButton,
        CharacterConfirm,
        CharacterCancel,
        FirstDataDelete,
        SecondDataDelete,
        ThirdDataDelete,
        LoadStart,
        LoadCancel,
        LoginButton,
        SignUpButton,
        QuitGame,
        SignUpConfirmButton,
        SignUpCancelButton

    }
    enum Images
    {
    }
    enum Texts
    {
        TitleText,
        NewStartText,
        LoadDataText,
        OptionText,
        CharacterName,
        NameInputText,
        CharacterConfirmText,
        CharacterCancelText,
        WarningText,
        FirstDataName,
        FirstDataLevel,
        SecondDataName,
        SecondDataLevel,
        ThirdDataName,
        ThirdDataLevel,
        LoadWarningText,
        LoginIDText,
        LoginPWText,
        LoginWaringText,
        SignUpButtonText,
        LoginButtonText,
        QuitGameText,
        SignUpIDText,
        SignUpPWText,
        SignUpConfirmText,
        SignUpCancelText,
        SignUpWarningText
    }
    enum GameObjects
    {
        LoginPanel,
        SignUpPanel,
        MenuPanel,
        NewCharacter,
        LoadDataPanel,
        FirstData,
        SecondData,
        ThirdData
    }
    enum InputFields
    {
        NameInput,
        LoginIDInput,
        LoginPWInput,
        SignUpIDInput,
        SignUpPWInput
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TMP_InputField>(typeof(InputFields));


        InitLoginPanel();
        InitSignUpPanel();
        InitMenuPanel();
        InitNewCharacterPanel();
        InitLoadDataPanel();

        Managers.Event.DBEvent -= DB_Event;
        Managers.Event.DBEvent += DB_Event;
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (ActionQueue.Count > 0)
        {
            StartCoroutine(ProcessActionQueue());
        }
    }

    private void InitMenuPanel()
    {
        GetText((int)Texts.TitleText).text = $"Greater Rift";
        GetText((int)Texts.NewStartText).text = $"New Game";
        GetText((int)Texts.LoadDataText).text = $"Load Data";
        GetText((int)Texts.OptionText).text = $"Option";

        GetButton((int)Buttons.NewStartButton).gameObject.BindEvent((PointerEventData data) => NewStartEvent());
        GetButton((int)Buttons.LoadDataButton).gameObject.BindEvent((PointerEventData data) => LoadDataEvent());
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent((PointerEventData data) => OptionEvent());

        GetObject((int)GameObjects.MenuPanel).SetActive(false);
    }
    private void InitNewCharacterPanel()
    {
        GetText((int)Texts.CharacterName).text = $"Character Name";
        GetText((int)Texts.CharacterConfirmText).text = $"New Start";
        GetText((int)Texts.CharacterCancelText).text = $"Cancel";
        GetText((int)Texts.WarningText).text = "";

        GetButton((int)Buttons.CharacterConfirm).gameObject
            .BindEvent((PointerEventData data) => CharacterConfirm());
        GetButton((int)Buttons.CharacterCancel).gameObject
            .BindEvent((PointerEventData data) => CharacterCancel());
        GetObject((int)GameObjects.NewCharacter).SetActive(false);

    }
    private void InitLoadDataPanel()
    {
        GetText((int)Texts.LoadWarningText).text = "";
        GetText((int)Texts.FirstDataName).text = "No Data";
        GetText((int)Texts.FirstDataLevel).text = "";
        GetText((int)Texts.SecondDataName).text = "No Data";
        GetText((int)Texts.SecondDataLevel).text = "";
        GetText((int)Texts.ThirdDataName).text = "No Data";
        GetText((int)Texts.ThirdDataLevel).text = "";
        GetObject((int)GameObjects.FirstData).GetComponent<Outline>().enabled = false;
        GetObject((int)GameObjects.SecondData).GetComponent<Outline>().enabled = false;
        GetObject((int)GameObjects.ThirdData).GetComponent<Outline>().enabled = false;

        GetObject((int)GameObjects.FirstData).BindEvent((PointerEventData data) =>
            {
                HighlightData(GameObjects.FirstData);
                if (GetText((int)Texts.FirstDataName).text != "No Data")
                {
                    Managers.DB.CurrentDataSlot = "DataSlot1";
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot1Data;
                    Managers.DB.CurrentPlayerSkillData = Managers.DB.Slot1SkillData;
                    Managers.Inventory.Inventory = Managers.DB.Slot1InventoryData;
                    Managers.Inventory.Equipment = Managers.DB.Slot1EquipmantData;
                    GetText((int)Texts.LoadWarningText).text = "";
                }
                else
                {
                    Managers.DB.CurrentPlayerData = null;
                    Managers.DB.CurrentPlayerSkillData = null;
                    GetText((int)Texts.LoadWarningText).text = "Please Select Exist Data";
                }
            });
        GetObject((int)GameObjects.SecondData).BindEvent((PointerEventData data) =>
            {
                HighlightData(GameObjects.SecondData);
                if (GetText((int)Texts.SecondDataName).text != "No Data")
                {
                    Managers.DB.CurrentDataSlot = "DataSlot2";
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot2Data;
                    Managers.DB.CurrentPlayerSkillData = Managers.DB.Slot2SkillData;
                    Managers.Inventory.Inventory = Managers.DB.Slot2InventoryData;
                    Managers.Inventory.Equipment = Managers.DB.Slot2EquipmantData;

                    GetText((int)Texts.LoadWarningText).text = "";
                }
                else
                {
                    Managers.DB.CurrentPlayerData = null;
                    Managers.DB.CurrentPlayerSkillData = null;
                    GetText((int)Texts.LoadWarningText).text = "Please Select Exist Data";
                }

            });
        GetObject((int)GameObjects.ThirdData).BindEvent((PointerEventData data) =>
            {
                HighlightData(GameObjects.ThirdData);
                if (GetText((int)Texts.ThirdDataName).text != "No Data")
                {
                    Managers.DB.CurrentDataSlot = "DataSlot3";
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot3Data;
                    Managers.DB.CurrentPlayerSkillData = Managers.DB.Slot3SkillData;
                    Managers.Inventory.Inventory = Managers.DB.Slot3InventoryData;
                    Managers.Inventory.Equipment = Managers.DB.Slot3EquipmantData;

                    GetText((int)Texts.LoadWarningText).text = "";
                }
                else
                {
                    Managers.DB.CurrentPlayerData = null;
                    Managers.DB.CurrentPlayerSkillData = null;
                    GetText((int)Texts.LoadWarningText).text = "Please Select Exist Data";
                }

            });

        GetButton((int)Buttons.FirstDataDelete).gameObject.BindEvent((PointerEventData data) => { Managers.DB.DeletaData(1); });
        GetButton((int)Buttons.SecondDataDelete).gameObject.BindEvent((PointerEventData data) => { Managers.DB.DeletaData(2); });
        GetButton((int)Buttons.ThirdDataDelete).gameObject.BindEvent((PointerEventData data) => { Managers.DB.DeletaData(3); });
        GetButton((int)Buttons.LoadStart).gameObject.BindEvent((PointerEventData data) => LoadStart());
        GetButton((int)Buttons.LoadCancel).gameObject.BindEvent((PointerEventData data) => LoadCancel());

        GetObject((int)GameObjects.LoadDataPanel).SetActive(false);

    }
    private void InitLoginPanel()
    {
        GetText((int)Texts.LoginIDText).text = "ID";
        GetText((int)Texts.LoginPWText).text = "Password";
        GetText((int)Texts.LoginWaringText).text = "";
        GetText((int)Texts.LoginButtonText).text = "Login";
        GetText((int)Texts.SignUpButtonText).text = "Sign Up";
        GetText((int)Texts.QuitGameText).text = "QUIT";

        GetButton((int)Buttons.LoginButton).gameObject.BindEvent((PointerEventData data) => { Login(); });
        GetButton((int)Buttons.SignUpButton).gameObject.BindEvent((PointerEventData data) =>
        {
            GetObject((int)GameObjects.SignUpPanel).SetActive(true);
            GetObject((int)GameObjects.LoginPanel).SetActive(false);
        });
    }

    private void InitSignUpPanel()
    {
        GetText((int)Texts.SignUpIDText).text = "ID";
        GetText((int)Texts.SignUpPWText).text = "Password";
        GetText((int)Texts.SignUpConfirmText).text = "Confirm";
        GetText((int)Texts.SignUpCancelText).text = "Cancel";
        GetText((int)Texts.SignUpWarningText).text = "";

        GetButton((int)Buttons.SignUpConfirmButton).gameObject.BindEvent((PointerEventData data) => { CreateAccount(); });
        GetButton((int)Buttons.SignUpCancelButton).gameObject.BindEvent((PointerEventData data) =>
            {
                GetObject((int)GameObjects.SignUpPanel).SetActive(false);
                GetObject((int)GameObjects.LoginPanel).SetActive(true);
            });

        GetObject((int)GameObjects.SignUpPanel).SetActive(false);
    }

    private void OnDestroy()
    {
        Managers.Event.DBEvent -= DB_Event;
    }

    private void CreateAccount()
    {
        string id = Get<TMP_InputField>((int)InputFields.SignUpIDInput).text;
        string password = Get<TMP_InputField>((int)InputFields.SignUpPWInput).text;

        if (id == string.Empty)
        {
            GetText((int)Texts.SignUpWarningText).text = "Please enter your ID.";
            return;
        }
        if (password == string.Empty)
        {
            GetText((int)Texts.SignUpWarningText).text = "Please enter your Password.";
            return;
        }
        Managers.Game.PlayerLevel = 1;
        Managers.DB.CreateAccount(id, password);
    }

    private void Login()
    {
        GetText((int)Texts.LoginWaringText).text = "<color=white>Login....</color>";
        string id = Get<TMP_InputField>((int)InputFields.LoginIDInput).text;
        string password = Get<TMP_InputField>((int)InputFields.LoginPWInput).text;
        if (id == string.Empty)
        {
            GetText((int)Texts.LoginWaringText).text = "Please enter your ID";
            return;
        }
        if (password == string.Empty)
        {
            GetText((int)Texts.LoginWaringText).text = "Please enter your Password";
            return;
        }
        Managers.DB.Login(id, password);

    }
    private void NewStartEvent()
    {
        GetObject((int)GameObjects.MenuPanel).SetActive(false);
        GetObject((int)GameObjects.NewCharacter).SetActive(true);
    }

    private void LoadDataEvent()
    {
        //불러오기
        GetObject((int)GameObjects.MenuPanel).SetActive(false);
        GetObject((int)GameObjects.LoadDataPanel).SetActive(true);
    }

    private void OptionEvent()
    {
        //옵션 UI 출력
    }

    private void CharacterConfirm()
    {
        if (Get<TMP_InputField>((int)InputFields.NameInput).text != string.Empty)
        {
            Managers.DB.CreatePlayerData(GetText((int)Texts.NameInputText).text);
        }
        else
        {
            GetText((int)Texts.WarningText).text = "Please enter your name";
        }
    }
    private void CharacterCancel()
    {
        GetText((int)Texts.WarningText).text = "";
        GetObject((int)GameObjects.MenuPanel).SetActive(true);
        GetObject((int)GameObjects.NewCharacter).SetActive(false);
    }

    private void LoadStart()
    {
        if (Managers.DB.CurrentPlayerData == null)
        {
            GetText((int)Texts.LoadWarningText).text = "Please Select Data";
        }
        else
        {
            Managers.Game.PlayerLevel = Managers.DB.CurrentPlayerData.Level;
            GetText((int)Texts.LoadWarningText).text = "<color=white>Starting....</color>";
            Managers.DB.UpdateSkillData();
            Managers.Scene.LoadScene(Define.Scene.Town);
        }
    }
    private void LoadCancel()
    {
        GetObject((int)GameObjects.MenuPanel).SetActive(true);
        GetObject((int)GameObjects.LoadDataPanel).SetActive(false);
    }

    private void HighlightData(GameObjects dataObject)
    {
        List<GameObjects> list = new List<GameObjects>
        {
            GameObjects.FirstData,
            GameObjects.SecondData,
            GameObjects.ThirdData
        };

        foreach (GameObjects _object in list)
        {
            if (_object != dataObject)
            {
                GetObject((int)_object).GetComponent<Outline>().enabled = false;
            }
            else
            {
                GetObject((int)_object).GetComponent<Outline>().enabled = true;
            }
        }
    }
    private void UpdateLoadData()
    {
        if (Managers.DB.Slot1Data.Name != "Empty")
        {
            GetText((int)Texts.FirstDataName).text = Managers.DB.Slot1Data.Name;
            GetText((int)Texts.FirstDataLevel).text = "Level : " + Managers.DB.Slot1Data.Level.ToString();
        }
        else
        {
            GetText((int)Texts.FirstDataName).text = "No Data";
            GetText((int)Texts.FirstDataLevel).text = "";
        }
        if (Managers.DB.Slot2Data.Name != "Empty")
        {
            GetText((int)Texts.SecondDataName).text = Managers.DB.Slot2Data.Name;
            GetText((int)Texts.SecondDataLevel).text = "Level : " + Managers.DB.Slot2Data.Level.ToString();
        }
        else
        {
            GetText((int)Texts.SecondDataName).text = "No Data";
            GetText((int)Texts.SecondDataLevel).text = "";
        }
        if (Managers.DB.Slot3Data.Name != "Empty")
        {
            GetText((int)Texts.ThirdDataName).text = Managers.DB.Slot3Data.Name;
            GetText((int)Texts.ThirdDataLevel).text = "Level : " + Managers.DB.Slot3Data.Level.ToString();
        }
        else
        {
            GetText((int)Texts.ThirdDataName).text = "No Data";
            GetText((int)Texts.ThirdDataLevel).text = "";
        }
    }

    private Queue<(Action<Define.DB_Event>, Define.DB_Event)> ActionQueue = new Queue<(Action<Define.DB_Event>, Define.DB_Event)>();
    private void EnqueueAction(Action<Define.DB_Event> action, Define.DB_Event eventType)
    {
        ActionQueue.Enqueue((action, eventType));
    }

    private void DB_Event(Define.DB_Event eventType)
    {
        switch (eventType)
        {
            case Define.DB_Event.SuccessCreateNewPlayer:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.WarningText).text = "Creating Data...";
                        Managers.Scene.LoadScene(Define.Scene.Town);
                    }, eventType);
                    //새로운 캐릭터로 생성
                    break;
                }
            case Define.DB_Event.DuplicateID:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.SignUpWarningText).text = "Duplicate ID!";
                    }, eventType);
                    break;
                }
            case Define.DB_Event.SuccessCreateNewAccount:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.SignUpWarningText).text = "Account creation complete!";
                    }, eventType);
                    break;
                }
            case Define.DB_Event.NonExistID:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.LoginWaringText).text = "Non Exist ID";
                    }, eventType);
                    break;
                }
            case Define.DB_Event.WrongPassword:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.LoginWaringText).text = "Wrong Password";
                    }, eventType);
                    break;
                }
            case Define.DB_Event.SuccessLogin:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.LoginWaringText).text = "Success Login";
                        GetObject((int)GameObjects.LoginPanel).SetActive(false);
                        GetObject((int)GameObjects.MenuPanel).SetActive(true);
                        UpdateLoadData();

                    }, eventType);
                    break;
                }
            case Define.DB_Event.FullDataSlot:
                {
                    EnqueueAction(action =>
                    {
                        GetText((int)Texts.WarningText).text = "Data slot is full";
                    }, eventType);
                    break;
                }
            case Define.DB_Event.UpdateLoadData:
                {
                    EnqueueAction(action =>
                    {
                        UpdateLoadData();
                    }, eventType);
                    break;
                }
        }
    }

    private IEnumerator ProcessActionQueue()
    {
        while (ActionQueue.Count > 0)
        {
            (Action<Define.DB_Event> action, Define.DB_Event eventType) = ActionQueue.Dequeue();
            action?.Invoke(eventType);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
