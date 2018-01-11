namespace TrackTv.Data
{
    public interface IPoco
    {
        int GetPrimaryKey();

        bool IsNew();

        void SetPrimaryKey(int value);
    }
}