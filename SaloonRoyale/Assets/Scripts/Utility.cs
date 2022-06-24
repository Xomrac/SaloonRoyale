using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

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
	
	public static GameObject GetPointedObject()
	{
		var results = new List<RaycastResult>();

		// Pack the pointer event data
		var eventData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};

		// Raycast using the event data
		EventSystem.current.RaycastAll(eventData, results);

		// Return the first raycast result
		foreach (var result in results)
		{
			return result.gameObject;
		}

		return null;
	}
}