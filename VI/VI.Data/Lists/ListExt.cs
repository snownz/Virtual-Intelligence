using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VI.Data.Lists
{
	public static  class ListExt
	{
		
		public static List<T> CloneList<T>(this List<T> oldList)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream    stream    = new MemoryStream();
			formatter.Serialize(stream, oldList);
			stream.Position = 0;
			return (List<T>) formatter.Deserialize(stream);
		}

	}
}