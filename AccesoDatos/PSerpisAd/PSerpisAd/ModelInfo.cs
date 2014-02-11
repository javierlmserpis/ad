using System;
using System.Reflection;

namespace Serpis.Ad
{
	public class ModelInfo
	{
		private Type type;
		public ModelInfo ()
		{
			this.type = type;
			//?¿
			public static string GetSelect(Type type) {
				string keyName = null;
				List<string> fieldNames = new List<string>();
				foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
					if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
						keyName = propertyInfo.Name.ToLower ();
					else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
						fieldNames.Add (propertyInfo.Name.ToLower());
				}

			tableName = type.Name.ToLower();
		}
		
		private string tableName;

		public string UpdateText{
			get { return; }
		
	}
}

