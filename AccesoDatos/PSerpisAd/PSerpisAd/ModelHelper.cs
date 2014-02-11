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
					propertyInfo.SetValue(obj, id, null); //TODO convert al tipo destino
					
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					propertyInfo.SetValue(obj, dataReader[propertyInfo.Name.ToLower()], null); //TODO convert al tipo de destino
			}
			dataReader.Close ();
			return obj;
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
					object value= propertyInfo.GetValue(object, null);
					DbCommandUtil.AddParameter (updateDbCommand, propertyInfo.Name.ToLower(), value);
				}	
				
			
			updateDbCommand.ExecuteNonQuery ();
		
		}
			
		public static String GetInsert(Type type){
		
				string keyName = null;
			List<string> fieldNames = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties ()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyName = propertyInfo.Name.ToLower ();
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldNames.Add (propertyInfo.Name.ToLower());
			}
				
			List<string> fieldNames = new List<string>();
			foreach (type.)
				
			
			string tableName = type.Name.ToLower();
			
			return string.Format ("insert into {0} ({1}) VALUES ({2})",
			                       tableName, string.Join(", ", fieldNames));
				
		}

		
			
}

