

namespace NoSQLDatabase.Data
{
    public interface IDatabase
    {
        void Connect();
        void Disconnect();

        void Select();
        void Insert();
        void Update();
        void Delete();
    }
}