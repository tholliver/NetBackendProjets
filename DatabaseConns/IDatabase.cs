namespace DatabaseConns
{
    public interface IDatabase
    {
        string Uri
        {
            get; set;
        }

        // Returns a Conn obj
        public void Connect();

        // Returns a void
        public void Disconnect();
        public void Select();
        public void Insert();
        public void Update();
        public void Delete();
    }
}