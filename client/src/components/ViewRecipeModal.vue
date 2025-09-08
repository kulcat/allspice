<script setup>
import { AppState } from '@/AppState';
import RecipeIngredient from './RecipeIngredient.vue';
import { computed, ref } from 'vue';
import { Pop } from '../utils/Pop';
import { recipeService } from '../services/RecipeService';
import { Modal } from 'bootstrap';


const title = ref("");
const img = ref("");
const selectedCategory = ref("");
const categories = ['Breakfast', 'Lunch', 'Dinner', 'Snack', 'Dessert'];
const instructions = ref("");

var editMode = false;

function selectRecipe(selected) {
  selectedCategory.value = selected;
}

async function submitForm() {
  try {
    const recipeData = {
      title: title.value,
      img: img.value,
      category: selectedCategory.value,
      instructions: instructions.value,
    };

    await recipeService.createRecipe(recipeData);
    await recipeService.getRecipes();
    Pop.toast('Recipe created!', 'success');

    const modalElm = document.getElementById('createRecipeModal');
    const modal = Modal.getInstance(modalElm);
    modal.hide();
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
    <div class="modal-dialog m-0 p-0 modal-fullscreen">
      <div class="modal-content m-0 p-0">
        <div class="modal-body d-flex flex-column flex-md-row m-0 p-0 ">
          <button type="button" class="btn-close position-fixed end-0 top-0 m-2 fs-5" data-bs-dismiss="modal"
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
                  <span>
                    <input name="title" v-model="title" placeholder="Title" :required="true">
                  </span>
                  <span>
                    by: {{ AppState?.account?.name }}
                  </span>
                </div>
                <div>
                  <button type="button" class="btn" data-bs-toggle="dropdown" data-bs-target="dropdown">
                    {{ selectedCategory || "Select a category" }}
                  </button>

                  <ul class="dropdown-menu">
                    <li>
                      <button class="dropdown-item" @click="editMode = !editMode">
                        Edit
                      </button>
                      <button class="dropdown-item">
                        Delete
                      </button>
                    </li>
                  </ul>
                </div>
              </div>

              <div>
                <button type="button" class="btn" data-bs-toggle="dropdown" data-bs-target="dropdown">
                  {{ selectedCategory || "Select a category" }}
                </button>

                <ul class="dropdown-menu">
                  <li v-for="category in categories" :key="category">
                    <button class="dropdown-item" @click="selectRecipe(category)">
                      {{ category }}
                    </button>
                  </li>
                </ul>
              </div>

              <div class="d-flex flex-column">
                <span>Ingredients</span>
                <div>
                  <RecipeIngredient />
                </div>
                <div class="d-flex">
                  <input placeholder="Ingredient Quantity">
                  <input placeholder="Ingredient Name">
                  <div class="btn" type="button"></div>
                </div>
              </div>

              <div class="d-flex flex-column w-100">
                <span>Instructions</span>
                <textarea class="w-100" v-model="instructions">

                </textarea>
              </div>
              <div>

                <button type="submit" class="btn btn-primary">Create</button>
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