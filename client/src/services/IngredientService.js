import { Pop } from "@/utils/Pop.js";
import { api } from "./AxiosService.js";
import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { Ingredient } from "@/models/Ingredient.js";

class IngredientService {
  async getIngredients() {
    try {
      const response = await api.get('api/Ingredients');
      console.log(response);

      AppState.ingredients.length = 0;

      response.data.forEach(data => {
        AppState.ingredients.push(new Ingredient(data));
      });
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

  async createIngredient(data, recipe_id) {
    try {
      const newIngredient = new Ingredient(data);
      newIngredient.recipe_id = recipe_id;

      const response = await api.post('api/Ingredients', newIngredient);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

  async createIngredients(ingredients, recipe_id) {
    try {
      ingredients.forEach(ingredient => {
        ingredient.recipe_id = recipe_id;
      })

      const response = await api.post('api/Ingredients/bulk-post', ingredients);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
      console.log(e);
    }
  }

  async deleteIngredient(id) {
    try {
      const response = await api.delete(`api/Ingredients/${id}`);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

  async deleteIngredients(ids) {
    try {
      const response = await api.post(`api/Ingredients/bulk-delete`, ids);
      console.log(response);
    } catch (e) {
      Pop.toast(e, 'error');
    }
  }

}



export const ingredientService = new IngredientService();