using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class DataManager : SingletonUnity<DataManager> {

	private SQLiteHelper sqlite;

	void Start()
	{
		//pc
		string appDBPath = Application.dataPath  + "/data.db";
		
		sqlite = new SQLiteHelper (@"Data Source=" + appDBPath);
	}

	void OnDestroy()
	{
		sqlite.CloseConnection ();
	}

    /*public bool IsCollectionExist(Entity entity)
    {
		SqliteDataReader reader = sqlite.ReadTable ("collections", new string[]{ "id" }, new string[]{ "name" }, new string[]{ "=" }, new string[]{ "'" + entity.name + "'" });
        while (reader.Read())
        {
            if(reader.GetInt32(reader.GetOrdinal("id")) == entity.id)
            {
                return true;
            }
        } 
		reader.Close ();
        return false;
    }


	public void DeleteAllHistory()
	{
		sqlite.DeleteValuesOR ("history", new string[]{ "id" }, new string[]{ ">" }, new string[]{"'-1'"});
	}*/


}
