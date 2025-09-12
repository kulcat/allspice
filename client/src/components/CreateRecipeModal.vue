<script setup>
import { AppState } from '@/AppState';
import RecipeIngredient from './RecipeIngredient.vue';
import { computed, ref } from 'vue';
import { Pop } from '../utils/Pop';
import { recipeService } from '../services/RecipeService';
import { Modal } from 'bootstrap';
import { Ingredient } from '@/models/Ingredient';
import { ingredientService } from '@/services/IngredientService';

const title = ref("");
const img = ref("");
const selectedCategory = ref("");
const categories = ['Breakfast', 'Lunch', 'Dinner', 'Snack', 'Dessert'];
const instructions = ref("");

const ingredients = ref([]);
const ingredientName = ref("");
const ingredientQuantity = ref("");

function addIngredient() {
  if(ingredientName.value && ingredientQuantity.value) {
    const ingredientData = {
      name: ingredientName.value,
      quantity: ingredientQuantity.value,
    }
    const newIngredient = new Ingredient(ingredientData);
    ingredients.value.push(newIngredient);  
  }
}

function removeIngredient(index) {
  ingredients.value.splice(index, 1);
}

async function submitForm() {
  try {
    const recipeData = {
      title: title.value,
      img: img.value,
      category: selectedCategory.value,
      instructions: instructions.value,
    };

    const recipe = await recipeService.createRecipe(recipeData);
    Pop.toast('Recipe created!', 'success');

    await ingredientService.createIngredients(ingredients.value, recipe.id);
    await recipeService.getRecipes();

    const modalElm = document.getElementById('createRecipeModal');
    const modal = Modal.getInstance(modalElm);
    modal.hide();

    title.value = "";
    img.value = "";
    selectedCategory.value = "";
    instructions.value = "";

    ingredients.value.length = 0;
    ingredientName.value = "";
    ingredientQuantity.value = "";

  }
  catch (error) {
    Pop.toast('Could not create recipe', 'error');
    console.error(error);
  }
}

const imgError = (e) => {
  e.target.src = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
}

</script>

<template>
  <div class="modal fade" id="createRecipeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content m-0 p-0">
        <div class="modal-body d-flex flex-column flex-md-row m-0 p-0 ">
          <button type="button" class="btn-close position-absolute end-0 top-0 m-2 fs-5" data-bs-dismiss="modal"
            aria-label="Close" style="z-index: 100"></button>

          <div class="img-container col-md-6 m-0 p-0 position-relative" style="">
            <img class="img-fluid object-fit-cover w-100 h-100" :src="img" style="" @error="imgError">

            <div class="overlay d-flex justify-content-center align-items-center">
              <input type="text" v-model="img" placeholder="Image URL...">
            </div>
          </div>

          <div class="col-md-6 p-3">

            <form @submit.prevent="submitForm">
              <div class="d-flex">
                <div class="d-flex flex-column">
                  <input class="fs-2" name="title" v-model="title" placeholder="Title" :required="true">
                  <span>
                    by: {{ AppState?.account?.name }}
                  </span>
                </div>
              </div>

              <div>
                <select class="dropdown-toggle" v-model="selectedCategory" required>
                  <option value="" disabled>Select a category...</option>
                  <option v-for="category in categories" :key="category">{{ category }}</option>
                </select>
              </div>

              <div class="d-flex flex-column">
                <span class="fs-3">Ingredients</span>
                <div class="d-flex flex-column flex-fill">
                  <div class="d-flex" v-for="(ingredient, index) in ingredients" :key="ingredient.id">
                    <button type="button" class="btn" @click="removeIngredient(index)">
                      <i class="mdi mdi-close text-danger"></i>
                    </button>
                    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>

                  </div>
                </div>
                <div class="d-flex">
                  <input v-model="ingredientQuantity" placeholder="Ingredient Quantity">
                  <input v-model="ingredientName" placeholder="Ingredient Name">
                  <button type="button" class="btn rounded-circle text-white"
                    style="background-color: #587464; width: 2.5rem; height: 2.5rem;" @click="addIngredient">
                    <i class="mdi mdi-plus"></i>
                  </button>
                </div>
              </div>

              <div class="d-flex flex-column w-100">
                <span class="fs-3">Instructions</span>
                <textarea class="w-100" v-model="instructions">

                </textarea>
              </div>
              <div class="d-flex justify-content-end">
                <button type="submit" class="btn btn-primary position-relative end-0 bottom-0 m-2 fs-5">Create</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.img-container {
  position: relative;
}

.img-container .overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  opacity: 0;
  transition: opacity 0.3s ease;
}

.img-container:hover .overlay {
  opacity: 1;
}

@media (min-width: 992px) {
  .img-container {
    height: none;
  }
}
</style>