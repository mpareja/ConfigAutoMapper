namespace ConfigAutoMapper
{
	public interface IConfigSource
	{
		string Get(string key);
	}
}