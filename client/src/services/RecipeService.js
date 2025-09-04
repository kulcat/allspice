import { Pop } from "@/utils/Pop.js";
import { api } from "./AxiosService.js";

class RecipeService {
  async getRecipes() {
    try {
      const response = await api.get('api/Recipes');
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }
}

export const recipeService = new RecipeService();