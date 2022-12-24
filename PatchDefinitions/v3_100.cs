// Copyright (C) 2022 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	public static class v3_100
	{
		public static void addVersions()
		{
			HitmanVersion.addVersion("3.100.0.0_epic_dx12", 0x61E0EE81, v3_100_0_epic_dx12);
			HitmanVersion.addVersion("3.100.0.0_steam_dx12", 0x61E05DF7, v3_100_0_steam_dx12);
			HitmanVersion.addVersion("3.100.0.0-h1_epic_dx12", 0x61E96E3F, v3_100_0_h1_epic_dx12);
			HitmanVersion.addVersion("3.100.0.0-h1_steam_dx12", 0x61E9717D, v3_100_0_h1_steam_dx12);
			HitmanVersion.addVersion("3.100.0.0-h2_epic_dx12", 0x61F294F2, v3_100_0_h2_epic_dx12);
			HitmanVersion.addVersion("3.100.0.0-h2_steam_dx12", 0x61F294A5, v3_100_0_h2_steam_dx12);
		}

		private static HitmanVersion v3_100_0_epic_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0FE8D6D, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0CA289D, "0F85B5000000", "909090909090", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0CA2983, "0F84B8000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x3BF8E68, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6A5E8, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BF90C0, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_100_0_steam_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0FE85ED, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0C9FCBD, "0F85B5000000", "909090909090", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0C9FDA3, "0F84B8000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x3BFF7C8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6F790, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BFFA20, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_100_0_h1_epic_dx12 = new HitmanVersion()
		{
			certpin = v3_100_0_epic_dx12.certpin,
			authheader = v3_100_0_epic_dx12.authheader,
			configdomain = new[] { new Patch(0x3BF8E28, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6A5B8, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BF9080, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_100_0_h1_steam_dx12 = new HitmanVersion()
		{
			certpin = v3_100_0_steam_dx12.certpin,
			authheader = v3_100_0_steam_dx12.authheader,
			configdomain = new[] { new Patch(0x3BFF808, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6F7A0, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BFFA60, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_100_0_h2_epic_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0FE8CCD, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0CA287D, "0F85B5000000", "909090909090", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0CA2963, "0F84B8000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x3BF8FA8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6AA98, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BF9200, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_100_0_h2_steam_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0FE857D, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0C9FCCD, "0F85B5000000", "909090909090", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0C9FDB3, "0F84B8000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x3BFF808, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[] { new Patch(0x1E6F5A8, "68", "61", MemProtection.PAGE_READONLY) },
			dynres_noforceoffline = new[] { new Patch(0x3BFFA60, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};
	}
}
