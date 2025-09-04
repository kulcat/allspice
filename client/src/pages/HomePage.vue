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
  <main class="container-fluid">

    <div class="d-flex justify-content-center align-items-center row" v-if="!(AppState.account && AppState.recipes)">
      <i class="mdi mdi-loading"></i>
    </div>

    <div v-else class="row">
      <div>
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createRecipeModal">
          Create
        </button>
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#recipeDetailsModal">
          View
        </button>
      </div>
      <div>
        <RecipeCompact v-for="recipe in AppState.recipes" :key="recipe.id" :recipe="recipe" />
      </div>
    </div>

    <div>
      <CreateRecipeModal />
      <RecipeDetailsModal />
    </div>
  </main>
</template>

<style scoped lang="scss"></style>
