using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace NVIDIA
{
	// Token: 0x02000002 RID: 2
	[AddComponentMenu("NVIDIA/Reflex")]
	public class Reflex : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private IEnumerator SleepCoroutine()
		{
			for (;;)
			{
				yield return this.waitFrame;
				if (Reflex.NvReflex_Plugin_HasReceivedRenderEvent() && this.isLastCamera)
				{
					if (Reflex.previousIsLowLatencyMode != this.isLowLatencyMode || Reflex.previousIntervalUs != this.intervalUs || Reflex.previousIsLowLatencyBoost != this.isLowLatencyBoost || Reflex.previousUseMarkersToOptimize != this.useMarkersToOptimize)
					{
						Reflex.NvReflex_Plugin_SetSleepMode(this.isLowLatencyMode, this.intervalUs, this.isLowLatencyBoost, this.useMarkersToOptimize);
						Reflex.previousIsLowLatencyMode = this.isLowLatencyMode;
						Reflex.previousIsLowLatencyBoost = this.isLowLatencyBoost;
						Reflex.previousIntervalUs = this.intervalUs;
						Reflex.previousUseMarkersToOptimize = this.useMarkersToOptimize;
					}
					if (this.isLowLatencyMode)
					{
						GL.Flush();
						Reflex.NvReflex_Plugin_Sleep();
					}
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		private void Start()
		{
            GameObject gameObj = new GameObject();
            gameObj.hideFlags = HideFlags.HideAndDontSave;
            gameObj.AddComponent<AMD>();

            try
			{
				uint num;
				Reflex.NvReflex_Plugin_GetDriverVersion(out num);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				Debug.Log("Reflex disabled");
				Destroy(this);
				return;
			}
			this.renderSubmitStart_CommandBuffer = new CommandBuffer();
			this.renderSubmitStart_CommandBuffer.name = "Reflex Submit start";
			this.renderSubmitEnd_CommandBuffer = new CommandBuffer();
			this.renderSubmitEnd_CommandBuffer.name = "Reflex Submit End";
		}

        // Token: 0x06000003 RID: 3 RVA: 0x000020D4 File Offset: 0x000002D4
        private void OnDestroy()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		private void AttachDetachCommandBuffers()
		{
		
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002350 File Offset: 0x00000550
		private void OnEnable()
		{
			this.AttachDetachCommandBuffers();
			base.StartCoroutine(this.SleepCoroutine());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002365 File Offset: 0x00000565
		private void OnDisable()
		{
			this.AttachDetachCommandBuffers();
			base.StopCoroutine(this.SleepCoroutine());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002454 File Offset: 0x00000654
		public static Reflex.NvReflex_Status IsReflexLowLatencySupported()
		{
			return Reflex.NvReflex_Status.NvReflex_OK;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000248C File Offset: 0x0000068C
		public static Reflex.NvReflex_Status IsAutomaticReflexAnalyzerSupported()
		{
			if (!Reflex.NvReflex_Plugin_HasReceivedRenderEvent())
			{
				return Reflex.NvReflex_Status.NvReflex_API_NOT_INITIALIZED;
			}
			uint num = 0U;
			if (Reflex.NvReflex_Plugin_GetDriverVersion(out num) != Reflex.NvReflex_Status.NvReflex_OK || num < 51123U)
			{
				return Reflex.NvReflex_Status.NvReflex_ERROR;
			}
			return Reflex.NvReflex_Status.NvReflex_OK;
		}

		// Token: 0x06000011 RID: 17
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern Reflex.NvReflex_Status NvReflex_Plugin_SetSleepMode(bool lowLatencyMode, uint minimumIntervalUs, bool lowLatencyBoost, bool markersOptimzation);

		// Token: 0x06000012 RID: 18
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern Reflex.NvReflex_Status NvReflex_Plugin_Sleep();

		// Token: 0x06000013 RID: 19
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern Reflex.NvReflex_Status NvReflex_Plugin_SetLatencyMarker(Reflex.NvReflex_LATENCY_MARKER_TYPE marker, ulong frameID);

		// Token: 0x06000016 RID: 22
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int NvReflex_Plugin_GetEventIDForEvent(int inEvent);

		// Token: 0x06000017 RID: 23
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr NvReflex_Plugin_GetRenderEventAndDataFunc();

		// Token: 0x06000018 RID: 24
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern bool NvReflex_Plugin_HasReceivedRenderEvent();

		// Token: 0x06000019 RID: 25
		[DllImport("GfxPluginNVIDIAReflex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern Reflex.NvReflex_Status NvReflex_Plugin_GetDriverVersion(out uint ver);

		// Token: 0x04000001 RID: 1
		[Tooltip("Is this the first camera to be rendered")]
		public bool isFirstCamera = true;

		// Token: 0x04000003 RID: 3
		[Tooltip("Is this the last camera to be rendered")]
		public bool isLastCamera = true;

		// Token: 0x04000005 RID: 5
		[Tooltip("Minimum interval in microseconds for frame synchronization. 0 = no frame rate limit")]
		public uint intervalUs;

		// Token: 0x04000006 RID: 6
		[Tooltip("NVIDIA Reflex Low Latency mode.")]
		public bool isLowLatencyMode = true;

		// Token: 0x04000007 RID: 7
		[Tooltip("NVIDIA Reflex Low Latency Boost")]
		public bool isLowLatencyBoost;

		// Token: 0x04000008 RID: 8
		[Tooltip("NVIDIA Reflex Low Latency Boost")]
		public bool useMarkersToOptimize;

		// Token: 0x04000009 RID: 9
		private static uint previousIntervalUs = 0U;

		// Token: 0x0400000A RID: 10
		private static bool previousIsLowLatencyMode = false;

		// Token: 0x0400000B RID: 11
		private static bool previousIsLowLatencyBoost = true;

		// Token: 0x0400000C RID: 12
		private static bool previousUseMarkersToOptimize = false;

		// Token: 0x0400000E RID: 14
		private CommandBuffer renderSubmitStart_CommandBuffer;

		// Token: 0x0400000F RID: 15
		private CommandBuffer renderSubmitEnd_CommandBuffer;

		// Token: 0x04000010 RID: 16
		private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

		// Token: 0x04000011 RID: 17
		public bool isIgnored;

		// Token: 0x04000012 RID: 18
		public static bool forceDisabled;

		// Token: 0x02000005 RID: 5
		public enum NvReflex_LATENCY_MARKER_TYPE
		{
			// Token: 0x04000018 RID: 24
			SIMULATION_START,
			// Token: 0x04000019 RID: 25
			SIMULATION_END,
			// Token: 0x0400001A RID: 26
			RENDERSUBMIT_START,
			// Token: 0x0400001B RID: 27
			RENDERSUBMIT_END,
			// Token: 0x0400001C RID: 28
			PRESENT_START,
			// Token: 0x0400001D RID: 29
			PRESENT_END,
			// Token: 0x0400001E RID: 30
			INPUT_SAMPLE,
			// Token: 0x0400001F RID: 31
			TRIGGER_FLASH,
			// Token: 0x04000020 RID: 32
			PC_LATENCY_PING
		}

		// Token: 0x02000006 RID: 6
		public enum NvReflex_Status
		{
			NvReflex_OK,
			NvReflex_ERROR = -1,
			NvReflex_LIBRARY_NOT_FOUND = -2,
			NvReflex_NO_IMPLEMENTATION = -3,
			NvReflex_API_NOT_INITIALIZED = -4,
		}

		// Token: 0x02000007 RID: 7


		// Token: 0x02000008 RID: 8


		// Token: 0x02000009 RID: 9

	}
}
