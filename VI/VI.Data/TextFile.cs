using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VI.Data
{
	public static class TextFile
	{
		public static string Open(string path)
		{
			return File.ReadAllText(path);
		}

		public static (Dictionary<char, int>, Dictionary<int, char>) GetUnique(this string text)
		{
			var chars = text.Distinct().ToArray();
			return (null, null);
		}
	}
}