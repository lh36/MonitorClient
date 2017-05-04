using System.Collections;
using System;
using System.Collections.Generic;

public class Algorithm 
{
	/*
	//快速排序算法，面向对象中的某一属性
	static public void QuickSortEntities(Entity[] entities, int left, int right)
	{
		if(left < right)
		{
			int id = entities[left].id;
			int low = left;
			int high = right;
			Entity entity = entities [left];

			while(low < high)
			{
				while(low < high && entities[high].id >= id)
				{
					high--;
				}
				entities[low] = entities[high];

				while(low < high && entities[low].id <= id)
				{
					low++;
				}
				entities[high] = entities[low];
			}

			entities[low] = entity;
			QuickSortEntities(entities,left,low-1);
			QuickSortEntities(entities,low+1,right);
		}
	}
*/

	//重新设定数组的大小
	public static Array Redim (Array origArray, int desiredSize)
	{
		//determine the type of element
		Type t = origArray.GetType().GetElementType();
		//create a number of elements with a new array of expectations
		//new array type must match the type of the original array
		Array newArray = Array.CreateInstance (t, desiredSize);
		//copy the original elements of the array to the new array
		Array.Copy (origArray, 0, newArray, 0, Math.Min (origArray.Length, desiredSize));
		//return new array
		return newArray;
	}

}

