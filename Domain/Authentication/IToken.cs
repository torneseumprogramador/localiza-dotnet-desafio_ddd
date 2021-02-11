using System;
using Domain.Entities;

namespace Domain.Authentication
{
	public interface IToken
	{
		string GerarToken(IPerson person);
	}
}