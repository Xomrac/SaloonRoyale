using System;
using System.Collections.Generic;

public static class Utility
{
	public static void Shuffle<T>(this IList<T> list)  
	{  
		var randomNumber = new Random();
		int n = list.Count;  
		while (n > 1) 
		{  
			n--;  
			int k = randomNumber.Next(n + 1);  
			(list[k], list[n]) = (list[n], list[k]);
		}  
	}	
}