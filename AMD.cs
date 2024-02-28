using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using Comfort.Common;
using EFT.InventoryLogic;
using EFT.UI.Ragfair;
using EFT.Ballistics;
using System.Collections;
using EFT.UI;
using EFT.NextObservedPlayer;
using BSG.CameraEffects;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;
using System.Reflection;
using EFT.Interactive;
using EFT.CameraControl;
using System.ComponentModel;
using Diz.LanguageExtensions;
using Diz.Skinning;
using static CC_Vintage;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using UnityStandardAssets.ImageEffects;
using GPUInstancer;
using EFT.Visual;
using static VisorEffect;
using System.Runtime.CompilerServices;

namespace NVIDIA
{
    public class AMD : MonoBehaviour
    {
        void OnGUI()
        {
            FakeOnGUI();
        }

        private void Start()
        {
            FakeStart();
        }

        private void Update()
        {
            FakeUpdate();
        }

        private void FixedUpdate()
        {
            FakeFixedUpdate();
        }

        private void Awake()
        {
            FakeAwake();
        }

        void FakeFixedUpdate()
        {

        }

        private void FakeOnGUI()
        {
            //GUI.Label(new Rect(10f, 10f, 200f, 100f), "DONNYCODES");

            //if (Globals.GameWorld)
            //    GUI.Label(new Rect(10f, 15f, 200f, 100f), "GameWorld Found!");

            //if (Globals.LocalPlayer)
            //    GUI.Label(new Rect(10f, 30f, 200f, 100f), $"Team ID {Globals.LocalPlayer.TeamId}");

            //if (Globals.MainCamera)
            //    GUI.Label(new Rect(10f, 45f, 200f, 100f), "MainCamera Found!");

            //if (Globals.Offline)
            //GUI.Label(new Rect(10f, 60f, 200f, 100f), "Offline Game!");

            //if (!Globals.Container.IsNullOrEmpty())
            //    GUI.Label(new Rect(10f, 30f, 200f, 100f), $"Containers In List {Globals.Container.Count}");

            //if (!Globals.GroundItem.IsNullOrEmpty())
            //    GUI.Label(new Rect(10f, 40f, 200f, 100f), $"GroundItems In List {Globals.GroundItem.Count}");

            if (Globals.IsMenuOpen)
            {
                GUI.Box(new Rect(100f, 50f, 400f, 800f), "");

                if (GUI.Button(new Rect(105f, 55f, 70f, 25f), "Aimbot"))
                    MenuConfig.MenuIndex = 0;

                if (GUI.Button(new Rect(180f, 55f, 70f, 25f), "Visuals"))
                    MenuConfig.MenuIndex = 1;

                if (GUI.Button(new Rect(255f, 55f, 70f, 25f), "Misc"))
                    MenuConfig.MenuIndex = 2;

                MenuConfig.InTeam = GUI.Toggle(new Rect(330f, 55f, 175f, 25f), MenuConfig.InTeam, "Enable Team Check");

                GUILayout.BeginArea(new Rect(105f, 85f, 400f, 800f));

                switch (MenuConfig.MenuIndex)
                {
                    case 0:
                        MenuConfig.EnableAimbot = GUILayout.Toggle(MenuConfig.EnableAimbot, "Aimbot");
                        MenuConfig.EnableAimbotPred = GUILayout.Toggle(MenuConfig.EnableAimbotPred, "Prediction");
                        MenuConfig.EnableFOVCircle = GUILayout.Toggle(MenuConfig.EnableFOVCircle, "Draw FOV Circle");
                        GUILayout.Label(" ");

                        //GUILayout.Label(" ");
                        //MenuConfig.EnableSilentAim = GUILayout.Toggle(MenuConfig.EnableSilentAim, "Silent Aim");
                        //GUILayout.Label($"Aimbot Hitchance {MenuConfig.AimbotHitchance}");
                        //MenuConfig.AimbotHitchance = GUILayout.HorizontalScrollbar(MenuConfig.AimbotHitchance, 1f, 1f, 100f, GUILayout.MaxWidth(200f));
                        //GUILayout.Label(" ");

                        MenuConfig.EnableOnlyWhenAimbotting = GUILayout.Toggle(MenuConfig.EnableOnlyWhenAimbotting, "Only When Aimbotting");
                        MenuConfig.EnableNoRecoil = GUILayout.Toggle(MenuConfig.EnableNoRecoil, "No Recoil");
                        MenuConfig.EnableNoSway = GUILayout.Toggle(MenuConfig.EnableNoSway, "No Sway");
                        GUILayout.Label(" ");

                        GUILayout.Label($"Aimbot FOV {MenuConfig.AimbotFOV}");
                        MenuConfig.AimbotFOV = GUILayout.HorizontalScrollbar(MenuConfig.AimbotFOV, 1f, 0.1f, 20f, GUILayout.MaxWidth(200f));
                        GUILayout.Label($"Aimbot Max Distance {MenuConfig.AimbotMaxDistance}");
                        MenuConfig.AimbotMaxDistance = GUILayout.HorizontalScrollbar(MenuConfig.AimbotMaxDistance, 1f, 0.1f, 1500f, GUILayout.MaxWidth(200f));

                        GUILayout.Label($"Aimbot Key: {Globals.KeyNames[MenuConfig.AimbotKeyIndex]}");

                        if (GUILayout.Button("Q", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 0;
                        if (GUILayout.Button("E", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 1;
                        if (GUILayout.Button("LALT", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 2;
                        if (GUILayout.Button("M5", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 3;
                        if (GUILayout.Button("M4", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 4;
                        if (GUILayout.Button("RMB", GUILayout.MaxWidth(75f)))
                            MenuConfig.AimbotKeyIndex = 5;
                        break;
                    case 1:
                        MenuConfig.EnableESP = GUILayout.Toggle(MenuConfig.EnableESP, "Enable ESP");
                        MenuConfig.EnableWeapon = GUILayout.Toggle(MenuConfig.EnableWeapon, "Weapon ESP");
                        MenuConfig.EnableName = GUILayout.Toggle(MenuConfig.EnableName, "Name ESP");
                        MenuConfig.EnableAimLines = GUILayout.Toggle(MenuConfig.EnableAimLines, "Aim Line ESP");
                        MenuConfig.EnableSkeleton = GUILayout.Toggle(MenuConfig.EnableSkeleton, "Skeleton ESP");

                        GUI.color = new Color(MenuConfig.ESPClrR / 255f, MenuConfig.ESPClrG / 255f, MenuConfig.ESPClrB / 255f);
                        GUILayout.Label($"ESP Color");
                        GUI.color = Color.white;
                        MenuConfig.ESPClrR = GUILayout.HorizontalScrollbar(MenuConfig.ESPClrR, 1f, 0f, 255f, GUILayout.MaxWidth(200f));
                        MenuConfig.ESPClrG = GUILayout.HorizontalScrollbar(MenuConfig.ESPClrG, 1f, 0f, 255f, GUILayout.MaxWidth(200f));
                        MenuConfig.ESPClrB = GUILayout.HorizontalScrollbar(MenuConfig.ESPClrB, 1f, 0f, 255f, GUILayout.MaxWidth(200f));

                        GUILayout.Label($"Scav Render Distance {Math.Round(MenuConfig.ScavDistance)}");
                        MenuConfig.ScavDistance = GUILayout.HorizontalScrollbar(MenuConfig.ScavDistance, 1f, 5f, 1500f, GUILayout.MaxWidth(300f));

                        MenuConfig.EnableGrenadeESP = GUILayout.Toggle(MenuConfig.EnableGrenadeESP, "Grenade ESP");

                        GUILayout.Label(" ");

                        MenuConfig.EnableChams = GUILayout.Toggle(MenuConfig.EnableChams, "Chams");
                        MenuConfig.DisableModelOcclusion = GUILayout.Toggle(MenuConfig.DisableModelOcclusion, "Disable Model Occlusion");

                        GUI.color = new Color(MenuConfig.VisR / 255f, MenuConfig.VisG / 255f, MenuConfig.VisB / 255f);
                        GUILayout.Label("Chams Visible");
                        GUI.color = Color.white;
                        MenuConfig.VisR = GUILayout.HorizontalScrollbar(MenuConfig.VisR, 1f, 255f, 1f, GUILayout.MaxWidth(200f));
                        MenuConfig.VisG = GUILayout.HorizontalScrollbar(MenuConfig.VisG, 1f, 255f, 1f, GUILayout.MaxWidth(200f));
                        MenuConfig.VisB = GUILayout.HorizontalScrollbar(MenuConfig.VisB, 1f, 255f, 1f, GUILayout.MaxWidth(200f));

                        GUI.color = new Color(MenuConfig.HidR / 255f, MenuConfig.HidG / 255f, MenuConfig.HidB / 255f);
                        GUILayout.Label("Chams Hidden");
                        GUI.color = Color.white;
                        MenuConfig.HidR = GUILayout.HorizontalScrollbar(MenuConfig.HidR, 1f, 0f, 255f, GUILayout.MaxWidth(200f));
                        MenuConfig.HidG = GUILayout.HorizontalScrollbar(MenuConfig.HidG, 1f, 0f, 255f, GUILayout.MaxWidth(200f));
                        MenuConfig.HidB = GUILayout.HorizontalScrollbar(MenuConfig.HidB, 1f, 0f, 255f, GUILayout.MaxWidth(200f));

                        GUILayout.Label(" ");

                        MenuConfig.EnableItemESP = GUILayout.Toggle(MenuConfig.EnableItemESP, "Item ESP (F2)");
                        MenuConfig.EnableContainerESP = GUILayout.Toggle(MenuConfig.EnableContainerESP, "Container ESP (F3)");
                        MenuConfig.EnableQuestESP = GUILayout.Toggle(MenuConfig.EnableQuestESP, "Include Quests In ESP");
                        MenuConfig.ShowKappaItemsInGreen = GUILayout.Toggle(MenuConfig.ShowKappaItemsInGreen, "Highlight Kappa Items");

                        GUILayout.Label($"Vaulable Distance {Math.Round(MenuConfig.VaulableItemsDistance)}");
                        MenuConfig.VaulableItemsDistance = GUILayout.HorizontalScrollbar(MenuConfig.VaulableItemsDistance, 1f, 0.5f, 1500f, GUILayout.MaxWidth(300f));
                        GUILayout.Label($"Quest Distance {Math.Round(MenuConfig.QuestItemsDistance)}");
                        MenuConfig.QuestItemsDistance = GUILayout.HorizontalScrollbar(MenuConfig.QuestItemsDistance, 1f, 0.5f, 1500f, GUILayout.MaxWidth(300f));
                        GUILayout.Label($"Regular Distance {Math.Round(MenuConfig.RegularItemsDistance)}");
                        MenuConfig.RegularItemsDistance = GUILayout.HorizontalScrollbar(MenuConfig.RegularItemsDistance, 1f, 0.5f, 1500f, GUILayout.MaxWidth(300f));

                        Globals.ItemToSearch = GUILayout.TextField(Globals.ItemToSearch, 256, GUILayout.MaxWidth(350f));
                        break;
                    case 2:
                        MenuConfig.ThermalVision = GUILayout.Toggle(MenuConfig.ThermalVision, "Thermal Vision");
                        MenuConfig.NightVision = GUILayout.Toggle(MenuConfig.NightVision, "Night Vision");
                        MenuConfig.UnlockHiddenQuestItems = GUILayout.Toggle(MenuConfig.UnlockHiddenQuestItems, "Unlock Hidden Quest Items");
                        MenuConfig.AmmoIndicator = GUILayout.Toggle(MenuConfig.AmmoIndicator, "Ammo Indicator");
                        MenuConfig.Crosshair = GUILayout.Toggle(MenuConfig.Crosshair, "Crosshair");

                        GUILayout.Label(" ");
                        MenuConfig.LootThruWalls = GUILayout.Toggle(MenuConfig.LootThruWalls, "Loot Thru Walls (F4)");
                        GUILayout.Label($"Dist Factor {MenuConfig.ThruDist}");
                        MenuConfig.ThruDist = GUILayout.HorizontalScrollbar(MenuConfig.ThruDist, 0.2f, 3f, 4f, GUILayout.MaxWidth(300f));
                        GUILayout.Label(" ");

                        GUILayout.Label(" ");
                        MenuConfig.ExtendedLean = GUILayout.Toggle(MenuConfig.ExtendedLean, "Extended Lean (< | >)");
                        GUILayout.Label($"Lean Factor {MenuConfig.LeanFactor}");
                        MenuConfig.LeanFactor = GUILayout.HorizontalScrollbar(MenuConfig.LeanFactor, 0.5f, 0.1f, 1.2f, GUILayout.MaxWidth(300f));
                        GUILayout.Label(" ");
                        break;
                }

                GUILayout.EndArea();
            }

            if (Event.current.type == EventType.Repaint)
            {
                if (Globals.IsFarLootToggled && MenuConfig.LootThruWalls)
                    Renderer.DrawString(new Vector2(Screen.width / 2f, Screen.height / 2f + 170f), "FAR LOOT", Color.green);

                if (Globals.IsExtendedLeanToggled && MenuConfig.ExtendedLean)
                    Renderer.DrawString(new Vector2(Screen.width / 2f, Screen.height / 2f + 190f), "LEAN", Color.magenta);

                if (MenuConfig.EnableESP)
                    ESP();

                if (MenuConfig.EnableContainerESP && Input.GetKey(KeyCode.F3))
                    ContainerESP();

                if (MenuConfig.EnableItemESP && Input.GetKey(KeyCode.F2))
                    ItemESP();

                if (MenuConfig.EnableGrenadeESP)
                    GrenadeESP();

                if (MenuConfig.Crosshair)
                {
                    Vector3 EndPos = Globals.W2S(Globals.BarrelRaycast());

                    if (EndPos != Vector3.zero)
                        Renderer.DrawDot(new Vector2(EndPos.x, EndPos.y), Color.red);          
                }
            }
        }

        private void FakeStart()
        {
            Globals.Bundle = AssetBundle.LoadFromMemory(System.IO.File.ReadAllBytes("C:\\Data\\assets.assets"));

            UnityEngine.Scripting.GarbageCollector.GCMode = UnityEngine.Scripting.GarbageCollector.Mode.Enabled;
        }

        private void FakeUpdate()
        {
            float LastCacheTime = 0f;
            float LastSlowCacheTime = 0f;

            // MENU SHIT
            if (Input.GetKeyUp(KeyCode.Insert))
                Globals.IsMenuOpen = !Globals.IsMenuOpen;

            if (Globals.IsMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            // MENU SHIT

            // MAIN CACHE LOOP 0.25 SECOND UPD TIME
            if (Time.time >= LastCacheTime)
            {
                if (Camera.main != null)
                    Globals.MainCamera = Camera.main;

                if (Singleton<GameWorld>.Instance != null)
                    Globals.GameWorld = Singleton<GameWorld>.Instance;

                if (Globals.LocalPlayer != null)
                    if (Globals.LocalPlayer?.HandsController?.Item is Weapon)
                        Globals.LocalPlayerWeapon = (Weapon)Globals.LocalPlayer?.HandsController?.Item;

                //if (Camera.main != null)
                    //Globals.OpticCamera = Camera.allCameras.FirstOrDefault(c => c.name == "BaseOpticCamera(Clone)");

                if (Globals.GameWorld != null && Globals.GameWorld.RegisteredPlayers != null)
                {
                    List<IPlayer> RegisteredPlayers = Globals.GameWorld.RegisteredPlayers;

                    Globals.Players.Clear();
                    Globals.Grenades.Clear();

                    foreach (var player in RegisteredPlayers)
                    {
                        if (player == null)
                            continue;

                        if (player.IsYourPlayer)
                            Globals.LocalPlayer = player as Player;

                        Globals.Players.Add(player);
                    }

                    for (int i = 0; i < Globals.GameWorld.Grenades.Count; i++)
                    {
                        Throwable Throwables = Globals.GameWorld.Grenades.GetByIndex(i);
                        if (Throwables == null)
                            continue;

                        Globals.Grenades.Add(Throwables);
                    }
                }
                else
                {
                    Globals.Players.Clear();
                    Globals.Grenades.Clear();
                }

                LastCacheTime = Time.time + 0.25f;
            }
            // MAIN CACHE LOOP

            // SLOW UPDATE LOOP UPD EVERY 1 SECONDS
            if (Time.time >= LastSlowCacheTime)
            {
                Globals.PreloaderUI = MonoBehaviourSingleton<EFT.UI.PreloaderUI>.Instance;
                Globals.PreloaderUI.SetSessionId("DonHack");

                //if (Globals.MainCamera != null)
                //Globals.GetScopeParameters(Globals.MainCamera, );

                Globals.Containers.Clear();
                Globals.GroundItems.Clear();

                if (Globals.GameWorld != null)
                {
                    foreach (LootableContainer Container in LocationScene.GetAllObjects<LootableContainer>(false))
                    {
                        if (Container == null)
                            continue;

                        Globals.Containers.Add(Container);
                    }

                    for (int i = 0; i < Globals.GameWorld.LootItems.Count; i++)
                    {
                        LootItem Item = Globals.GameWorld.LootItems.GetByIndex(i);

                        if (Item == null)
                            continue;

                        if (!(Item.Item.TemplateId == "55d7217a4bdc2d86028b456d".Localized()))
                            Globals.GroundItems.Add(Item);
                    }
                }
                else
                {
                    Globals.Containers.Clear();
                    Globals.GroundItems.Clear();
                }

                LastSlowCacheTime = Time.time + 1f;
            }
            // SLOW UPDATE LOOP

            Misc();
            AimFeatures();
        }

        private void FakeAwake()
        {
            DontDestroyOnLoad(this);
        }

        private void Misc()
        {
            if (Camera.main != null && Globals.MainCamera != null)
            {
                // still dk if it does anything but couldnt hurt
                Camera.main.hideFlags = HideFlags.HideAndDontSave;

                Globals.MainCamera.GetComponent<NightVision>().SetPrivateField("_on", MenuConfig.NightVision);

                // CUSTOM THERMAL
                Globals.MainCamera.GetComponent<ThermalVision>().On = MenuConfig.ThermalVision;

                if (MenuConfig.ThermalVision)
                {
                    Globals.MainCamera.GetComponent<ThermalVision>().ChromaticAberrationThermalShift = 0f;
                    Globals.MainCamera.GetComponent<ThermalVision>().IsFpsStuck = false;
                    Globals.MainCamera.GetComponent<ThermalVision>().IsGlitch = false;
                    Globals.MainCamera.GetComponent<ThermalVision>().IsMotionBlurred = false;
                    Globals.MainCamera.GetComponent<ThermalVision>().IsNoisy = false;
                    Globals.MainCamera.GetComponent<ThermalVision>().IsPixelated = false;
                }
            }

            // YEAH
            if (Input.GetKeyUp(KeyCode.F4))
                Globals.IsFarLootToggled = !Globals.IsFarLootToggled;

            if (Globals.LocalPlayer != null && Globals.LocalPlayer.ProceduralWeaponAnimation != null)
            {
                if (MenuConfig.LootThruWalls)
                {
                    // IDK MAYBE UD METHODZZZ
                    Globals.LocalPlayer.ProceduralWeaponAnimation.hideFlags = HideFlags.HideAndDontSave;

                    Globals.LocalPlayer.ProceduralWeaponAnimation._fovCompensatoryDistance = Globals.IsFarLootToggled ? MenuConfig.ThruDist : 0.0189f;
                }
                else
                    Globals.LocalPlayer.ProceduralWeaponAnimation._fovCompensatoryDistance = 0.0189f;

                if (MenuConfig.ExtendedLean)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) 
                    {
                        Globals.IsExtendedLeanToggled = true;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition - new Vector3(MenuConfig.LeanFactor, 0f, 0f);
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        Globals.IsExtendedLeanToggled = false;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition + new Vector3(MenuConfig.LeanFactor, 0f, 0f);
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Globals.IsExtendedLeanToggled = true;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition + new Vector3(MenuConfig.LeanFactor, 0f, 0f);
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        Globals.IsExtendedLeanToggled = false;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition - new Vector3(MenuConfig.LeanFactor, 0f, 0f);
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Globals.IsExtendedLeanToggled = true;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition + new Vector3(0f, MenuConfig.LeanFactor, 0f);
                    }
                    if (Input.GetKeyUp(KeyCode.UpArrow))
                    {
                        Globals.IsExtendedLeanToggled = false;
                        Globals.LocalPlayer.WeaponRoot.localPosition = Globals.LocalPlayer.WeaponRoot.localPosition - new Vector3(0f, MenuConfig.LeanFactor, 0f);
                    }
                }
            }
        }

        public static float BulletDrop(Vector3 startVector, Vector3 endVector, float BulletSpeed)
        {
            float Distance = Vector3.Distance(startVector, endVector);
            if (Distance >= 50f)
            {
                float TravelTime = Distance / BulletSpeed;
                return (float)(4.905 * TravelTime * TravelTime);
            }
            return 0f;
        }

        private void AimFeatures()
        {
            if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.MainCamera == null || Globals.Players.IsNullOrEmpty())
                return;

            KeyCode[] AimKey = { KeyCode.Q, KeyCode.E, KeyCode.LeftAlt, KeyCode.Mouse4, KeyCode.Mouse3, KeyCode.Mouse1 };

            bool DoEffects = MenuConfig.EnableOnlyWhenAimbotting ? Input.GetKey(AimKey[MenuConfig.AimbotKeyIndex]) : true;

            // IDK MAYBE UD METHODZZZ
            Globals.LocalPlayer.ProceduralWeaponAnimation.hideFlags = HideFlags.HideAndDontSave;

            if (DoEffects)
            {
                if (MenuConfig.EnableNoRecoil)
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.NewShotRecoil.RecoilEffectOn = false;
                else
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.NewShotRecoil.RecoilEffectOn = true;

                if (MenuConfig.EnableNoSway)
                {
                    Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0f;

                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.x = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.y = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.z = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.x = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.y = 0f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.z = 0f;
                }
                else
                {
                    Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 1f;
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 1f;
                }
            }
            else
            {
                Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.NewShotRecoil.RecoilEffectOn = true;
                Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 1f;
                Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 1f;
            }

            if (Input.GetKey(AimKey[MenuConfig.AimbotKeyIndex]) && MenuConfig.EnableAimbot)
            {
                float DistanceToTarget = 9999f;
                Vector3 AimbotTargetPos = Vector3.zero;

                for (int i = 0; i < Globals.Players.Count(); i++)
                {
                    IPlayer _Player = Globals.Players.ElementAt(i);

                    if (_Player == null)
                        continue;

                    if (_Player.GetType() != typeof(ObservedPlayerView))
                        continue;

                    ObservedPlayerView Player = _Player as ObservedPlayerView;
                    if (Player == null)
                        continue;

                    if (Player.TeamId == Globals.LocalPlayer.TeamId && MenuConfig.InTeam)
                        continue;

                    Vector3 AimPos = Player.PlayerBones.Neck.position;

                    float Distance = Vector3.Distance(Globals.MainCamera.transform.position, AimPos);
                    if (Distance > MenuConfig.AimbotMaxDistance)
                        continue;

                    if (AimPos != Vector3.zero && Globals.CalcFov(AimPos) <= MenuConfig.AimbotFOV)
                    {
                        if (DistanceToTarget > Distance)
                        {
                            DistanceToTarget = Distance;

                            float TravelTime = Distance / Globals.LocalPlayerWeapon.CurrentAmmoTemplate.InitialSpeed;

                            if (MenuConfig.EnableAimbotPred)
                            {
                                if (Player.Velocity.magnitude > 0.01f)
                                    AimPos += Player.Velocity * TravelTime;

                                if (Globals.LocalPlayer.Velocity.magnitude > 0.01f)
                                    AimPos -= Globals.LocalPlayer.Velocity * Time.deltaTime;

                                if (Distance > 100f)
                                    AimPos = AimPos + Vector3.up * BulletDrop(Globals.LocalPlayer.Fireport.position, AimPos, Globals.LocalPlayerWeapon.CurrentAmmoTemplate.InitialSpeed);
                            }

                            AimbotTargetPos = AimPos;
                        }
                    }
                }

                if (AimbotTargetPos != Vector3.zero)
                    Globals.AimAtPos(AimbotTargetPos);
            }
        }

        private void ItemESP()
        {
            if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.MainCamera == null || Globals.GroundItems.IsNullOrEmpty())
                return;

            for (int i = 0; i < Globals.GroundItems.Count(); i++)
            {
                LootItem Loot = Globals.GroundItems.ElementAt(i);

                if (Loot.Item == null)
                    continue;


                if (Loot.Item.QuestItem && MenuConfig.UnlockHiddenQuestItems)
                    Loot.gameObject.SetActive(true);

                Vector3 GroundItemsPos = Globals.W2S(Loot.transform.position);

                if (GroundItemsPos == Vector3.zero || !Globals.IsOnScreen(GroundItemsPos))
                    continue;

                float Distance2Loot = Vector3.Distance(Globals.MainCamera.transform.position, Loot.transform.position);

                if (Loot.Item.LocalizedName() == Globals.ItemToSearch)
                    Renderer.DrawString(new Vector2(GroundItemsPos.x, GroundItemsPos.y), Loot.Item.LocalizedName() + " " + Math.Round(Distance2Loot).ToString() + "M", Color.green);

                // DRAW BIG MONEYYYY
                if (Loot.Item.LocalizedName() != Globals.ItemToSearch)
                {
                    if (Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Green)" || Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Red)"
                        || Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Blue)" || Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Violet)"
                        || Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Yellow)" || Loot.Item.LocalizedName() == "TerraGroup Labs keycard (Black)"
                        || Loot.Item.LocalizedName() == "UVSR Taiga-1 survival machete" || Loot.Item.LocalizedName() == "Red Rebel ice pick"
                        || Loot.Item.LocalizedName() == "Dorm room 314 marked key" || Loot.Item.LocalizedName() == "Chekannaya 15 apartment key"
                        || Loot.Item.LocalizedName() == "Graphics card" || Loot.Item.LocalizedName() == "Physical Bitcoin"
                        || Loot.Item.LocalizedName() == "Intelligence folder" || Loot.Item.LocalizedName() == "LEDX Skin Transilluminator"
                        || Loot.Item.LocalizedName() == "TerraGroup Labs access keycard" || Loot.Item.LocalizedName() == "Bottle of Fierce Hatchling moonshine"
                        || Loot.Item.LocalizedName() == "Portable defibrillator" || Loot.Item.LocalizedName() == "RB-PKPM marked key"
                        || Loot.Item.LocalizedName() == "Tetriz portable game console" || Loot.Item.LocalizedName() == "Bronze lion figurine"
                        || Loot.Item.LocalizedName() == "Virtex programmable processor" || Loot.Item.LocalizedName() == "Military power filter"
                        || Loot.Item.LocalizedName() == "VPX Flash Storage Module" || Loot.Item.LocalizedName() == "Relaxation room key"
                        || Loot.Item.LocalizedName() == "Phased array element" || Loot.Item.LocalizedName() == "Military COFDM Wireless Signal Transmitter"
                        || Loot.Item.LocalizedName() == "Can of thermite" || Loot.Item.LocalizedName() == "Gold skull ring"
                        || Loot.Item.LocalizedName() == "Golden Star balm" || Loot.Item.LocalizedName() == "Chain with Prokill medallion"
                        || Loot.Item.LocalizedName() == "GreenBat lithium battery" || Loot.Item.LocalizedName() == "Roler Submariner gold wrist watch"
                        || Loot.Item.LocalizedName() == "Ophthalmoscope" || Loot.Item.LocalizedName() == "Iridium military thermal vision module"
                        || Loot.Item.LocalizedName() == "Car dealership closed section key" || Loot.Item.LocalizedName() == "RB-BK marked key"
                        || Loot.Item.LocalizedName() == "RB-VO marked key" || Loot.Item.LocalizedName() == "Keycard with a blue marking"
                        || Loot.Item.LocalizedName() == "Mysterious room marked key" || Loot.Item.LocalizedName() == "Abandoned factory marked key"
                        || Loot.Item.LocalizedName() == "Health Resort west wing room 216 key" || Loot.Item.LocalizedName() == "Cottage back door key"
                        || Loot.Item.LocalizedName() == "ULTRA medical storage key" || Loot.Item.LocalizedName() == "Kiba Arms outer door key"
                        || Loot.Item.LocalizedName() == "Health Resort office key with a blue tape" || Loot.Item.LocalizedName() == "RB-PKPM marked key"
                        || Loot.Item.LocalizedName() == "Health Resort west wing room 301 key" || Loot.Item.LocalizedName() == "Health Resort east wing room 226 key"
                        || Loot.Item.LocalizedName() == "Health Resort west wing room 218 key" || Loot.Item.LocalizedName() == "TerraGroup Labs weapon testing area key"
                        || Loot.Item.LocalizedName() == "Shared bedroom marked key" || Loot.Item.LocalizedName() == "EMERCOM medical unit key"
                        || Loot.Item.LocalizedName() == "Factory emergency exit key" || Loot.Item.LocalizedName() == "Relaxation room key")
                    {
                        if (!Loot.Item.QuestItem && Distance2Loot < MenuConfig.VaulableItemsDistance)
                            Renderer.DrawString(new Vector2(GroundItemsPos.x, GroundItemsPos.y), Loot.Item.LocalizedShortName() + " " + Math.Round(Distance2Loot).ToString() + "M", Color.red);
                    }
                    else if (MenuConfig.ShowKappaItemsInGreen)
                    {
                        if (Loot.Item.LocalizedName() == "Old firesteel" || Loot.Item.LocalizedName() == "Antique axe" ||
                        Loot.Item.LocalizedName() == "Battered antique book" || Loot.Item.LocalizedName() == "FireKlean gun lube" ||
                        Loot.Item.LocalizedName() == "Golden rooster figurine" || Loot.Item.LocalizedName() == "Silver Badge" ||
                        Loot.Item.LocalizedName() == "Deadlyslob's beard oil" || Loot.Item.LocalizedName() == "Golden 1GPhone smartphone" ||
                        Loot.Item.LocalizedName() == "Jar of DevilDog mayo" || Loot.Item.LocalizedName() == "Can of sprats" ||
                        Loot.Item.LocalizedName() == "Fake mustache" || Loot.Item.LocalizedName() == "Kotton beanie" ||
                        Loot.Item.LocalizedName() == "Can of Dr. Lupo's coffee beans" || Loot.Item.LocalizedName() == "Pestily plague mask" ||
                        Loot.Item.LocalizedName() == "Raven figurine" || Loot.Item.LocalizedName() == "Shroud half-mask" ||
                        Loot.Item.LocalizedName() == "Veritas guitar pick" || Loot.Item.LocalizedName() == "42 Signature Blend English Tea" ||
                        Loot.Item.LocalizedName() == "Smoke balaclava" || Loot.Item.LocalizedName() == "Evasion armband" ||
                        Loot.Item.LocalizedName() == "Can of RatCola soda" || Loot.Item.LocalizedName() == "Loot Lord plushie" ||
                        Loot.Item.LocalizedName() == "WZ Wallet" || Loot.Item.LocalizedName() == "LVNDMARK's rat poison" ||
                        Loot.Item.LocalizedName() == "Missam forklift key" || Loot.Item.LocalizedName() == "Video cassette with the Cyborg Killer movie" ||
                        Loot.Item.LocalizedName() == "BakeEzy cook book" || Loot.Item.LocalizedName() == "JohnB Liquid DNB glasses" ||
                        Loot.Item.LocalizedName() == "Glorious E lightweight armored mask" || Loot.Item.LocalizedName() == "Baddie's red beard" ||
                        Loot.Item.LocalizedName() == "DRD body armor" || Loot.Item.LocalizedName() == "Gingy keychain" ||
                        Loot.Item.LocalizedName() == "Golden egg" || Loot.Item.LocalizedName() == "Press pass (issued for NoiceGuy)" ||
                        Loot.Item.LocalizedName() == "Axel parrot figurine" || Loot.Item.LocalizedName() == "BEAR Buddy plush toy")
                            Renderer.DrawString(new Vector2(GroundItemsPos.x, GroundItemsPos.y), Loot.Item.LocalizedShortName(), Color.green);
                    }
                    else
                    {
                        if (!Loot.Item.QuestItem && Distance2Loot < MenuConfig.RegularItemsDistance)
                            Renderer.DrawString(new Vector2(GroundItemsPos.x, GroundItemsPos.y), Loot.Item.LocalizedShortName(), Color.white);

                        if (Loot.Item.QuestItem && Distance2Loot < MenuConfig.QuestItemsDistance && MenuConfig.EnableQuestESP)
                            Renderer.DrawString(new Vector2(GroundItemsPos.x, GroundItemsPos.y), Loot.Item.LocalizedShortName() + " " + Math.Round(Distance2Loot).ToString() + "M", new Color(255, 125, 0));
                    }
                }
            }
        }

        private void ContainerESP()
        {
            if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.MainCamera == null || Globals.Containers.IsNullOrEmpty())
                return;

            for (int i = 0; i < Globals.Containers.Count(); i++)
            {
                LootableContainer Container = Globals.Containers.ElementAt(i);

                if (Container == null || Container.ItemOwner == null)
                    continue;

                Vector3 ContainerPos = Globals.W2S(Container.transform.position);

                if (ContainerPos == Vector3.zero || !Globals.IsOnScreen(ContainerPos))
                    continue;

                float Distance2Container = Vector3.Distance(Globals.MainCamera.transform.position, Container.transform.position);

                foreach (var Item in Container.ItemOwner.RootItem.GetAllItems())
                {
                    if (Item == null)
                        continue;

                    if (Item.LocalizedName() == Globals.ItemToSearch)
                        Renderer.DrawString(new Vector2(ContainerPos.x, ContainerPos.y), Item.LocalizedName() + " " + Math.Round(Distance2Container).ToString() + "M", Color.green);

                    // DRAW BIG MONEYYYY // || Item.LocalizedName() == ""
                    if (Item.LocalizedName() != Globals.ItemToSearch)
                    {
                        if (Item.LocalizedName() == "TerraGroup Labs keycard (Green)" || Item.LocalizedName() == "TerraGroup Labs keycard (Red)"
                            || Item.LocalizedName() == "TerraGroup Labs keycard (Blue)" || Item.LocalizedName() == "TerraGroup Labs keycard (Violet)"
                            || Item.LocalizedName() == "TerraGroup Labs keycard (Yellow)" || Item.LocalizedName() == "TerraGroup Labs keycard (Black)"
                            || Item.LocalizedName() == "UVSR Taiga-1 survival machete" || Item.LocalizedName() == "Red Rebel ice pick"
                            || Item.LocalizedName() == "Dorm room 314 marked key" || Item.LocalizedName() == "Chekannaya 15 apartment key"
                            || Item.LocalizedName() == "Graphics card" || Item.LocalizedName() == "Physical Bitcoin"
                            || Item.LocalizedName() == "Intelligence folder" || Item.LocalizedName() == "LEDX Skin Transilluminator"
                            || Item.LocalizedName() == "TerraGroup Labs access keycard" || Item.LocalizedName() == "Bottle of Fierce Hatchling moonshine"
                            || Item.LocalizedName() == "Portable defibrillator" || Item.LocalizedName() == "RB-PKPM marked key"
                            || Item.LocalizedName() == "Tetriz portable game console" || Item.LocalizedName() == "Bronze lion figurine"
                            || Item.LocalizedName() == "Virtex programmable processor" || Item.LocalizedName() == "Military power filter"
                            || Item.LocalizedName() == "VPX Flash Storage Module" || Item.LocalizedName() == "Relaxation room key"
                            || Item.LocalizedName() == "Phased array element" || Item.LocalizedName() == "Military COFDM Wireless Signal Transmitter"
                            || Item.LocalizedName() == "Can of thermite" || Item.LocalizedName() == "Gold skull ring"
                            || Item.LocalizedName() == "Golden Star balm" || Item.LocalizedName() == "Chain with Prokill medallion"
                            || Item.LocalizedName() == "GreenBat lithium battery" || Item.LocalizedName() == "Roler Submariner gold wrist watch"
                            || Item.LocalizedName() == "Ophthalmoscope" || Item.LocalizedName() == "Iridium military thermal vision module"
                            || Item.LocalizedName() == "Car dealership closed section key" || Item.LocalizedName() == "RB-BK marked key"
                            || Item.LocalizedName() == "RB-VO marked key" || Item.LocalizedName() == "Keycard with a blue marking"
                            || Item.LocalizedName() == "Mysterious room marked key" || Item.LocalizedName() == "Abandoned factory marked key"
                            || Item.LocalizedName() == "Health Resort west wing room 216 key" || Item.LocalizedName() == "Cottage back door key"
                            || Item.LocalizedName() == "ULTRA medical storage key" || Item.LocalizedName() == "Kiba Arms outer door key"
                            || Item.LocalizedName() == "Health Resort office key with a blue tape" || Item.LocalizedName() == "RB-PKPM marked key"
                            || Item.LocalizedName() == "Health Resort west wing room 301 key" || Item.LocalizedName() == "Health Resort east wing room 226 key"
                            || Item.LocalizedName() == "Health Resort west wing room 218 key" || Item.LocalizedName() == "TerraGroup Labs weapon testing area key"
                            || Item.LocalizedName() == "Shared bedroom marked key" || Item.LocalizedName() == "EMERCOM medical unit key"
                            || Item.LocalizedName() == "Factory emergency exit key" || Item.LocalizedName() == "Relaxation room key")
                        {
                            if (!Item.QuestItem && Distance2Container < MenuConfig.VaulableItemsDistance)
                                Renderer.DrawString(new Vector2(ContainerPos.x, ContainerPos.y), Item.LocalizedShortName() + " " + Math.Round(Distance2Container).ToString() + "M", Color.red);
                        }
                        else if (MenuConfig.ShowKappaItemsInGreen)
                        {
                            if (Item.LocalizedName() == "Old firesteel" || Item.LocalizedName() == "Antique axe" ||
                            Item.LocalizedName() == "Battered antique book" || Item.LocalizedName() == "FireKlean gun lube" ||
                            Item.LocalizedName() == "Golden rooster figurine" || Item.LocalizedName() == "Silver Badge" ||
                            Item.LocalizedName() == "Deadlyslob's beard oil" || Item.LocalizedName() == "Golden 1GPhone smartphone" ||
                            Item.LocalizedName() == "Jar of DevilDog mayo" || Item.LocalizedName() == "Can of sprats" ||
                            Item.LocalizedName() == "Fake mustache" || Item.LocalizedName() == "Kotton beanie" ||
                            Item.LocalizedName() == "Can of Dr. Lupo's coffee beans" || Item.LocalizedName() == "Pestily plague mask" ||
                            Item.LocalizedName() == "Raven figurine" || Item.LocalizedName() == "Shroud half-mask" ||
                            Item.LocalizedName() == "Veritas guitar pick" || Item.LocalizedName() == "42 Signature Blend English Tea" ||
                            Item.LocalizedName() == "Smoke balaclava" || Item.LocalizedName() == "Evasion armband" ||
                            Item.LocalizedName() == "Can of RatCola soda" || Item.LocalizedName() == "Loot Lord plushie" ||
                            Item.LocalizedName() == "WZ Wallet" || Item.LocalizedName() == "LVNDMARK's rat poison" ||
                            Item.LocalizedName() == "Missam forklift key" || Item.LocalizedName() == "Video cassette with the Cyborg Killer movie" ||
                            Item.LocalizedName() == "BakeEzy cook book" || Item.LocalizedName() == "JohnB Liquid DNB glasses" ||
                            Item.LocalizedName() == "Glorious E lightweight armored mask" || Item.LocalizedName() == "Baddie's red beard" ||
                            Item.LocalizedName() == "DRD body armor" || Item.LocalizedName() == "Gingy keychain" ||
                            Item.LocalizedName() == "Golden egg" || Item.LocalizedName() == "Press pass (issued for NoiceGuy)" ||
                            Item.LocalizedName() == "Axel parrot figurine" || Item.LocalizedName() == "BEAR Buddy plush toy")
                                Renderer.DrawString(new Vector2(ContainerPos.x, ContainerPos.y), Item.LocalizedShortName(), Color.green);
                        }
                        else
                        {
                            if (!Item.QuestItem && Distance2Container < MenuConfig.RegularItemsDistance)
                                Renderer.DrawString(new Vector2(ContainerPos.x, ContainerPos.y), Item.LocalizedShortName(), Color.white);

                            if (Item.QuestItem && Distance2Container < MenuConfig.QuestItemsDistance)
                                Renderer.DrawString(new Vector2(ContainerPos.x, ContainerPos.y), Item.LocalizedShortName() + " " + Math.Round(Distance2Container).ToString() + "M", new Color(255, 125, 0));
                        }
                    }
                }
            }
        }

        void GrenadeESP()
        {
            if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.MainCamera == null || Globals.Grenades.IsNullOrEmpty())
                return;

            for (int i = 0; i < Globals.Grenades.Count(); i++)
            {
                Throwable Throwables = Globals.Grenades.ElementAt(i);

                if (Throwables == null)
                    continue;

                Grenade Grenades = Throwables as Grenade;

                if (Grenades == null)
                    continue;

                Vector3 GrenadePos = Globals.W2S(Grenades.transform.position);

                if (GrenadePos == Vector3.zero || !Globals.IsOnScreen(GrenadePos))
                    continue;

                float GrenadeDistance = Vector3.Distance(Globals.LocalPlayer.Transform.position, Grenades.transform.position);

                Renderer.DrawString(new Vector2(GrenadePos.x, GrenadePos.y), Globals.ThrowableName(Grenades.name), GrenadeDistance < 8f ? Color.red : Color.white);
            }
        }

        private void ESP()
        {
            if (MenuConfig.EnableFOVCircle)
                Renderer.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), MenuConfig.AimbotFOV * 15.7f, 32, true, 1.25f);

            if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.MainCamera == null || Globals.Players.IsNullOrEmpty())
                return;

            if (MenuConfig.AmmoIndicator)
            {
                string AmmoIndicatorString = "";

                if (Globals.LocalPlayerWeapon != null && Globals.LocalPlayer != null)
                {
                    Color IndicatorClr = Color.white;

                    int CurrentAmmo = Globals.LocalPlayerWeapon.GetCurrentMagazineCount();
                    int MagCapacity = Globals.LocalPlayerWeapon.GetMaxMagazineCount();
                    int CurrentChamber = Globals.LocalPlayerWeapon.ChamberAmmoCount;

                    if (CurrentAmmo >= (MagCapacity / 2))
                        IndicatorClr = new Color(255, 255, 255);

                    if (CurrentAmmo <= (MagCapacity / 2))
                        IndicatorClr = new Color(255, 155, 0);

                    if (CurrentAmmo <= 3)
                        IndicatorClr = new Color(255, 0, 0);

                    AmmoIndicatorString = CurrentAmmo.ToString() + "/" + MagCapacity.ToString() + " + " + CurrentChamber;
                    Renderer.DrawString(new Vector2(Screen.width / 2f, Screen.height / 2f + 150f), AmmoIndicatorString, IndicatorClr);
                }
                else
                    AmmoIndicatorString = "";
            }

            for (int i = 0; i < Globals.Players.Count(); i++)
            {
                IPlayer _Player = Globals.Players.ElementAt(i);

                if (_Player == null)
                    continue;

                if (_Player.GetType() != typeof(ObservedPlayerView))
                    continue;

                ObservedPlayerView Player = _Player as ObservedPlayerView;
                if (Player == null)
                    continue;

                if (MenuConfig.EnableChams)
                {
                    var Skins = Player.PlayerBody.BodySkins.Values;
                    foreach (var Skin in Skins)
                    {
                        foreach (var Renderer in Skin.GetRenderers())
                        {
                            var Material = Renderer.material;

                            Material.shader = Globals.Bundle.LoadAsset<Shader>("chams.shader");

                            Material.SetColor("_ColorVisible", new Color(MenuConfig.VisR / 255f, MenuConfig.VisG / 255f, MenuConfig.VisB / 255f, 1f));
                            Material.SetColor("_ColorBehind", new Color(MenuConfig.HidR / 255f, MenuConfig.HidG / 255f, MenuConfig.HidB / 255f, 1f));
                        }
                    }
                }

                Vector3 HeadPos = Globals.W2S(Player.PlayerBones.Head.position);
                Vector3 Position = Globals.W2S(Player.Transform.position);

                if (HeadPos == Vector3.zero || Position == Vector3.zero || (!Globals.IsOnScreen(HeadPos) && !Globals.IsOnScreen(Position)))
                    continue;

                float Distance2Player = Vector3.Distance(Globals.MainCamera.transform.position, Player.PlayerBones.Head.position);
                bool IsVisible = Globals.IsPointVisible(Player, HeadPos);

                Color ESPClr = new Color(MenuConfig.ESPClrR / 255f, MenuConfig.ESPClrG / 255f, MenuConfig.ESPClrB / 255f);

                if (MenuConfig.EnableSkeleton)
                {
                    float SkeleLineWidth = 1.5f;

                    if (Distance2Player <= 200f)
                    {
                        TempBones.RightPalm = Globals.W2S(Player.PlayerBones.RightPalm.position);
                        TempBones.LeftPalm = Globals.W2S(Player.PlayerBones.LeftPalm.position);
                        TempBones.LeftShoulder = Globals.W2S(Player.PlayerBones.LeftShoulder.position);
                        TempBones.RightShoulder = Globals.W2S(Player.PlayerBones.RightShoulder.position);
                        TempBones.Neck = Globals.W2S(Player.PlayerBones.Neck.position);
                        TempBones.Head = Globals.W2S(Player.PlayerBones.Head.position);
                        TempBones.Pelvis = Globals.W2S(Player.PlayerBones.Pelvis.position);
                        TempBones.KickingFoot = Globals.W2S(Player.PlayerBones.KickingFoot.position);
                        TempBones.LeftFoot = Globals.W2S(Globals.GetBoneById(Player, 18));
                        TempBones.LeftForearm = Globals.W2S(Globals.GetBoneById(Player, 91));
                        TempBones.RightForearm = Globals.W2S(Globals.GetBoneById(Player, 112));
                        TempBones.LeftElbow = Globals.W2S(Globals.GetBoneById(Player, 90));
                        TempBones.RightElbow = Globals.W2S(Globals.GetBoneById(Player, 111));
                        TempBones.LeftCalf = Globals.W2S(Globals.GetBoneById(Player, 17));
                        TempBones.RightCalf = Globals.W2S(Globals.GetBoneById(Player, 22));

                        Renderer.DrawLine(new Vector2(TempBones.Head.x, TempBones.Head.y), new Vector2(TempBones.Neck.x, TempBones.Neck.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.Neck.x, TempBones.Neck.y), new Vector2(TempBones.LeftShoulder.x, TempBones.LeftShoulder.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.Neck.x, TempBones.Neck.y), new Vector2(TempBones.RightShoulder.x, TempBones.RightShoulder.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.LeftShoulder.x, TempBones.LeftShoulder.y), new Vector2(TempBones.LeftElbow.x, TempBones.LeftElbow.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.RightShoulder.x, TempBones.RightShoulder.y), new Vector2(TempBones.RightElbow.x, TempBones.RightElbow.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.LeftElbow.x, TempBones.LeftElbow.y), new Vector2(TempBones.LeftForearm.x, TempBones.LeftForearm.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.RightElbow.x, TempBones.RightElbow.y), new Vector2(TempBones.RightForearm.x, TempBones.RightForearm.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.LeftForearm.x, TempBones.LeftForearm.y), new Vector2(TempBones.LeftPalm.x, TempBones.LeftPalm.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.RightForearm.x, TempBones.RightForearm.y), new Vector2(TempBones.RightPalm.x, TempBones.RightPalm.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.Neck.x, TempBones.Neck.y), new Vector2(TempBones.Pelvis.x, TempBones.Pelvis.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.Pelvis.x, TempBones.Pelvis.y), new Vector2(TempBones.LeftCalf.x, TempBones.LeftCalf.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.Pelvis.x, TempBones.Pelvis.y), new Vector2(TempBones.RightCalf.x, TempBones.RightCalf.y), SkeleLineWidth, ESPClr);

                        Renderer.DrawLine(new Vector2(TempBones.LeftCalf.x, TempBones.LeftCalf.y), new Vector2(TempBones.LeftFoot.x, TempBones.LeftFoot.y), SkeleLineWidth, ESPClr);
                        Renderer.DrawLine(new Vector2(TempBones.RightCalf.x, TempBones.RightCalf.y), new Vector2(TempBones.KickingFoot.x, TempBones.KickingFoot.y), SkeleLineWidth, ESPClr);
                    }
                }

                float BoxPositionY = (HeadPos.y - 10f);
                float BoxHeight = (Math.Abs(HeadPos.y - Position.y) + 10f);
                float BoxWidth = (BoxHeight * 0.65f);

                bool IsScav = false;
                if (Player.ObservedPlayerController != null && Player.ObservedPlayerController.InfoContainer != null)
                    IsScav = Player.ObservedPlayerController.InfoContainer.Side == EPlayerSide.Savage;
                else
                {
                    IsScav = Globals.IsRussianCharOrSpace(Player.NickName.Localized());
                    Renderer.DrawString(new Vector2(25f, 100f), "WARNING 1", Color.red, false);
                }

                bool IsTeam = Player.TeamId == Globals.LocalPlayer.TeamId && MenuConfig.InTeam;

                bool IsBoss = Globals.IsBossByName(Player.NickName.Localized());

                if (IsBoss)
                {
                    if (MenuConfig.EnableName)
                        Renderer.DrawString(new Vector2(HeadPos.x, BoxPositionY - 2.5f), Globals.TranslateBossName(Player.NickName.Localized()) + " " + Math.Round(Distance2Player).ToString() + "M", Color.green);
                }
                else if (IsScav)
                {
                    if (MenuConfig.EnableName)
                    {
                        if (Distance2Player <= MenuConfig.ScavDistance)
                            Renderer.DrawString(new Vector2(HeadPos.x, BoxPositionY - 2.5f), "AI" + " " + Math.Round(Distance2Player).ToString() + "M", Distance2Player >= 300f ? new Color(0.6f, 0f, 0.6f, 0.4f) : new Color(1f, 0f, 1f, 1f));
                    }
                }
                else
                {
                    if (MenuConfig.DisableModelOcclusion)
                        Player.ObservedPlayerController.Culling.Disable();
                    else
                        Player.ObservedPlayerController.Culling.Enable();

                    if (MenuConfig.EnableName)
                       Renderer.DrawString(new Vector2(HeadPos.x, BoxPositionY - 2.5f), Player.NickName.Localized() + " " + Math.Round(Distance2Player).ToString() + "M", IsTeam ? Color.cyan : Distance2Player >= 300f ? new Color(0.6f, 0.6f, 0.6f, 0.4f) : Color.white);               

                    if (Player.ObservedPlayerController.HandsController != null)
                    {
                        if (Player.ObservedPlayerController.HandsController.Weapon != null)
                        {
                            if (MenuConfig.EnableWeapon && !IsTeam && Distance2Player <= 250f)
                                Renderer.DrawString(new Vector2(HeadPos.x, BoxPositionY - 13f), Player.ObservedPlayerController.HandsController.Weapon.LocalizedShortName(), Color.white);
                        }
                        else if (Player.ObservedPlayerController.HandsController.ItemInHands != null)
                        {
                            if (MenuConfig.EnableWeapon && !IsTeam && Distance2Player <= 250f)
                                Renderer.DrawString(new Vector2(HeadPos.x, BoxPositionY - 13f), Player.ObservedPlayerController.HandsController.ItemInHands.LocalizedShortName(), Color.white);
                        }
                    }  
                }

                if (MenuConfig.EnableAimLines && !IsTeam && Distance2Player <= 80f)
                {
                    TempBones.GunPointingAt = Globals.W2S(Player.PlayerBones.WeaponRoot.position);
                    TempBones.GunPointingAtForward = Globals.W2S(Player.PlayerBones.WeaponRoot.position - Player.PlayerBones.WeaponRoot.up * 1.3f);

                    Renderer.DrawLine(new Vector2(TempBones.GunPointingAt.x, TempBones.GunPointingAt.y), new Vector2(TempBones.GunPointingAtForward.x, TempBones.GunPointingAtForward.y), 1.5f, Color.white);
                }
            }
        }
    }
}
