using System;
using System.Collections.Generic;

namespace Serpis.Ad
{
	public static class ModelInfoStore
	{
		private static Dictionary<Type,ModelInfo> modelInfos;
		
		public static ModelInfo Get(Type type) {
			if(modelInfos.ContainsKey(type))
				return modelInfos[type];
			ModelInfo[type] = modelInfos;
			return modelInfo;
		}
		
	}
}

