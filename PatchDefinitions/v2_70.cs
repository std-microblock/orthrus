// Copyright (C) 2021 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	public static class v2_70
	{
		public static void addVersions()
		{
			HitmanVersion.addVersion("2.70.1.0_dx11", 0x5D7A215A, v2_70_1_dx11);
			HitmanVersion.addVersion("2.70.1.0_dx12", 0x5D7A23C1, v2_70_1_dx12);
			HitmanVersion.addVersion("2.70.1.0-h1_dx11", 0x5D8CE739, v2_70_1_h1_dx11);
			HitmanVersion.addVersion("2.70.1.0-h1_dx12", 0x5D8CE78C, v2_70_1_h1_dx12);
		}

		private static HitmanVersion v2_70_1_dx11 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F2F853, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B58838, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B5885C, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BB50C8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x1828F38, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4D3A4, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BB5A08, "01", "00", MemProtection.PAGE_READWRITE) }
		};

		public static HitmanVersion v2_70_1_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F2F343, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B58328, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B5834C, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BC6708, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x1838FC8, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4CE94, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BC7048, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v2_70_1_h1_dx11 = new HitmanVersion()
		{
			certpin = v2_70_1_dx11.certpin,
			authheader = v2_70_1_dx11.authheader,
			configdomain = new[] { new Patch(0x2BB50A8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = v2_70_1_dx11.protocol,
			dynres_noforceoffline = new[] { new Patch(0x2BB59E8, "01", "00", MemProtection.PAGE_READWRITE) }
		};

		public static HitmanVersion v2_70_1_h1_dx12 = new HitmanVersion()
		{
			certpin = v2_70_1_dx12.certpin,
			authheader = v2_70_1_dx12.authheader,
			configdomain = new[] { new Patch(0x2BC66E8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = v2_70_1_dx12.protocol,
			dynres_noforceoffline = new[] { new Patch(0x2BC7028, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};
	}
}
