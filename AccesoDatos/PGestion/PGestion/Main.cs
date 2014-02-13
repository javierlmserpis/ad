using Gtk;
using System;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int i = (int)Convert.ChangeType("123", typeof(int));
			Console.WriteLine ("i={0}", i);
			
			object obj = Categoria.Load (typeof(Categoria), "");
			Console.WriteLine ("obj.Gettype()={0}", obj.GetType ());
			App.Instance.DbConnection = new MySqlConnection("Server=localhost;Database=dbprueba;User Id=root;Password=sistemas");
			
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
