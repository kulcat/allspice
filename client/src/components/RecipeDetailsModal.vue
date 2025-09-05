<script setup>
import { AppState } from '@/AppState';
import RecipeIngredient from './RecipeIngredient.vue';
import { computed, ref } from 'vue';
import { Pop } from '../utils/Pop';
import { recipeService } from '../services/RecipeService';
import { Modal } from 'bootstrap';


const title = ref("");
const instructions = ref("");
const img = ref("");
const category = ref("");
const ingredients = ref([]);


async function submitForm() {
  try {
    const recipeData = {
      id: 0,
      title: title.value,
      instructions: instructions.value,
      img: "a",
      category: "Lunch"
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

function addRecipe() {

}

function removeRecipe() {

}
</script>

<template>
  <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog m-0 p-0 modal-fullscreen">
      <div class="modal-content m-0 p-0">
        <div class="modal-body d-flex flex-column flex-md-row m-0 p-0 ">
          <button type="button" class="btn-close position-absolute" data-bs-dismiss="modal" aria-label="Close"
            style=""></button>

          <div class="image-container col-md-6 m-0 p-0" style="">
            <img v-if="AppState?.activeRecipe?.img" class="img-fluid object-fit-cover w-100 h-100" src="" style="">
            <img v-else class="img-fluid object-fit-cover w-100 h-100"
              src="https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png" style="">
          </div>

          <div class="col-md-6 p-3">

            <form @submit.prevent="submitForm">
              <div class="d-flex">
                <div class="d-flex flex-column">
                  <span>
                    <input name="title" model="title" placeholder="Title" :required="true">
                  </span>
                  <span>
                    by: {{ AppState?.account?.name }}
                  </span>
                </div>
                <div>
                  <!-- dropdown edit delete -->
                </div>
              </div>
              <div>
                <!-- dropdown category -->
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
                <textarea class="w-100" model="instructions">

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

<style scoped lang="scss"></style>