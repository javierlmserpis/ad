using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Serpis.Ad
{
	public class ModelHelper
	{
		public static string GetSelect(Type type) {
			string keyName = null;
			List<string> fieldNames = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyName = propertyInfo.Name.ToLower ();
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldNames.Add (propertyInfo.Name.ToLower());
			}
			
			string tableName = type.Name.ToLower();
			
			return string.Format ("select {0} from {1} where {2}=",
			                      string.Join(", ", fieldNames), tableName, keyName);
		}
		
		public static object Load(Type type, string id) {
			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand ();
			selectDbCommand.CommandText = GetSelect(type) + id;
			IDataReader dataReader = selectDbCommand.ExecuteReader();
			dataReader.Read(); //lee el primero
			
			object obj = Activator.CreateInstance(type);
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)){
					object value = convert(id, propertyInfo.PropertyType); 
					propertyInfo.SetValue(obj,value,null);
			}else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)){
					object value = convert(dataReader[propertyInfo.Name.ToLower()], propertyInfo.PropertyType,null);
					propertyInfo.SetValue(obj,value,null);
				}
			}
			dataReader.Close ();
			return obj;
		}
		
		private object convert(object value, Type type){
			return Convert.ChangeType (value,type);
		}
		
		private static string formatParameter(string field){
			return string.Format ("{0}=@{0}", field);
		}
		
		public static string GetUpdate(Type type){
			string keyParameter = null;
			List<string> fieldParameters = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyName = propertyInfo.Name.ToLower ();
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldParameters.Add (formatParameter(propertyInfo.Name.ToLower()));
			}
			string tableName = type.Name.ToLower();
			
			return string.Format ("update{0} set {1} where {2}", tableName, string.Join(", ", fieldParameters), keyParameter);
		}
		
	

		public static void Save(object obj) {
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			Type type = obj.GetType ();
			updateDbCommand.CommandText = GetUpdate (obj.GetType());
			DbCommandUtil.AddParameter (updateDbCommand, "", categoria.Nombre);
			
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true) || propertyInfo.IsDefined (typeof(KeyAttribute), true)){
					
					object value= propertyInfo.GetValue(object,null);
					
					DbCommandUtil.AddParameter (updateDbCommand, propertyInfo.Name.ToLower(), value);
					
			}
			
			updateDbCommand.ExecuteNonQuery ();
		
			}
		}
	}
}
/*/		public static String GetInsert(Type type){
		
				string keyName = null;
			List<string> fieldNames = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyName = propertyInfo.Name.ToLower ();
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldNames.Add (propertyInfo.Name.ToLower());
			}
				
			
				
			
			string tableName = type.Name.ToLower();
			
			return string.Format ("insert into {0} ({1}) VALUES ({2})",
			                       tableName, string.Join(", ", fieldNames));
				
		}

		
	/*/		