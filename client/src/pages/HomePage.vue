<script setup>
import { AppState } from '@/AppState';
import CreateRecipeModal from '@/components/CreateRecipeModal.vue';
import Login from '@/components/Login.vue';
import RecipeCompact from '@/components/RecipeCompact.vue';
import ViewRecipeModal from '@/components/ViewRecipeModal.vue';
import { ingredientService } from '@/services/IngredientService';
import { recipeService } from '@/services/RecipeService';
import { Pop } from '@/utils/Pop';
import { computed, ref, watch } from 'vue';

watch(
  () => AppState.recipes,
  () => {
    getRecipes();
  },
  { immediate: true }
);

const recipes = computed(() => {
  let result = AppState.recipes;

  if (search.value) {
    const query = search.value.toLowerCase();
    result = result.filter(r =>
      r.title.toLowerCase().includes(query) ||
      r.category.toLowerCase().includes(query) ||
      r.creator.name.toLowerCase().includes(query)
    );
  }

  if (currentFilter.value == 'created') {
    result = result.filter(recipe => recipe.creator.id === AppState.account.id);
  }
  // else if(currentFilter.value == 'favorites') {
  //   
  // }

  return result;
});

const search = ref("");
const currentFilter = ref(null);

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
  <main class="container position-relative p-3 d-flex flex-column align-items-center">

    <div class="d-flex flex-column w-100 position-relative" style="height: 30dvh;">

      <div class="position-absolute top-0 start-0 w-100 h-100 rounded-2 shadow z-n1"
        style="background: url('https://sestra.nl/wp-content/uploads/2019/06/Het-wachten-waard-1024x683.jpg') center/cover no-repeat">
      </div>

      <div class="d-flex justify-content-end align-items-center w-100 gap-3 px-3 py-1">
        <div class="d-flex align-items-center position-relative bg-white rounded-2 px-1" style="height: 2.5rem">
          <input type="text" class="border-0" v-model="search" placeholder="Search">
          <button class="btn" style="right:0;"><i class="mdi mdi-magnify fs-5"></i></button>
        </div>
        <Login />
      </div>

      <div class="d-flex flex-column justify-content-center align-items-center text-white"
        style="font-family: 'Sahitya';">
        <span class=" fs-1">All-Spice</span>
        <span class="fs-3">Cherish Your Family</span>
        <span class="fs-3">And Their Cooking</span>
      </div>
    </div>

    <div class="d-flex justify-content-center w-100">
      <nav class="bg-white shadow px-3 py-1" style="transform: translate(0, -50%);">
        <button type="button" class="btn text-success" @click="currentFilter = null">
          Home
        </button>
        <button type="button" class="btn text-success" @click="currentFilter = 'created'">
          My Recipes
        </button>
        <button type="button" class="btn text-success" @click="currentFilter = 'favorites'">
          Favorites
        </button>
      </nav>
    </div>

    <div v-if="!(AppState.recipes && AppState.account)"
      class="d-flex justify-content-center align-items-center w-100 h-100">
      <i v-if="!AppState.recipes" class="mdi mdi-loading spinning fs-1"></i>
      <span v-else >Please log in</span>
    </div>

    <div v-else class="d-flex flex flex-wrap justify-content-center gap-5 mx-3 mt-5">
      <RecipeCompact v-for="recipe in recipes" :key="recipe.id" :recipe="recipe" />
    </div>

    <button id="createButton" type="button" class="btn rounded-circle position-fixed end-0 bottom-0 m-5 text-white fs-1"
      data-bs-toggle="modal" data-bs-target="#createRecipeModal" style="background-color: #587464;">
      <i class="mdi mdi-plus"></i>
    </button>

    <div>
      <CreateRecipeModal />
      <ViewRecipeModal />
    </div>
  </main>
</template>

<style scoped lang="scss">
#createButton {
  width: 5rem;
  height: 5rem;
}

.spinning {
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
}
</style>
