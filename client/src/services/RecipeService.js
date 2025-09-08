import { Pop } from "@/utils/Pop.js";
import { api } from "./AxiosService.js";
import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";

class RecipeService {
  async getRecipes() {
    try {
      const response = await api.get('api/Recipes');
      console.log(response);

      AppState.recipes.length = 0;

      response.data.forEach(data => {
        AppState.recipes.push(new Recipe(data));
      });
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

  async createRecipe(recipeData) {
    try {
      const newRecipe = new Recipe(recipeData);
      newRecipe.creator_id = AppState.account.id;

      const response = await api.post('api/Recipes', newRecipe);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

}



export const recipeService = new RecipeService();