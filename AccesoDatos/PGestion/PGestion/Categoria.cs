using System;
using System.Data;

namespace Serpis.Ad
{
	public class Categoria
	{
		private int id;
		public int Id {
			get {return id;}
			set {id = value;}
		}
			
		private string nombre;
		public string Nombre {
				get {return nombre;}
				set { nombre = value;
				}
			}
		public static Categoria Load(string id){
			Categoria categoria = new Categoria();
			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = "select nombre from categoria where id=" +id;
			IDataReader dataReader = selectDbCommand.ExecuteReader();
			
			dataReader.Read();
			categoria.Id = int.Parse(id);
			categoria.Nombre = dataReader["nombre"].ToString();
			dataReader.Close();
			return categoria;
		}
		public static void Save(Categoria categoria){
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand();
			updateDbCommand.CommandText = "update categoria set nombre=@nombre where id=" +categoria.Id;
			DbCommandUtil.AddParameter(updateDbCommand, "nombre", categoria.Nombre);
			updateDbCommand.ExecuteNonQuery();
		}
	}
}