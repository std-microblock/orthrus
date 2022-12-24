// Copyright (C) 2021 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

namespace HitmanPatcher
{
	public static class v3_50
	{
		public static void addVersions()
		{
			HitmanVersion.addVersion("3.50.0.0_dx12", 0x60F92990, v3_50_0_dx12);
			HitmanVersion.addVersion("3.50.0.0-h1_dx12", 0x611E4422, v3_50_0_h1_dx12);
		}

		private static HitmanVersion v3_50_0_dx12 = new HitmanVersion()
		{
			certpin = new[] { new Patch(0x0FA4E2D, "0F85", "90E9", MemProtection.PAGE_EXECUTE_READ) },
			authheader = new[]
			{
				new Patch(0x0C6568D, "0F85B5000000", "909090909090", MemProtection.PAGE_EXECUTE_READ),
				new Patch(0x0C65773, "0F84B8000000", "909090909090", MemProtection.PAGE_EXECUTE_READ)
			},
			configdomain = new[] { new Patch(0x3B18DD8, "", "", MemProtection.PAGE_READWRITE, "configdomain") },
			protocol = new[]
			{
				new Patch(0x1E6FBA0, "68", "61", MemProtection.PAGE_READONLY) // dont ask me why this works
			},
			dynres_noforceoffline = new[] { new Patch(0x3B19758, "01", "00", MemProtection.PAGE_EXECUTE_READWRITE) }
		};

		private static HitmanVersion v3_50_0_h1_dx12 = new HitmanVersion()
		{
			certpin = v3_50_0_dx12.certpin,
			authheader = v3_50_0_dx12.authheader,
			configdomain = v3_50_0_dx12.configdomain,
			protocol = new[]
			{
				new Patch(0x1E6FB60, "68", "61", MemProtection.PAGE_READONLY) // dont ask me why this works
			},
			dynres_noforceoffline = v3_50_0_dx12.dynres_noforceoffline
		};
	}
}
