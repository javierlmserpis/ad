using Gtk;
using System;

namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		public CategoriaListView ()	{
			TreeViewHelper treeViewHelper = new TreeViewHelper(treeView, App.Instance.DbConnection, "select id, nombre from categoria");
		
			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			refreshAction.Activated += delegate {
				treeViewHelper.Refresh ();
			};			
			actionGroup.Add (refreshAction);
			
			Gtk.Action editAction = new Gtk.Action("editAction", null, null, Stock.Edit);
			editAction.Activated += delegate {
				Console.WriteLine("id={0}", treeViewHelper.Id);
				Console.WriteLine("categoria.Id={0}", treeViewHelper.Id);
			};
			
			actionGroup.Add (editAction);
			
			
			
		}
	}
}

