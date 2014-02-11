using System;
using NUnit.Framework;

namespace Serpis.Ad
{
	internal class ModelHelperFoo{
	[Key]
		public int Id {get;set;}
	[Field]
		public string Nombre{get;set;}
	}
	internal class ModelHelperBar{
	
		[Key]
		public int Id {get;set;}
		
		[Field]
		public string Nombre{get;set;}
		
		[Field]
		public decimal Precio {get;set;}
	}
	[TestFixture]
	public class ModelInfoTest
	{
		[Test]
		public void TableName ()
		{
			ModelInfo modelInfo = new ModelInfo(typeof(ModelInfoFoo));
			Assert.AreEqual("modelinfofoo", modelInfo.TableName);
		}
		[Test]
		public void KeyPropertyInfo() {
		ModelInfo modelInfo = new ModelInfo(typeof(ModelInfoFoo));
	    Assert.IsNotNull(modelInfo.keyPropertyInfo);
			Assert.AreEqual("Id", modelInfo.keyPropertyInfo.Name);
		}
		[Test]
		public void KeyName(){
			
		}
	}
}

