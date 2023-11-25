
public class FavoritesServices : IFavoritesServices
{
    public void AddFavorites(string favorite, string user)
    {
        FavoritesSql sql = new FavoritesSql();
        sql.AddFavorites(favorite,user);
    }

    public void DeleteFavorites(string stock, string user)
    {
        FavoritesSql sql = new FavoritesSql();
        sql.RemoveFavorites(stock, user);
    }

    public List<string> GetFavorites(string user)
    {
        FavoritesSql sql = new FavoritesSql();
        return sql.ReadFavorites(user);
    }
}