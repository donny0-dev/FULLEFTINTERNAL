using EFT.Interactive;
using EFT.NextObservedPlayer;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT.UI;
using System.Runtime.InteropServices;

namespace NVIDIA
{
    public static class TempBones
    {
        public static Vector3 RightPalm;
        public static Vector3 LeftPalm;
        public static Vector3 LeftShoulder;
        public static Vector3 RightShoulder;
        public static Vector3 Neck;
        public static Vector3 Head;
        public static Vector3 Pelvis;
        public static Vector3 KickingFoot;
        public static Vector3 LeftFoot;
        public static Vector3 LeftForearm;
        public static Vector3 RightForearm;
        public static Vector3 LeftElbow;
        public static Vector3 RightElbow;
        public static Vector3 LeftCalf;
        public static Vector3 RightCalf;
        public static Vector3 GunPointingAt;
        public static Vector3 GunPointingAtForward;
    }

    public sealed class RunOnceAction
    {
        private readonly Action F;
        private bool hasRun;

        public RunOnceAction(Action f)
        {
            F = f;
        }

        public void Run()
        {
            if (hasRun) return;
            F();
            hasRun = true;
        }
    }

    public static class Globals
    {
        public static Camera MainCamera;
        public static EFT.GameWorld GameWorld;

        public static bool EnabledHiddenQuestItems = false;

        public static AssetBundle Bundle;

        public static PreloaderUI PreloaderUI;

        public static EFT.Player LocalPlayer;
        public static EFT.InventoryLogic.Weapon LocalPlayerWeapon;

        public static List<IPlayer> Players = new List<IPlayer>();
        public static List<LootableContainer> Containers = new List<LootableContainer>();
        public static List<LootItem> GroundItems = new List<LootItem>();
        public static List<Throwable> Grenades = new List<Throwable>();

        public static bool IsFarLootToggled = false;
        public static bool IsExtendedLeanToggled = false;

        public static bool IsMenuOpen = false;
        //public static bool Offline = false;

        //public static Camera OpticCamera;
        //public static (Vector2 Center, float Radius) ScopeParameters;

        public static string ItemToSearch = "";
        public static string LastColliderHit = "";

        public static string[] KeyNames = { "Q", "E", "LALT", "M5", "M4", "RMB" };

        public static Vector3 FinalVector(Diz.Skinning.Skeleton Skeletor, int BoneId)
        {
            try { return Skeletor.Bones.ElementAt(BoneId).Value.position; }
            catch { return Vector3.zero; }
        }

        public static Vector3 GetBoneById(ObservedPlayerView Player, int BoneId)
        {
            return FinalVector(Player.PlayerBody.SkeletonRootJoint, BoneId);
        }

        public static bool IsOnScreen(Vector3 V)
        {
            if (V.x > 0.01f && V.y > 0.01f && V.x < Screen.width && V.y < Screen.height && V.z > 0.01f)
                return true;

            return false;
        }

        private static RaycastHit RaycastHit;
        private static int VisMask = 1 << 12 | 1 << 16;

        public static bool IsPointVisible(ObservedPlayerView player, Vector3 BonePos)
        {
            return Physics.Raycast(Globals.LocalPlayer.PlayerBones.Head.position, BonePos, out RaycastHit, VisMask) && RaycastHit.collider && RaycastHit.collider.gameObject.transform.root.gameObject == player.transform.root.gameObject;
        }

        public static string GetPlayerRaycast(ObservedPlayerView player, Vector3 BonePos)
        {
            Physics.Linecast(Globals.LocalPlayer.PlayerBones.Head.position, BonePos, out RaycastHit);

            return "collider tag = " + RaycastHit.collider.gameObject.tag;
        }

        public static Vector3 W2S(Vector3 pos)
        {
            if (!Globals.MainCamera)
                return new Vector3(0, 0, 0);

            var WS2P = Globals.MainCamera.WorldToScreenPoint(pos);
            WS2P.y = Screen.height - WS2P.y;

            if (WS2P.z < 0.001f)
                return new Vector3(0, 0, 0);

            return WS2P;
        }

        public static bool IsRussianCharOrSpace(string name)
        {
            if (name.Contains("и") || name.Contains("н")
                || name.Contains("ф") || name.Contains("Ш")
                || name.Contains("Б") || name.Contains("Д")
                || name.Contains("л") || name.Contains("ь")
                || name.Contains("т") || name.Contains("Г")
                || name.Contains("щ") || name.Contains("й")
                || name.Contains("ц") || name.Contains("ч")
                || name.Contains("я") || name.Contains("ы")
                || name.Contains("ё") || name.Contains("э")
                || name.Contains(" "))
                return true;

            return false;
        }

        public static string TranslateBossName(string name)
        {
            if (name == "Килла")
                return "Killa";
            else if (name == "Решала")
                return "Reshala";
            else if (name == "Глухарь")
                return "Gluhar";
            else if (name == "Штурман")
                return "Shturman";
            else if (name == "Санитар")
                return "Sanitar";
            else if (name == "Тагилла")
                return "Tagilla";
            else if (name == "Зрячий")
                return "Zryachiy";
            else if (name == "Кабан")
                return "Kaban";
            else if (name == "Дед Мороз")
                return "Santa";
            else if (name == "Коллонтай")
                return "Kollontay";
            else if (name == "Big Pipe")
                return "Big Pipe";
            else if (name == "Birdeye")
                return "Birdeye";
            else if (name == "Knight")
                return "Death Knight";

            return "";
        }

        public static bool IsBossByName(string name)
        {
            if (name == "Килла" || name == "Решала" || name == "Глухарь" || name == "Штурман" || name == "Санитар" || name == "Тагилла" || name == "Зрячий" || name == "Кабан" || name == "Big Pipe" || name == "Birdeye" || name == "Knight" || name == "Дед Мороз" || name == "Коллонтай")
                return true;
            else
                return false;
        }

        public static string ThrowableName(string name)
        {
            switch (name)
            {
                case "weapon_rgd5_world(Clone)":
                    return "RGD5";
                case "weapon_grenade_f1_world(Clone)":
                    return "F1";
                case "weapon_rgd2_world(Clone)":
                    return "SMOKE";
                case "weapon_rdg2_world(Clone)":
                    return "SMOKE";
                case "weapon_m18_world(Clone)":
                    return "SMOKE";
                case "weapon_m67_world(Clone)":
                    return "M67";
                case "weapon_zarya_world(Clone)":
                    return "FLASH";
                case "weapon_m7920_world(Clone)":
                    return "FLASH";
                case "weapon_grenade_chattabka_vog17_world(Clone)":
                    return "VOG 17";
                case "weapon_grenade_chattabka_vog25_world(Clone)":
                    return "VOG 25";
                case "weapon_grenade_rgo_world(Clone)":
                    return "IMPACT";
                case "weapon_grenade_rgn_world(Clone)":
                    return "IMPACT";
                default:
                    return name.Replace("weapon_", "").Replace("_world(Clone)", "").Replace("grenade_", "");
            }
        }

        //public static bool GetCameraOffset(Camera Camera, out float Scale, out Vector2 CameraOffset)
        //{
        //    Scale = 0f;
        //    CameraOffset = Vector2.zero;

        //    if (OpticCamera == null)
        //        return false;

        //    Scale = Screen.height / (float)Camera.scaledPixelHeight;
        //    CameraOffset = new Vector2(
        //        Camera.pixelWidth / 2 - OpticCamera.pixelWidth / 2,
        //        Camera.pixelHeight / 2 - OpticCamera.pixelHeight / 2);

        //    return true;
        //}

        //public static void GetScopeParameters(Camera Camera, OpticSight CurrentOptic)
        //{
        //    var OpticTransform = CurrentOptic.LensRenderer.transform;
        //    var LensMesh = CurrentOptic.LensRenderer.GetComponent<MeshFilter>().mesh;
        //    var LensUpperRight = OpticTransform.TransformPoint(LensMesh.bounds.max);
        //    var LensUpperLeft = OpticTransform.TransformPoint(new Vector3(LensMesh.bounds.min.x, 0, LensMesh.bounds.max.z));

        //    var lensUpperRight3D = Camera.WorldToScreenPoint(LensUpperRight);
        //    var lensUpperLeft3D = Camera.WorldToScreenPoint(LensUpperLeft);
        //    ScopeParameters.Radius = Vector2.Distance(lensUpperRight3D, lensUpperLeft3D) / 2;
        //    ScopeParameters.Center = Camera.WorldToScreenPoint(OpticTransform.position);
        //}

        //public static Vector2 ScopePointToScreenPoint(Camera Camera, Vector3 WorldPoint, out bool Visible)
        //{
        //    Visible = false;

        //    if (OpticCamera == null || !GetCameraOffset(Camera, out var scale, out var cameraOffset))
        //        return Camera.WorldToScreenPoint(WorldPoint);

        //    var ScopePoint = OpticCamera.WorldToScreenPoint(WorldPoint) + (Vector3)cameraOffset;
        //    ScopePoint.y = Screen.height - ScopePoint.y * scale;
        //    ScopePoint.x *= scale;

        //    var Distance = Vector2.Distance(ScopeParameters.Center, ScopePoint);
        //    if (Distance <= ScopeParameters.Radius)
        //        Visible = true;

        //    return ScopePoint;
        //}

        //public static bool W2SChecked(Vector3 Position, out Vector2 ScreenPos, bool IsScoped)
        //{
        //    if (IsScoped)
        //    {
        //        ScreenPos = (Vector3)ScopePointToScreenPoint(Globals.MainCamera, Position, out var Visible);
        //        if (Visible == false || ScreenPos.IsZero())
        //            return false;
        //    }
        //    else
        //    {
        //        ScreenPos = Globals.MainCamera.WorldToScreenPoint(Position);
        //        if (ScreenPos.IsZero())
        //            return false;
        //    }

        //    return true;
        //}

        public static float CalcFov(Vector3 ang)
        {
            if (Globals.MainCamera == null)
                return 0f;

            Vector3 position = Globals.MainCamera.transform.position;
            Vector3 forward = Globals.MainCamera.transform.forward;
            Vector3 normalized = (ang - position).normalized;

            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }
        public static void AimAtPos(Vector3 position)
        {
            if (Globals.LocalPlayer == null)
                return;

            Vector3 b = Globals.LocalPlayer.Fireport.position - Globals.LocalPlayer.Fireport.up * 1f;
            Vector3 eulerAngles = Quaternion.LookRotation((position - b).normalized).eulerAngles;

            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;

            Globals.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

        internal static void SetPrivateField(this object obj, string name, object value)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = obj.GetType();
            FieldInfo field = type.GetField(name, bindingAttr);
            field.SetValue(obj, value);
        }

        public static Vector3 BarrelRaycast()
        {
            if (Globals.GameWorld == null || Globals.MainCamera == null || Globals.LocalPlayer == null || Globals.LocalPlayerWeapon == null)
                return Vector3.zero;

            Physics.Linecast(
                Globals.LocalPlayer.Fireport.position,
                Globals.LocalPlayer.Fireport.position - Globals.LocalPlayer.Fireport.up * 9999f,
                out RaycastHit);

            return RaycastHit.point;
        }

        public static string GetContainerName(ObservedPlayerView Player)
        {
            foreach (var Items in Player.ObservedPlayerController.InventoryController.EquipmentItems)
            {
                if (Items.LocalizedName().Contains("Secure container"))
                    return Items.LocalizedName();
            }

            return "";
        }
    }

    public static class MenuConfig
    {
        //AIM
        public static bool EnableAimbot = true;

        //public static bool EnableSilentAim = false;
        //public static float AimbotHitchance = 80f;
        //public static float AimbotAimcone = 0f;

        public static bool EnableAimbotPred = true;
        public static bool EnableNoSway = true;
        public static bool EnableNoRecoil = true;
        public static bool EnableOnlyWhenAimbotting = true;
        public static float AimbotFOV = 2f;
        public static bool EnableFOVCircle = true;
        public static float AimbotMaxDistance = 200f;
        public static int AimbotKeyIndex = 0;
        public static int AimbotHitboxIndex = 0;

        //ESP
        public static bool EnableESP = true;

        public static float ScavDistance = 200f;

        public static bool EnableBox = false;
        public static bool EnableName = true;
        public static bool EnableWeapon = true;
        public static bool EnableAimLines = false;
        public static bool EnableSkeleton = true;

        public static float ESPClrR = 0f;
        public static float ESPClrG = 255f;
        public static float ESPClrB = 255f;

        public static bool EnableChams = true;
        public static bool DisableModelOcclusion = true;
        public static float VisR = 255f;
        public static float VisG = 0f;
        public static float VisB = 255f;
        public static float HidR = 155f;
        public static float HidG = 155f;
        public static float HidB = 155f;

        public static bool EnableGrenadeESP = true;

        public static bool EnableItemESP = true;
        public static bool EnableContainerESP = true;
        public static bool EnableQuestESP = false;
        public static bool ShowKappaItemsInGreen = false;

        public static float QuestItemsDistance = 1000f;
        public static float VaulableItemsDistance = 1000f;
        public static float RegularItemsDistance = 5f;

        // MISC
        public static bool ThermalVision = false;
        public static bool NightVision = false;
        public static bool UnlockHiddenQuestItems = false;
        public static bool AmmoIndicator = true;
        public static bool Crosshair = true;
        public static bool NoFog = false;
        public static bool InTeam = false;

        public static bool LootThruWalls = false;
        public static float ThruDist = 3f;

        public static bool ExtendedLean = false;
        public static float LeanFactor = 0.9f;

        public static int PlayerFontSize = 10;
        public static int ItemFontSize = 10;

        public static float TestFactor = 4f;

        public static int MenuIndex = 0;
    }

    public static class Renderer
    {
        public static Material DrawMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        public static Texture2D LineTex = new Texture2D(1, 1);

        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);
        private class RingArray
        {
            public Vector2[] Positions { get; private set; }
            public RingArray(int numSegments)
            {
                Positions = new Vector2[numSegments];
                var stepSize = 360f / numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    var rad = Mathf.Deg2Rad * stepSize * i;
                    Positions[i] = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
                }
            }
        }

        private static Dictionary<int, RingArray> ringDict = new Dictionary<int, RingArray>();
        public static Color Color
        {
            get { return GUI.color; }
            set { GUI.color = value; }
        }

        public static void DrawLine(Vector2 start, Vector2 end, float thickness, Color color)
        {
            LineTex.filterMode = FilterMode.Point;
            var backup_matrix = GUI.matrix;
            var backup_color = GUI.color;
            GUI.color = color;
            var width = end - start;
            float rotate = (float)(57.29577951308232 * (double)Mathf.Atan(width.y / width.x));
            if (width.x < 0f)
                rotate += 180f;

            if (thickness < 1)
                thickness = 1;
            int rotate2 = (int)Mathf.Ceil((float)(thickness / 2));
            GUIUtility.RotateAroundPivot(rotate, start);
            GUI.DrawTexture(new Rect(start.x, start.y - (float)rotate2, width.magnitude, (float)thickness), LineTex);
            GUIUtility.RotateAroundPivot(-rotate, start);
            GUI.color = backup_color;
            GUI.matrix = backup_matrix;
        }

        public static void DrawBox(float x, float y, float w, float h, Color color)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), 1f, color);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), 1f, color);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), 1f, color);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), 1f, color);
        }

        public static void DrawBox(Vector2 position, Vector2 size, float thickness, Color color, bool centered = true)
        {
            Color = color;
            DrawBox(position, size, thickness, centered);
        }

        public static void DrawBox(Vector2 position, Vector2 size, float thickness, bool centered = true)
        {
            var upperLeft = centered ? position - size / 2f : position;
            GUI.DrawTexture(new Rect(position.x, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x + size.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture);
        }

        public static void DrawCross(Vector2 position, Vector2 size, float thickness, Color color)
        {
            Color = color;
            DrawCross(position, size, thickness);
        }

        public static void DrawCross(Vector2 position, Vector2 size, float thickness)
        {
            GUI.DrawTexture(new Rect(position.x - size.x / 2f, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y - size.y / 2f, thickness, size.y), Texture2D.whiteTexture);
        }

        public static void DrawDot(Vector2 position, Color color)
        {
            Color = color;
            DrawDot(position);
        }

        public static void DrawDot(Vector2 position)
        {
            DrawBox(position - Vector2.one, Vector2.one * 2f, 1f);
        }

        public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
        {
            Color = color;
            DrawString(position, label, centered);
        }

        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);
            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content);
        }

        public static void DrawCircle(Vector2 position, float radius, int numSides, bool centered = true, float thickness = 1f)
        {
            DrawCircle(position, radius, numSides, Color.white, centered, thickness);
        }

        public static void DrawCircle(Vector2 position, float radius, int numSides, Color color, bool centered = true, float thickness = 1f)
        {
            RingArray arr;
            if (ringDict.ContainsKey(numSides))
                arr = ringDict[numSides];
            else
                arr = ringDict[numSides] = new RingArray(numSides);
            var center = centered ? position : position + Vector2.one * radius;
            for (int i = 0; i < numSides - 1; i++)
                DrawLine(center + arr.Positions[i] * radius, center + arr.Positions[i + 1] * radius, thickness, color);
            DrawLine(center + arr.Positions[0] * radius, center + arr.Positions[arr.Positions.Length - 1] * radius, thickness, color);
        }

        public static void DrawSnapline(Vector3 worldpos, Color color)
        {
            if (Globals.MainCamera == null)
                return;

            Vector3 pos = Globals.MainCamera.WorldToScreenPoint(worldpos);
            pos.y = Screen.height - pos.y;
            GL.PushMatrix();
            GL.Begin(1);
            DrawMaterial.SetPass(0);
            GL.Color(color);
            GL.Vertex3(Screen.width / 2, Screen.height, 0f);
            GL.Vertex3(pos.x, pos.y, 0f);
            GL.End();
            GL.PopMatrix();
        }
    }
}
