using System;
using NUnit.Framework;

namespace Serpis.Ad
{
	internal class ModelHelperFoo {
		
		
		public int Id {get;set;}
		
		
		public string Nombre {get;set;}
	}
	
	[TestFixture()]
	public class ModelHelperTest
	{
		[Test()]
		public void GetSelect ()
		{
			string selectText = ModelHelper.GetSelect(typeof(ModelHelperFoo));
			
			string expected = "select nombre from modelhelperfoo where id=";
			Assert.AreEqual(expected, selectText );
		}
	}
}

