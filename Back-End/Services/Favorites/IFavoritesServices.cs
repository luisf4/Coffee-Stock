public interface IFavoritesServices { 
    public void AddFavorites(string favorite, string user);
    public List<string> GetFavorites(string user);
    public void DeleteFavorites(string id, string user);
}