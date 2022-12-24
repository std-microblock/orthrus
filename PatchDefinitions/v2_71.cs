// Copyright (C) 2020 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	public static class v2_71
	{
		public static void addVersions()
		{
			HitmanVersion.addVersion("2.71.0.0_dx11", 0x5D9DEA3E, v2_71_0_dx11);
			HitmanVersion.addVersion("2.71.0.0_dx12", 0x5D9DEA53, v2_71_0_dx12);
		}

		private static HitmanVersion v2_71_0_dx11 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F33293, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B5A238, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B5A25C, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BBB5E8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x182D598, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4EDA4, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BBBF28, "01", "00", MemProtection.PAGE_READWRITE) }
		};

		public static HitmanVersion v2_71_0_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F32DF3, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B59D98, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B59DBC, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BD9CA8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x18486B8, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4E904, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BDA5E8, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};
	}
}
