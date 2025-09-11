<script setup>
import { recipeService } from '@/services/RecipeService';
import { Recipe } from '../models/Recipe';
import { ref } from 'vue';
import { AppState } from '@/AppState';
import { favoritesService } from '@/services/FavoritesService';
import { Pop } from '@/utils/Pop';

const props = defineProps({
  recipe: Recipe,
})

const favorite = ref(props.recipe.favorites.find(favorite => favorite.account_id === AppState.account.id));
const favoriteCount = ref(props.recipe.favoriteCount);

const imgError = (e) => {
  e.target.src = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
}

function capitalize(str) {
  return str ? str.charAt(0).toUpperCase() + str.slice(1) : "";
}

async function setActiveRecipe(recipe) {
  recipeService.setActiveRecipe(recipe);
}

async function toggleFavorite() {
  if (favorite.value) {
    await favoritesService.deleteFavorite(favorite.value.id);
  }
  else {
    const newFavorite = {
      recipe_id: props.recipe.id
    }
    await favoritesService.createFavorite(newFavorite);
  }

  try {
    await recipeService.getRecipes();
  }
  catch (e) {
    Pop.toast("Could not get recipes", 'error');
    console.log(e);
  }

  favorite.value = props.recipe.favorites.find(favorite => favorite.account_id === AppState.account.id)
  favoriteCount.value = props.recipe.favoriteCount;
}

</script>

<template>
  <div class="d-flex position-relative" style="width: 22.5rem; height: 22.5rem;">
    <img class="img-fluid object-fit-cover w-100 h-100 rounded-2" :src="recipe?.img" @error="imgError"
      data-bs-toggle="modal" data-bs-target="#viewRecipeModal" @click="setActiveRecipe(recipe)">

    <div class="d-flex position-absolute top-0 end-0 p-2">
      <button type="button" class="btn text-white rounded-3 px-3 py-1 fs-4 glass fw-medium z-1" @click="toggleFavorite">
        <span>{{ favoriteCount }}</span>
        <i v-if="favorite" class="mdi mdi-bookmark"></i>
        <i v-else class="mdi mdi-bookmark-outline"></i>
      </button>
    </div>

    <div class="d-flex justify-content-left position-absolute top-0 start-0 w-100 p-2">
      <span class="text-white rounded-3 px-3 py-1 fs-4 glass fw-medium">
        {{ capitalize(recipe?.category) }}
      </span>
    </div>

    <div class="d-flex justify-content-center position-absolute bottom-0 start-0 w-100 p-2">
      <span class="text-white rounded-3 px-3 py-1 w-100 fs-3 glass fw-medium">
        {{ capitalize(recipe?.title) }}
      </span>
    </div>

  </div>
</template>

<style lang="scss">
.glass {
  background-color: rgba(10, 10, 10, 0.25);
  border: 1px solid rgba(255, 255, 255, 0.06);
  -webkit-backdrop-filter: blur(10px);
  backdrop-filter: blur(10px);
}
</style>