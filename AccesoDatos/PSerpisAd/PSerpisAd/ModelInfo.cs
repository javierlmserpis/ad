using System;

namespace Serpis.Ad
{
	public class ModelInfo
	{
		public string TableName { 
			get {return tableName;} }
		private Type type;
		
		public ModelInfo (Type type)
		{
			this.type = type;
			tableName = type.Name.ToLower ();
			
				
		}
		private string tableName;
		private string keyParameter;
		private string fieldNames;
		//public string[] FieldNames {get {return fieldNames.ToArray();}}
	}
}

