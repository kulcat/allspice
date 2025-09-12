<script setup>
import { AppState } from '@/AppState';
import RecipeIngredient from './RecipeIngredient.vue';
import { computed, onBeforeUnmount, onMounted, ref, watch } from 'vue';
import { Pop } from '../utils/Pop';
import { recipeService } from '../services/RecipeService';
import { Modal } from 'bootstrap';
import { Ingredient } from '@/models/Ingredient';
import { ingredientService } from '@/services/IngredientService';

const recipe = computed(() => AppState.activeRecipe);

const title = ref("");
const img = ref("");
const selectedCategory = ref("");
const categories = ['Breakfast', 'Lunch', 'Dinner', 'Snack', 'Dessert'];
const instructions = ref("");

const ingredients = ref([]);
const ingredientsToAdd = ref([]);
const ingredientsToRemove = ref([]);
const ingredientName = ref("");
const ingredientQuantity = ref("");

const editMode = ref(false);
const isOpen = ref(false);

watch(recipe, (val) => {
  if (val) {
    title.value = recipe.value.title;
    img.value = recipe.value.img;
    selectedCategory.value = capitalize(recipe.value.category);
    instructions.value = recipe.value.instructions;

    ingredients.value = recipe.value.ingredients;
  }
});

let modal;

onMounted(() => {
  modal = document.getElementById("viewRecipeModal");

  if (modal) {
    modal.addEventListener("hidden.bs.modal", onModalHidden);
  }
});

onBeforeUnmount(() => {
  if (modal) {
    modal.removeEventListener("hidden.bs.modal", onModalHidden);
  }
});

function onModalHidden() {
  editMode.value = false;
}

function addIngredient() {
  if(ingredientName.value && ingredientQuantity.value) {
    const ingredientData = {
      name: ingredientName.value,
      quantity: ingredientQuantity.value,
    }
    const newIngredient = new Ingredient(ingredientData);
    ingredientsToAdd.value.push(newIngredient);
  }
}

function removeIngredient(ingredient, index) {
  if (ingredientsToAdd.value.find(i => i.id === ingredient.id)) {
    ingredientsToAdd.value.splice(index, 1);
  }
  else if (ingredients.value.find(i => i.id === ingredient.id)) {
    ingredients.value.splice(index, 1);
    ingredientsToRemove.value.push(ingredient.id);
  }
}

async function deleteRecipe() {
  try {
    recipeService.deleteRecipe(recipe.value.id);
    const modal = Modal.getInstance(document.getElementById('viewRecipeModal'));
    modal.hide();
  } catch (e) {
    Pop.toast(e, "error");
  }
}

async function submitForm() {
  try {
    const recipeData = {
      title: title.value,
      img: img.value,
      category: selectedCategory.value,
      instructions: instructions.value,
    };

    const updatedRecipe = await recipeService.updateRecipe(recipeData);
    Pop.toast('Recipe created!', 'success');

    if (ingredientsToAdd.value.length > 0) {
      await ingredientService.createIngredients(ingredientsToAdd.value, updatedRecipe.id);
      ingredientsToAdd.value.length = 0;
    }

    if (ingredientsToRemove.value.length > 0) {
      await ingredientService.deleteIngredients(ingredientsToRemove.value);
      ingredientsToRemove.value.length = 0;
    }

    try {
      await recipeService.getRecipes();
    }
    catch (e) {
      Pop.toast("Could not get recipes", 'error');
      console.log(e);
    }

    try {
      await recipeService.setActiveRecipe(recipe.value);
    }
    catch (e) {
      Pop.toast("Could not get activeRecipe", 'error');
      console.log(e);
    }

    editMode.value = false;
  }
  catch (error) {
    Pop.toast('Could not create recipe', 'error');
    console.error(error);
  }
}

function capitalize(str) {
  return str ? str.charAt(0).toUpperCase() + str.slice(1) : "";
}

const imgError = (e) => {
  e.target.src = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
}


</script>

<template>
  <div class="modal fade" id="viewRecipeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content m-0 p-0">
        <div v-if="recipe" class="modal-body d-flex flex-column flex-md-row m-0 p-0 ">
          <button type="button" class="btn-close position-absolute end-0 top-0 m-2 fs-5 text-danger"
            data-bs-dismiss="modal" aria-label="Close" style="z-index: 100">
          </button>

          <div v-if="editMode" class="img-container col-md-6 m-0 p-0 position-relative" style="">
            <img class="img-fluid object-fit-cover w-100 h-100" :src="img" style="" @error="imgError">

            <div class="overlay d-flex justify-content-center align-items-center">
              <input type="text" v-model="img" placeholder="Image URL...">
            </div>
          </div>
          <div v-else class="img-container col-md-6 m-0 p-0 position-relative" style="">
            <img class="img-fluid object-fit-cover w-100 h-100" :src="recipe?.img" style="" @error="imgError">
          </div>

          <div class="col-md-6 p-3 h-100">

            <form @submit.prevent="submitForm" class="">
              <div class="d-flex h-25">
                <div class="d-flex flex-column">

                  <input v-if="editMode" class="fs-2 text-success" name="title" v-model="title" placeholder="Title"
                    :required="true">
                  <span v-else class="fs-2 fw-bold text-success"> {{ recipe?.title }}</span>
                  <span class="fs-4">
                    by: {{ recipe?.creator.name }}
                  </span>
                </div>
                <div class="dropdown">
                  <button v-if="AppState.account.id == recipe.creator.id" type="button" class="btn dropdown-toggle"
                    data-bs-toggle="dropdown">
                    <i class="mdi mdi-dots-horizontal fs-3"></i>
                  </button>

                  <div class="dropdown-menu">
                    <button type="button" class="btn dropdown-item" @click="editMode = !editMode">
                      <span v-if="editMode">
                        Cancel Edit
                      </span>
                      <span v-else>
                        Edit
                      </span>
                    </button>
                    <button type="button" class="btn dropdown-item" @click="deleteRecipe()">
                      Delete
                    </button>
                    <!-- <button type="button" class="btn" @click="favoriteRecipe()">
                      <i v-if="isFavorite" class="mdi mdi-bookmark"></i>
                      <i v-else class="mdi mdi-bookmark-outline"></i>
                    </button> -->
                  </div>
                </div>
              </div>

              <div class="mx-1">
                <select v-if="editMode" v-model="selectedCategory" required>
                  <option value="" disabled>Select a category...</option>
                  <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
                </select>
                <span v-else>{{ selectedCategory }}</span>
              </div>

              <div class="d-flex flex-column" style="max-height: 50%;">
                <span class="fs-3">Ingredients</span>
                <div v-if="editMode" class="d-flex flex-column flex-fill mx-1">

                  <div class="d-flex" v-for="(ingredient, index) in ingredients" :key="ingredient.id">
                    <button type="button" class="btn" @click="removeIngredient(ingredient, index)">
                      <i class="mdi mdi-close text-danger"></i>
                    </button>
                    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>
                  </div>
                  <div class="d-flex" v-for="(ingredient, index) in ingredientsToAdd" :key="ingredient.id">
                    <button type="button" class="btn" @click="removeIngredient(ingredient, index)">
                      <i class="mdi mdi-close text-danger"></i>
                    </button>
                    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>
                  </div>

                </div>
                <div v-else class="d-flex flex-column flex-fill mx-1">

                  <div class="d-flex" v-for="ingredient in recipe.ingredients" :key="ingredient.id">
                    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>
                  </div>

                </div>
                <div v-if="editMode" class="d-flex mx-1">
                  <input v-model="ingredientQuantity" placeholder="Ingredient Quantity">
                  <input v-model="ingredientName" placeholder="Ingredient Name">
                  <button type="button" class="btn rounded-circle text-white mx-2"
                    style="background-color: #587464; width: 2.5rem; height: 2.5rem;" @click="addIngredient">
                    <i class="mdi mdi-plus"></i>
                  </button>
                </div>
              </div>

              <div class="d-flex flex-column w-100">
                <span class="fs-3">Instructions</span>
                <textarea :disabled="!editMode" class="w-100" v-model="instructions" style="resize: none">

                </textarea>
              </div>
              <div class="d-flex justify-content-end">
                <button v-if="editMode" type="submit" class="btn btn-primary my-2 fs-5">Save Changes</button>
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