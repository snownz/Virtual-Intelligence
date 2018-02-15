namespace VI.NumSharp.Arrays
{
	public class Array<T>
		where T : class
	{
		public Array(T[] data)
		{
			AsArray = data;
		}

		public Array(int size)
		{
			AsArray = new T[size];
		}

		public int Length => AsArray.Length;

		public T this[int x]
		{
			get
			{
				if (x < 0) x = Length + x;
				return AsArray[x];
			}
			set
			{
				if (x < 0) x = Length + x;

				AsArray[x] = value;
			}
		}

		public T[] AsArray { get; }


		public override string ToString()
		{
			var str                                      = "[";
			for (var i = 0; i < AsArray.Length; i++) str += $"{AsArray[i]},\n ";
			str                                          =  str.Remove(str.Length - 2);
			str                                          += "]";
			return str;
		}
	}
}