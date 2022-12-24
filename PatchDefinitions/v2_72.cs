// Copyright (C) 2020-2021 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	public static class v2_72
	{
		public static void addVersions()
		{
			HitmanVersion.addVersion("2.72.0.0_dx11", 0x5DC03F3C, v2_72_0_dx11);
			HitmanVersion.addVersion("2.72.0.0_dx12", 0x5DC03F66, v2_72_0_dx12);
			HitmanVersion.addVersion("2.72.0.0-h1_dx11", 0x5DD54263, v2_72_0_h1_dx11);
			HitmanVersion.addVersion("2.72.0.0-h1_dx12", 0x5DD542FA, v2_72_0_h1_dx12);
			HitmanVersion.addVersion("2.72.0.0-h3_dx11", 0x5ED64501, v2_72_0_h3_dx11);
			HitmanVersion.addVersion("2.72.0.0-h3_dx12", 0x5ED6452D, v2_72_0_h3_dx12);
			HitmanVersion.addVersion("2.72.0.0-h4_dx11", 0x5EE9D065, v2_72_0_h4_dx11);
			HitmanVersion.addVersion("2.72.0.0-h4_dx12", 0x5EE9D095, v2_72_0_h4_dx12);
			HitmanVersion.addVersion("2.72.0.0-h5_dx11", 0x5F8D57CA, v2_72_0_h5_dx11);
			HitmanVersion.addVersion("2.72.0.0-h5_dx12", 0x5F8D56D3, v2_72_0_h5_dx12);
		}

		private static HitmanVersion v2_72_0_dx11 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F333B3, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B5A1F8, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B5A21C, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BBBC08, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x182D598, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4ED64, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BBC548, "01", "00", MemProtection.PAGE_READWRITE) }
		};

		public static HitmanVersion v2_72_0_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F32F13, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0B59D58, "75", "EB", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0B59D7C, "0F8486000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x2BDA208, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x18486B8, Patch.https, Patch.http, MemProtection.PAGE_READONLY),
				new Patch(0x0B4E8C4, "0C", "0B", MemProtection.PAGE_EXECUTE_READ)
			},
			dynres_noforceoffline = new[] { new Patch(0x2BDAB48, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v2_72_0_h1_dx11 = new HitmanVersion()
		{
			certpin = v2_72_0_dx11.certpin,
			authheader = v2_72_0_dx11.authheader,
			configdomain = new[] { new Patch(0x2BBBBE8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = v2_72_0_dx11.protocol,
			dynres_noforceoffline = new[] { new Patch(0x2BBC528, "01", "00", MemProtection.PAGE_READWRITE) }
		};

		public static HitmanVersion v2_72_0_h1_dx12 = new HitmanVersion()
		{
			certpin = v2_72_0_dx12.certpin,
			authheader = v2_72_0_dx12.authheader,
			configdomain = v2_72_0_dx12.configdomain,
			protocol = v2_72_0_dx12.protocol,
			dynres_noforceoffline = v2_72_0_dx12.dynres_noforceoffline
		};

		private static HitmanVersion v2_72_0_h3_dx11 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F33363, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = v2_72_0_dx11.authheader,
			configdomain = v2_72_0_h1_dx11.configdomain,
			protocol = v2_72_0_dx11.protocol,
			dynres_noforceoffline = v2_72_0_h1_dx11.dynres_noforceoffline
		};

		public static HitmanVersion v2_72_0_h3_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F32EC3, "75", "EB", MemProtection.PAGE_EXECUTE_READ) },
			authheader = v2_72_0_dx12.authheader,
			configdomain = v2_72_0_dx12.configdomain,
			protocol = v2_72_0_dx12.protocol,
			dynres_noforceoffline = v2_72_0_dx12.dynres_noforceoffline
		};

		private static HitmanVersion v2_72_0_h4_dx11 = new HitmanVersion()
		{
			certpin = v2_72_0_h3_dx11.certpin,
			authheader = v2_72_0_dx11.authheader,
			configdomain = v2_72_0_dx11.configdomain,
			protocol = v2_72_0_dx11.protocol,
			dynres_noforceoffline = v2_72_0_dx11.dynres_noforceoffline
		};

		public static HitmanVersion v2_72_0_h4_dx12 = new HitmanVersion()
		{
			certpin = v2_72_0_h3_dx12.certpin,
			authheader = v2_72_0_dx12.authheader,
			configdomain = v2_72_0_dx12.configdomain,
			protocol = v2_72_0_dx12.protocol,
			dynres_noforceoffline = v2_72_0_dx12.dynres_noforceoffline
		};

		public static HitmanVersion v2_72_0_h5_dx11 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F32FAE, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = v2_72_0_dx11.authheader,
			configdomain = v2_72_0_dx11.configdomain,
			protocol = v2_72_0_dx11.protocol,
			dynres_noforceoffline = v2_72_0_dx11.dynres_noforceoffline
		};

		public static HitmanVersion v2_72_0_h5_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0F32B0E, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = v2_72_0_dx12.authheader,
			configdomain = v2_72_0_dx12.configdomain,
			protocol = v2_72_0_dx12.protocol,
			dynres_noforceoffline = v2_72_0_dx12.dynres_noforceoffline
		};
	}
}
