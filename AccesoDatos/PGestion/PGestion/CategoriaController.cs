using System;
using System.Reflection;

namespace Serpis.Ad
{
	public class CategoriaController
	{
		public CategoriaController (Categoria categoria, CategoriaView categoriaView)
		{
			categoriaView.Nombre = categoria.Nombre;
			
			categoriaView.SaveActionDelegate = delegate {
				categoria.Nombre = categoriaView.Nombre;
				Categoria.Save(categoria);
			};
		}
	}
}

