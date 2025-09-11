import { AppState } from "@/AppState.js";
import { Favorite } from "@/models/Favorite.js";
import { Pop } from "@/utils/Pop.js";
import { api } from "./AxiosService.js";

export class FavoritesService {
  async createFavorite(favoriteData) {
    try {
      const newFavorite = new Favorite(favoriteData);
      newFavorite.account_id = AppState.account.id;

      const response = await api.post('api/Favorites', newFavorite);
      console.log(response);

      return response.data;
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

  async deleteFavorite(id) {
    try {
      const response = await api.delete(`api/Favorites/${id}`);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }
}

export const favoritesService = new FavoritesService();