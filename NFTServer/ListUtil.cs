using System.Collections;
using System.Collections.Generic;

public static class ListUtil {
	public static void SetSize<T>(this List<T> l, int size)
	{
		if(size < l.Count)
			l.RemoveRange(size, l.Count - size);
		else
			for(size -= l.Count; size > 0; size--)
				l.Add(default(T));
	}
}