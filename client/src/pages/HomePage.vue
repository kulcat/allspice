<script setup>
import { AppState } from '@/AppState';
import CreateRecipeModal from '@/components/CreateRecipeModal.vue';
import Example from '@/components/Example.vue';
import RecipeCompact from '@/components/RecipeCompact.vue';
import RecipeDetailsModal from '@/components/RecipeDetailsModal.vue';
import { recipeService } from '@/services/RecipeService';
import { Pop } from '@/utils/Pop';
import { computed, watch } from 'vue';

watch(
  () => AppState.recipes,
  () => {
    getRecipes();
  },
  { immediate: true }
);

const recipes = computed(() => AppState.recipes);

async function getRecipes() {
  try {
    await recipeService.getRecipes();
  }
  catch (e) {
    Pop.toast("Could not get recipes", 'error');
    console.log(e);
  }
}

</script>

<template>
  <main class="container position-relative">

    <div >

    </div>
    <div>

    </div>

    <div class="d-flex justify-content-center align-items-center w-100 h-100"
      v-if="!(AppState.account && AppState.recipes)">
      <i class="mdi mdi-loading"></i>
    </div>

    <div v-else class="">
      <div class="d-flex flex-column">
        <RecipeCompact v-for="recipe in AppState.recipes" :key="recipe.id" :recipe="recipe" />
      </div>
    </div>

    <button id="createButton" type="button" class="btn rounded-circle position-absolute text-white fs-1"
      data-bs-toggle="modal" data-bs-target="#createRecipeModal" style="background-color: #587464;">
      <i class="mdi mdi-plus"></i>
    </button>

    <div>
      <CreateRecipeModal />
      <RecipeDetailsModal />
    </div>
  </main>
</template>

<style scoped lang="scss">
#createButton {
  width: 5rem;
  height: 5rem;
  top: 80%;
  left: 90%;
}
</style>
