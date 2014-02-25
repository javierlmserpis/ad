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
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					propertyInfo.SetValue(obj, id, null); //TODO convert al tipo de destino
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					propertyInfo.SetValue(obj, dataReader[propertyInfo.Name.ToLower()], null); //TODO convert al tipo de destino
			}
			dataReader.Close ();
			return obj;
		}
		public static string GetDelete(Type type){
			string KeyParameter = null;
			string KeyField = null;
			
			foreach (PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					KeyParameter = "@"+propertyInfo.Name.ToLower ();
					KeyField=propertyInfo.Name.ToLower();
				}
			}
			string tableName = type.Name.ToLower();
			return string.Format("DELETE FROM {0} where {1}={2}",tableName,KeyField,KeyParameter);
		}
		public static void Delete(object obj){
			Type type = obj.GetType();
			IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand();
			deleteDbCommand.CommandText = GetDelete (obj.GetType());
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				object valueType = propertyInfo.GetValue(obj,null);
				DbCommandUtil.AddParameter(deleteDbCommand, propertyInfo.Name.ToLower(), valueType);
				
			}
			deleteDbCommand.ExecuteNonQuery();
		}
		
		public static String GetInsert(Type type){
			string keyName;
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
		public static void Insert(object obj){
			Type type = obj.GetType();
			IDbCommand insertDbCommand = App.Instance.DbConnection.CreateCommand();
			insertDbCommand.CommandText = GetInsert (obj.GetType());
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				object valueType = propertyInfo.GetValue(obj,null);
				DbCommandUtil.AddParameter(insertDbCommand, propertyInfo.Name.ToLower(), valueType);
			}
			insertDbCommand.ExecuteNonQuery();
		}
	}
}

