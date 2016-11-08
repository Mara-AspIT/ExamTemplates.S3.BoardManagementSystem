using System;

namespace CrossCutting.Entities
{
	[Serializable]
	public class TestUserType : User
	{
		public TestUserType( string username ) : base( username )
		{

		}
	}
}
