using System;
using System.Collections;

namespace SIS.Validation.Design
{
	/// <summary>
	/// RegExItemCollection allow RegExPattern to be bindable.
	/// </summary>
	public class RegExPatternCollection : CollectionBase
	{
		/// <summary>
		/// Get or set RegExPattern.
		/// </summary>
		public RegExPattern this[int index] 
		{
			get { return (RegExPattern) this.List[index]; } 
			set { 	this.List[index] = value; }
		}

		/// <summary>
		/// Add new RegExPattern to collection.
		/// </summary>
		/// <param name="rePattern"></param>
		public void Add(RegExPattern rePattern)
		{ 
			this.List.Add(rePattern);
		}	
	}
}
