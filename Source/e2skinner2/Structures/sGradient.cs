using System;
using OpenSkinDesigner.Logic;

namespace OpenSkinDesigner.Structures
{
	// Token: 0x02000017 RID: 23
	public class sGradient
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000133D4 File Offset: 0x000115D4
		public sColor ColorStart
		{
			get
			{
				Logger.LogMessage("+++++++++++ sGradient.cs - color start ");
				return this.pColorStart;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000133F8 File Offset: 0x000115F8
		public sColor ColorMid
		{
			get
			{
				Logger.LogMessage("+++++++++++ sGradient.cs - color mid ");
				return this.pColorMid;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0001341C File Offset: 0x0001161C
		public sColor ColorEnd
		{
			get
			{
				Logger.LogMessage("+++++++++++ sGradient.cs - color end ");
				return this.pColorEnd;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00013440 File Offset: 0x00011640
		public eGradientDirection Direction
		{
			get
			{
				Logger.LogMessage("+++++++++++ sGradient.cs - direction ");
				return this.pDirection;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00013464 File Offset: 0x00011664
		protected sGradient(string value)
		{
			this.pValue = value;
			Logger.LogMessage("+++++++++++ sGradient.cs - string value ");
			string[] array = value.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			this.pColorStart = (sColor)cDataBase.pColors.get(array[0]);
			if (array.Length == 3)
			{
				// OpenPLi/enigma2 allows: startColor,endColor,direction.
				// Use the end color as the mid color for designer preview.
				this.pColorMid = (sColor)cDataBase.pColors.get(array[1]);
				this.pColorEnd = (sColor)cDataBase.pColors.get(array[1]);
			}
			else
			{
				this.pColorMid = (sColor)cDataBase.pColors.get(array[1]);
				this.pColorEnd = (sColor)cDataBase.pColors.get(array[2]);
			}
			string text = array[array.Length - 1];
			string text2 = text;
			if (!(text2 == "horizontal"))
			{
				if (!(text2 == "vertical"))
				{
					this.pDirection = eGradientDirection.Horizontal;
				}
				else
				{
					this.pDirection = eGradientDirection.Vertical;
				}
			}
			else
			{
				this.pDirection = eGradientDirection.Horizontal;
			}
		}


		public static bool isGradient(string value)
		{
			if (string.IsNullOrEmpty(value))
				return false;

			string[] parts = value.Split(new char[] { ',' });
			if (parts.Length != 3 && parts.Length != 4)
				return false;

			string direction = parts[parts.Length - 1].Trim().ToLowerInvariant();
			return direction == "horizontal" || direction == "vertical";
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00013524 File Offset: 0x00011724
		public static sGradient parse(string value)
		{
			Logger.LogMessage("+++++++++++ sGradient.cs - parse ");
			bool flag = value == null;
			sGradient sGradient;
			if (flag)
			{
				sGradient = null;
			}
			else
			{
				bool flag2 = !isGradient(value);
				if (flag2)
				{
					sGradient = null;
				}
				else
				{
					sGradient = new sGradient(value);
				}
			}
			return sGradient;
		}

		// Token: 0x040000F9 RID: 249
		public string pValue;

		// Token: 0x040000FA RID: 250
		protected sColor pColorStart;

		// Token: 0x040000FB RID: 251
		protected sColor pColorMid;

		// Token: 0x040000FC RID: 252
		protected sColor pColorEnd;

		// Token: 0x040000FD RID: 253
		protected eGradientDirection pDirection;
	}
}
