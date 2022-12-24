// Copyright (C) 2020 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace HitmanPatcher
{
	public class Patch
	{
		public static readonly byte[] http = Encoding.ASCII.GetBytes("http://{0}\0").ToArray();
		public static readonly byte[] https = Encoding.ASCII.GetBytes("https://{0}\0").ToArray();

		public int offset;
		public byte[] original, patch;
		public string customPatch;
		public MemProtection defaultProtection;

		public Patch(int offset, byte[] original, byte[] patch, MemProtection defaultProtection, string customPatch = "")
		{
			this.offset = offset;
			this.original = original;
			this.patch = patch;
			this.defaultProtection = defaultProtection;
			this.customPatch = customPatch;
		}

		public Patch(int offset, string original, string patch, MemProtection defaultProtection, string customPatch = "")
			: this(offset, SoapHexBinary.Parse(original).Value, SoapHexBinary.Parse(patch).Value, defaultProtection, customPatch)
		{

		}
	}

	public class HitmanVersion
	{
		public Patch[] certpin, authheader, configdomain, protocol, dynres_noforceoffline;

		private static Dictionary<uint, string> timestampMap = new Dictionary<uint, string>();

		private static Dictionary<string, HitmanVersion> versionMap = new Dictionary<string, HitmanVersion>();

		public static readonly HitmanVersion NotFound = new HitmanVersion();

		public static IEnumerable<string> Versions
		{
			get { return versionMap.Keys; }
		}

		public static void addVersion(string name, uint timestamp, HitmanVersion patchVersions)
		{
			timestampMap.Add(timestamp, name);
			versionMap.Add(name, patchVersions);
		}

		private static string versionStringFromTimestamp(UInt32 timestamp)
		{
			string result;
			if (!timestampMap.TryGetValue(timestamp, out result))
			{
				result = "unknown";
			}
			return result;
		}

		public static HitmanVersion getVersion(UInt32 timestamp, string versionString = "")
		{
			if (versionString == "")
			{
				versionString = versionStringFromTimestamp(timestamp);
			}

			HitmanVersion version;
			if (versionMap.TryGetValue(versionString, out version))
			{
				return version;
			}

			return HitmanVersion.NotFound;
		}

		static HitmanVersion()
		{
			sniper_v1_0.addVersions();
			v1_12.addVersions();
			v1_15.addVersions();
			v1_16.addVersions();
			v2_13.addVersions();
			v2_70.addVersions();
			v2_71.addVersions();
			v2_72.addVersions();
			v3_10.addVersions();
			v3_11.addVersions();
			v3_20.addVersions();
			v3_30.addVersions();
			v3_40.addVersions();
			v3_50.addVersions();
			v3_70.addVersions();
			v3_100.addVersions();
			v3_110.addVersions();
			v3_120.addVersions();
		}
	}
}