using System;
using System.Reflection;
using System.Collections.Generic;
namespace Serpis.Ad
{
	public class ModelInfo
	{
		public string TableName { 
			get {return tableName;} }
			private Type type;
			private List<string> fieldNames = new List<string>();
			public string insertText;
			private List<PropertyInfo> fieldPropertyInfos = new List<PropertyInfo>();
			
		
		public ModelInfo (Type type)
		{
			tableName = type.Name.ToLower ();
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
			if (propertyInfo.IsDefined(typeof(FieldAttribute),true)){
					fieldPropertyInfos.Add(propertyInfo);
					fieldNames.Add (propertyInfo.Name.ToLower());
				}
			}
				
		}
		private void setInsertText(){
			List<string> parameters = new List<string>();
			foreach (string fieldName in fieldNames)
				parameters.Add("@"+fieldName);
			insertText = string.Format ("insert into {0} ({1}) values ({2})", tableName, string.Join(", ", fieldNames), string.Join(", ",parameters));
			
		}
		public string InsertText {get {return null;}}
		public PropertyInfo[] FieldPropertyInfos {get {return fieldPropertyInfos.toArray ();}}
		private string tableName;
		private string keyParameter;
	}
}

