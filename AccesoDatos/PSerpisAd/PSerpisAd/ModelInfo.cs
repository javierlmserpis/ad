using System;
using System.Reflection;


namespace Serpis.Ad
{
	public class ModelInfo
	{
		private Type type;
		public ModelInfo (Type type)
		{
			this.type = type;
			tableName = type.Name.ToLower ();
			foreach(PropertyInfo propertyInfo in type.GetProperties())
				if(propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyPropertyInfo = propertyInfo;
		}
		private string tableName;
		public string TableName {get {return tableName;}}
		
		
		public PropertyInfo keyPropertyInfo {get {return keyPropertyInfo;}}
	}
}

